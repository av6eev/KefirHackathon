using System.Text;
using System.Text.Json;

namespace Server.Save;

public static class SaveHelper
{
    private const char Separator = ';';
    
    public static void SaveToFile(string path, string fileName, List<object> data, Encoding encoding)
    {
        var fullPath = path + fileName + ".csv";
        var tempPath = path + $"temp_{fileName}" + ".csv";
        
        var options = new JsonSerializerOptions { WriteIndented = false };
        using var document = JsonSerializer.SerializeToDocument(data, options);
        var root = document.RootElement.EnumerateArray();
        var builder = new StringBuilder();

        using var tempStreamWriter = new StreamWriter(tempPath);
        
        if (root.Any())
        {
            var headers = root.First().EnumerateObject().Select(o => o.Name);

            if (!File.Exists(fullPath))
            {
                builder.AppendJoin(Separator, headers);
                builder.AppendLine();    
            }

            foreach (var element in root)
            {
                var row = element.EnumerateObject().Select(o => o.Value.ToString());

                if (File.Exists(fullPath))
                {
                    foreach (var line in File.ReadLines(fullPath, encoding))
                    {
                        if (row.First() != line.Split(Separator).First())
                        {
                            tempStreamWriter.WriteLine(line);
                        }
                    }    
                }
                    
                builder.AppendJoin(Separator, row);
                builder.AppendLine();
            }
            
            tempStreamWriter.Close();
            
            File.AppendAllText(tempPath, builder.ToString(), encoding);
            File.Delete(fullPath);       
            File.Move(tempPath, fullPath);
        }
    }
}
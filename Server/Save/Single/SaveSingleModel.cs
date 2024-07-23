using System.Text;
using Server.Utilities.Model;
using ServerCore.Main.Utilities;

namespace Server.Save.Single
{
    public class SaveSingleModel : IModel
    {
        public INotifySaveModel SaveModel { get; }

        public SaveSingleModel(INotifySaveModel saveModel)
        {
            SaveModel = saveModel;
        }

        public void Save()
        {
            var path = ServerConst.SavingFilesPath;
            var resultList = SaveModel.GetSaveDataList().ToList();
            
            SaveHelper.SaveToFile(path, SaveModel.SaveFileName, resultList, new UTF8Encoding());
        }

        public void FillFromSave(IDictionary<string, object> nodes)
        {
            SaveModel.SetSaveData(nodes);
        }
    }
}
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Utilities
{
    public class CoreSwitcher
    {
        [MenuItem("Libs/Core switch to file", false, 2000)]
        private static void SwitchToFile()
        {
            var path = Application.dataPath + "/../Packages/manifest.json";
            var fileData = File.ReadAllText(path);
            var replaceItemFrom = "https://github.com/av6eev/SpaceAdventureLib.git";
            var replaceItemTo = "file:../../SpaceAdventureLib";
            fileData = fileData.Replace(replaceItemFrom, replaceItemTo);
            File.WriteAllText(path, fileData);
        }

        [MenuItem("Libs/Core switch to file", true, 2000)]
        static bool ValidateSwitchToFile()
        {
            var fileData = File.ReadLines(Application.dataPath + "/../Packages/manifest.json");
            var findItem = "\"space_adventure_lib\": \"https://github.com/av6eev/SpaceAdventureLib.git\"";

            foreach (var data in fileData)
            {
                if (data.Contains(findItem))
                {
                    return true;
                }
            }

            return false;
        }

        [MenuItem("Libs/Core switch to SSH", false, 2001)]
        private static void SwitchToSsh()
        {
            var path = Application.dataPath + "/../Packages/manifest.json";
            var fileData = File.ReadAllText(path);
            var replaceItemFrom = "file:../../SpaceAdventureLib";
            var replaceItemTo = "https://github.com/av6eev/SpaceAdventureLib.git";
            fileData = fileData.Replace(replaceItemFrom, replaceItemTo);
            File.WriteAllText(path, fileData);
        }

        [MenuItem("Libs/Core switch to SSH", true, 2001)]
        static bool ValidateSwitchToSsh()
        {
            var fileData = File.ReadLines(Application.dataPath + "/../Packages/manifest.json");
            var findItem = "\"space_adventure_lib\": \"file:../../SpaceAdventureLib\"";

            foreach (var data in fileData)
            {
                if (data.Contains(findItem))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
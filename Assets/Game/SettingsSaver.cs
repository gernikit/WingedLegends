using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace Game
{
    public class SettingsSaver
    {
        public void SaveSettings(GameSettings settings)
        {
            string jsonString = JsonConvert.SerializeObject(settings, Formatting.Indented);
            
            File.WriteAllText(@"Assets\Game\settings.json", jsonString);
        }
    }
}

using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Game
{
    public class SettingsSaver
    {
        private string _defaultPath = @"Assets\Game\settings.json";
        
        public void SaveSettings(GameSettings settings)
        {
            string jsonString = JsonConvert.SerializeObject(settings, Formatting.Indented, new StringEnumConverter());
            
            File.WriteAllText(_defaultPath, jsonString);
        }
    }
}

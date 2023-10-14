using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UnityEngine;

namespace Game
{
    public class SettingsLoader
    {
        private readonly string _defaultPath = @"Assets\Game\settings.json";
        
        public GameSettings LoadSettings()
        {
            try
            {
                if (File.Exists(_defaultPath))
                {
                    string jsonString = File.ReadAllText(_defaultPath);

                    GameSettings loadedSettings =
                        JsonConvert.DeserializeObject<GameSettings>(jsonString, new StringEnumConverter());

                    return loadedSettings;
                }
                else
                {
                    throw new FileNotFoundException($"The file '{_defaultPath}' does not exist.");
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"An error occurred while loading settings: {ex.Message}");
                return null;
            }
        }
    }
}

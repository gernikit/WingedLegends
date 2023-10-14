using UnityEngine;
using Map;

namespace Game
{
    public class GameRoot : MonoBehaviour
    {
        [SerializeField] private MapMediator _mapMediator;
        [SerializeField] private MapPresenter _mapPresenter;

        private GameSettings _settings;
        private PlayerData _playerData;

        private SettingsSaver _settingsSaver;
        private SettingsLoader _settingsLoader;
        
        private void Start()
        {
            _settingsSaver = new SettingsSaver();
            _settingsLoader = new SettingsLoader();
            
            LoadSettings();

            _playerData = new PlayerData(_settings.availableHeroes,
                _settings.missionCollection,
                _settings.doubleMissionsData,
                _settings.missionConditionalsData);
            _mapMediator.Init(_playerData);
            _mapPresenter.Init(_mapMediator, _playerData);
        }

        private void SaveSettings()
        {
            _settingsSaver.SaveSettings(_settings);
        }

        private void LoadSettings()
        {
            _settings = _settingsLoader.LoadSettings();
        }
    }
}

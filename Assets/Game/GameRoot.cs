using Heroes;
using UnityEngine;
using Map;
using Missions;

namespace Game
{
    public class GameRoot : MonoBehaviour
    {
        [SerializeField] private MapMediator _mapMediator;
        [SerializeField] private MapPresenter _mapPresenter;

        private GameSettings _settings;
        private PlayerData _playerData;

        private SettingsSaver _settingsSaver = new SettingsSaver();
        private SettingsLoader _settingsLoader = new SettingsLoader();
        
        private void Start()
        {
            MissionData missionData = new MissionData(0, "1", MissionState.Activated,
                new System.Numerics.Vector2(0,-4),
                new () {},
                new (){4, 5});
            MissionData missionData2 = new MissionData(1, "2", MissionState.Blocked,
                new System.Numerics.Vector2(0,-3),
                new () {0},
                new (){});
            MissionData missionData3 = new MissionData(2, "3", MissionState.Blocked,
                new System.Numerics.Vector2(0,-2),
                new () {0, 1},
                new (){});
            MissionCompletionData missionCompletionData =
                new MissionCompletionData(
                    new () { HeroType.Owl },
                    new ()
                    {
                        new Hero(HeroType.Hawk , 1)
                    });
            
            MissionHistoryData missionHistoryData = new MissionHistoryData(
                "Переполох в гнезде",
                "В последние годы в птичьем мире творится что-то странное. Птенцы пропадают из гнезда, перелётные птицы улетают и не возвращается. После исчезновения целого клана грачей Птичий Совет решил всё-таки разобраться с исчезновениями. Совет отправляет Ястреба (стартовый персонаж) в лес расспросить птиц о деталях.",
                "History",
                "PlayerSide",
                "EnemySide"
            );
            
            Mission mission = new Mission(missionData, missionCompletionData, missionHistoryData);
            Mission mission2 = new Mission(missionData2, missionCompletionData, missionHistoryData);
            Mission mission3 = new Mission(missionData3, missionCompletionData, missionHistoryData);
            
            _settings = new GameSettings();
            _settings.availableHeroes.Add(HeroType.Hawk);
            _settings.missionCollection.Add(mission);
            _settings.missionCollection.Add(mission2);
            _settings.missionCollection.Add(mission3);
            _settings.doubleMissionsData.Add(new DoubleMissionData(1,2));
            _settings.missionConditionalsData.Add(new MissionConditionalsData(9, HeroType.Owl));
            _settings.missionConditionalsData.Add(new MissionConditionalsData(10, HeroType.Raven));
            //SaveSettings();

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

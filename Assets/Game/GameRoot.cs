using System.Collections.Generic;
using Heroes;
using UnityEngine;
using Map;
using Missions;

namespace Game
{
    public class GameRoot : MonoBehaviour
    {
        [SerializeField] private MapMediator _mapMediator;
        [SerializeField] private List<MissionPoint> _missionPoints;

        private GameSettings _settings = new GameSettings();

        private SettingsSaver _settingsSaver = new SettingsSaver();
        private SettingsLoader _settingsLoader = new SettingsLoader();
        
        private void Start()
        {
            int missionsCount = 12;
            MissionData missionData = new MissionData(0, MissionState.Activated, new System.Numerics.Vector2(1,2), new List<uint>() {1,2});
            MissionCompletedData missionCompletedData =
                new MissionCompletedData(
                    new () { HeroType.Hawk },
                    new (){1, 2},
                    new ()
                    {
                        { HeroType.Hawk , 1}
                    });
            MissionHistoryData missionHistoryData = new MissionHistoryData(
                "Переполох в гнезде",
                "В последние годы в птичьем мире творится что-то странное. Птенцы пропадают из гнезда, перелётные птицы улетают и не возвращается. После исчезновения целого клана грачей Птичий Совет решил всё-таки разобраться с исчезновениями. Совет отправляет Ястреба (стартовый персонаж) в лес расспросить птиц о деталях.",
                "History",
                "PlayerSide",
                "EnemySide"
            );
            
            Mission mission = new Mission(missionData, missionCompletedData, missionHistoryData);

            for (int i = 0; i < missionsCount; i++)
            {
                _missionPoints[i].InitMissionPoint(_mapMediator, mission);
            }
            
            _settings.missionCount = 1;
            _settings.availableHeroes.Add(HeroType.Hawk);
            _settings.missionCollection.Add(mission);
            _settings.doubleMissionsData.Add(new DoubleMissionData(1,2));
            SaveSettings();

           //LoadSettings();
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

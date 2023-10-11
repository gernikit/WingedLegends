using System.Collections.Generic;
using System.Numerics;
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

        private SettingsSaver _settingsSaver = new SettingsSaver();
        private SettingsLoader _settingsLoader = new SettingsLoader();
        
        private void Start()
        {
            int missionsCount = 12;
            MissionData missionData = new MissionData(0, MissionState.Activated, new System.Numerics.Vector2(1,2), null);
            MissionCompletedData missionCompletedData =
                new MissionCompletedData(new List<Hero>() { new Hero(HeroName.Hawk, 0) }, null, null);
            SingleMission mission = new SingleMission(missionData, missionCompletedData);

            for (int i = 0; i < missionsCount; i++)
            {
                _missionPoints[i].InitMissionPoint(_mapMediator, mission);
            }

            GameSettings settings = new GameSettings();
            settings.missionData = missionData;
            SaveSettings(settings);
        }

        private void SaveSettings(GameSettings settings)
        {
            _settingsSaver.SaveSettings(settings);
        }
    }
}

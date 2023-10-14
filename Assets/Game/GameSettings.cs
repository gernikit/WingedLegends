using System;
using System.Collections.Generic;
using Heroes;
using Missions;

namespace Game
{
    [Serializable]
    public class GameSettings
    {
        public List<HeroType> availableHeroes;
        public List<Mission> missionCollection;
        public List<DoubleMissionData> doubleMissionsData;
        public List<MissionConditionalsData> missionConditionalsData;

        public GameSettings()
        {
            availableHeroes = new ();
            missionCollection = new ();
            doubleMissionsData = new ();
            missionConditionalsData = new ();
        }
    }
}
using System;
using System.Collections.Generic;
using Heroes;
using Missions;

namespace Game
{
    [Serializable]
    public class GameSettings
    {
        public uint missionCount;
        public List<HeroType> availableHeroes;
        public List<Mission> missionCollection;
        public List<DoubleMissionData> doubleMissionsData;

        public GameSettings()
        {
            availableHeroes = new List<HeroType>();
            missionCollection = new List<Mission>();
            doubleMissionsData = new List<DoubleMissionData>();
        }
    }
}
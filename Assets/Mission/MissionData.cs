using System;
using System.Collections.Generic;
using System.Numerics;
using Heroes;

namespace Missions {
    public enum MissionState
    {
        Activated,
        Blocked,
        TemporarilyBlocked,
        Completed
    }

    [Serializable]
    public class MissionCompletedData
    {

        private List<Hero> _unlockedHeroes;
        private List<MissionData> _temporarilyBlocked;
        private Dictionary<HeroName, int> _heroPoints;

        public MissionCompletedData(List<Hero> unlockedHeroes, List<MissionData> temporarilyBlocked, Dictionary<HeroName, int> heroPoints)
        {
            _unlockedHeroes = unlockedHeroes;
            _temporarilyBlocked = temporarilyBlocked;
            _heroPoints = heroPoints;
        }
        
        public List<Hero> UnlockedHeroes => _unlockedHeroes;
        public List<MissionData> TemporarilyBlocked => _temporarilyBlocked;
        public Dictionary<HeroName, int> HeroPoints => _heroPoints;
    }
    
    [Serializable]
    public class MissionData
    {
        private uint _id;
        private MissionState _state;
        private Vector2 _coordinates;
        private List<MissionData> _dependencies;
        
        public MissionData(uint id, MissionState state, Vector2 coordinates, List<MissionData> dependencies)
        {
            _id = id;
            _state = state;
            _coordinates = coordinates;
            _dependencies = dependencies;
        }
        
        public uint ID => _id;
        public MissionState State => _state;
        public Vector2 Coordinates => _coordinates;
        public List<MissionData> Dependencies => _dependencies;
    }
    
    public abstract class Mission {}

    public class SingleMission : Mission
    {
        private MissionData _missionData;
        private MissionCompletedData _completedData;

        public SingleMission(MissionData missionData, MissionCompletedData completedData)
        {
            _missionData = missionData;
            _completedData = completedData;
        }

        public MissionData MissionData => _missionData;
        public MissionCompletedData MissionCompletedData => _completedData;
    }

    public class DoubleMission : Mission
    {
        private SingleMission _first;
        private SingleMission _second;

        public DoubleMission(SingleMission first, SingleMission second)
        {
            _first = first;
            _second = second;
        }

        public SingleMission First => _first;
        public SingleMission Second => _second;
    }
}
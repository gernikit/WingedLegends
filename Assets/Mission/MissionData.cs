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
    public class MissionData
    {
        private uint _id;
        private MissionState _state;
        private Vector2 _coordinates;
        private List<uint> _dependenciesIds;
        
        public MissionData(uint id, MissionState state, Vector2 coordinates, List<uint> dependenciesIds)
        {
            _id = id;
            _state = state;
            _coordinates = coordinates;
            _dependenciesIds = dependenciesIds;
        }
        
        public uint ID => _id;
        public MissionState State => _state;
        public Vector2 Coordinates => _coordinates;
        public List<uint> DependenciesIds => _dependenciesIds;
    }
    
    [Serializable]
    public class MissionCompletedData
    {
        private List<HeroType> _unlockedHeroes;
        private List<uint> _tempBlockedMissionIds;
        private Dictionary<HeroType, int> _heroPoints;

        public MissionCompletedData(List<HeroType> unlockedHeroes, List<uint> tempBlockedMissionIds, Dictionary<HeroType, int> heroPoints)
        {
            _unlockedHeroes = unlockedHeroes;
            _tempBlockedMissionIds = tempBlockedMissionIds;
            _heroPoints = heroPoints;
        }
        
        public List<HeroType> UnlockedHeroes => _unlockedHeroes;
        public List<uint> TempBlockedMissionIds => _tempBlockedMissionIds;
        public Dictionary<HeroType, int> HeroPoints => _heroPoints;
    }

    [Serializable]
    public class MissionHistoryData
    {
        private string _name;
        private string _prehistoryText;
        private string _historyText;
        private string _playerSide;
        private string _enemySide;
        
        public MissionHistoryData(string name, string prehistoryText, string historyText, string playerSide, string enemySide)
        {
            _name = name;
            _prehistoryText = prehistoryText;
            _historyText = historyText;
            _playerSide = playerSide;
            _enemySide = enemySide;
        }
        
        public string Name => _name;
        public string PrehistoryText => _prehistoryText;
        public string HistoryText => _historyText;
        public string PlayerSide => _playerSide;
        public string EnemySide => _enemySide;
    }

    [Serializable]
    public class DoubleMissionData
    {
        private uint _firstMissionId;
        private uint _secondMissionId;
        
        public DoubleMissionData(uint firstMissionId, uint secondMissionId)
        {
            _firstMissionId = firstMissionId;
            _secondMissionId = secondMissionId;
        }

        public uint FirstMissionId => _firstMissionId;
        public uint SecondMissionId => _secondMissionId;
    }
    

    [Serializable]
    public class Mission
    {
        private MissionData _missionData;
        private MissionCompletedData _completedData;
        private MissionHistoryData _missionHistoryData;

        public Mission(MissionData missionData, MissionCompletedData completedData, MissionHistoryData missionHistoryData)
        {
            _missionData = missionData;
            _completedData = completedData;
            _missionHistoryData = missionHistoryData;
        }

        public MissionData MissionData => _missionData;
        public MissionCompletedData MissionCompletedData => _completedData;
        public MissionHistoryData MissionHistoryData => _missionHistoryData;
    }
}
using System;
using System.Collections.Generic;
using System.Numerics;
using Heroes;
using Newtonsoft.Json;

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
        private string _number;
        private MissionState _state;
        private Vector2 _coordinates;
        private List<uint> _dependenciesIds;
        private List<uint> _tempBlockedMissionIds;
        
        public MissionData(uint id, string number, MissionState state, Vector2 coordinates, List<uint> dependenciesIds, List<uint> tempBlockedMissionIds)
        {
            _id = id;
            _number = number;
            _state = state;
            _coordinates = coordinates;
            _dependenciesIds = dependenciesIds;
            _tempBlockedMissionIds = tempBlockedMissionIds;
        }
        
        public uint ID => _id;
        public string Number => _number;
        public MissionState State => _state;
        public Vector2 Coordinates => _coordinates;
        public List<uint> DependenciesIds => _dependenciesIds;
        public List<uint> TempBlockedMissionIds => _tempBlockedMissionIds;

        public void ChangeState(MissionState state)
        {
            _state = state;
        }
    }
    
    [Serializable]
    public class MissionCompletionData
    {
        private List<HeroType> _unlockedHeroes;
        private List<Hero> _heroPoints;
        
        public MissionCompletionData(List<HeroType> unlockedHeroes, List<Hero> heroPoints)
        {
            _unlockedHeroes = unlockedHeroes;
            _heroPoints = heroPoints;
        }
        
        public List<HeroType> UnlockedHeroes => _unlockedHeroes;
        public List<Hero> HeroPoints => _heroPoints;
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

        public bool TryGetSecondMission(uint id, out uint secondId)
        {
            if (id == _firstMissionId)
            {
                secondId = _secondMissionId;
                return true;
            }
            else if (id == _secondMissionId)
            {
                secondId = _firstMissionId;
                return true;
            }
            else
            {
                secondId = id;
                return false;
            }
        }
    }

    [Serializable]
    public class MissionConditionalsData
    {
        private uint _idMission;
        private HeroType _requiredHero;
        
        public MissionConditionalsData(uint idMission, HeroType requiredHero)
        {
            _idMission = idMission;
            _requiredHero = requiredHero;
        }
        
        public uint IDMission => _idMission;
        public HeroType RequiredHero => _requiredHero;

        public bool CanActivateMission(List<HeroType> availableHeroes)
        {
            if (availableHeroes.Contains(_requiredHero))
                return true;
            else
                return false;
        }
    }
    
    [Serializable]
    public class Mission
    {
        [JsonProperty("MissionData")]
        private MissionData _missionData;
        [JsonProperty("MissionCompletionData")]
        private MissionCompletionData _missionCompletionData;
        [JsonProperty("MissionHistoryData")]
        private MissionHistoryData _missionHistoryData;
        
        public Mission(MissionData missionData, MissionCompletionData completionData, MissionHistoryData missionHistoryData)
        {
            _missionData = missionData;
            _missionCompletionData = completionData;
            _missionHistoryData = missionHistoryData;
        }

        [JsonIgnore]
        public MissionData MissionData => _missionData;
        [JsonIgnore]
        public MissionCompletionData MissionCompletionData  => _missionCompletionData;
        [JsonIgnore]
        public MissionHistoryData MissionHistoryData => _missionHistoryData;

        public void ChangeState(MissionState state)
        {
            _missionData.ChangeState(state);
        }
    }
}
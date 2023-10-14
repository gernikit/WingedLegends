using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Heroes;
using Missions;

namespace Game
{
    public class PlayerData
    {
        private List<HeroType> _availableHeroes;
        private List<Mission> _missionsCollection;
        private List<DoubleMissionData> _doubleMissionsData;
        private List<MissionConditionalsData> _missionConditionalsData;
        private List<Hero> _heroesData;
        
        public PlayerData(List<HeroType> availableHeroes,
            List<Mission> missionCollection,
            List<DoubleMissionData> doubleMissionsData,
            List<MissionConditionalsData> missionConditionalsData)
        {
            _availableHeroes = new (availableHeroes);
            _missionsCollection = new (missionCollection);
            _doubleMissionsData = doubleMissionsData;
            _missionConditionalsData = missionConditionalsData;
            InitDefaultHeroesData();
        }
        
        public PlayerData(List<HeroType> availableHeroes,
            List<Mission> missionCollection,
            List<DoubleMissionData> doubleMissionsData,
            List<Hero> heroesData,
            List<MissionConditionalsData> missionConditionalsData)
        {
            _availableHeroes = new (availableHeroes);
            _missionsCollection = new (missionCollection);
            _doubleMissionsData = doubleMissionsData;
            _heroesData = new(heroesData);
            _missionConditionalsData = missionConditionalsData;
        }

        public List<HeroType> AvailableHeroes => _availableHeroes;
        public List<Mission> MissionsCollection => _missionsCollection;
        public List<DoubleMissionData> DoubleMissionsData => _doubleMissionsData;
        public List<Hero> HeroesData => _heroesData;
        public List<MissionConditionalsData> MissionConditionalsData => _missionConditionalsData;

        private void InitDefaultHeroesData()
        {
            _heroesData = new ();
            
            foreach (HeroType heroType in Enum.GetValues(typeof(HeroType)))
            {
                _heroesData.Add(new Hero(heroType, 0));
            }
        }

        public List<Mission> TempBlockMissions(List<uint> missionsIds)
        {
            List <Mission> tempBlockedMission = _missionsCollection
                .Where(mission => missionsIds.Contains(mission.MissionData.ID))
                .ToList();
            tempBlockedMission.ForEach(mission => mission.ChangeState(MissionState.TemporarilyBlocked));
            
            return tempBlockedMission;
        }

        public bool CheckActivateConditional(uint idMission)
        {
            MissionConditionalsData conditionals = _missionConditionalsData
                .Find(conditional => conditional.IDMission == idMission);

            if (conditionals == null)
                return true;
            else
                return conditionals.CanActivateMission(_availableHeroes);

        }

        public bool TryGetSecondMission(uint missionId, out uint secondMissionId)
        {
            uint tempSecondMissionId = missionId;
            DoubleMissionData missionData = _doubleMissionsData.Find(doubleMission =>
                doubleMission.TryGetSecondMission(missionId, out tempSecondMissionId));
            secondMissionId = tempSecondMissionId;
            
            return missionData != null;
        }

        public void RemoveDependencyBranchMission(Mission currentMission)
        {
            Mission dependency = currentMission;
            List<Mission> dependentMissions;

            do
            {
                dependentMissions =
                    _missionsCollection
                        .Where(mission => mission.MissionData.DependenciesIds.Contains(dependency.MissionData.ID))
                        .ToList();

                foreach (var dependentMission in dependentMissions)
                {
                    dependentMission.MissionData.DependenciesIds.Remove(dependency.MissionData.ID);

                    if (dependentMission.MissionData.DependenciesIds.Count == 0)
                        RemoveDependencyBranchMission(dependentMission);
                }
            } while (dependentMissions == null);
        }

        public void UnlockHeroes(List<HeroType> heroesTypes)
        {
            foreach (var heroType in heroesTypes)
            {
                if (_availableHeroes.Contains(heroType) == true)
                {
                    throw new FileNotFoundException($"This hero has already been unblocked.");
                }
                else
                {
                    _availableHeroes.Add(heroType);
                }
            }
        }
    }
}
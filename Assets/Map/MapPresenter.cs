using System.Collections.Generic;
using System.Linq;
using Game;
using Heroes;
using Missions;
using UnityEngine;

namespace Map
{
    public class MapPresenter : MonoBehaviour
    {
        private const int WinPoints = 5; 
        
        [SerializeField] private List<MissionPoint> _missionsPoints;
        
        private MapMediator _mapMediator;
        private PlayerData _playerData;

        public void Init(MapMediator mapMediator, PlayerData playerData)
        {
            _mapMediator = mapMediator;
            _playerData = playerData;
            
            for (int i = 0; i < playerData.MissionsCollection.Count; i++)
            {
                _missionsPoints[i].InitMissionPoint(this, playerData.MissionsCollection[i], _playerData);
            }
        }

        public void OnMissionSelected(Mission mission)
        {
            _mapMediator.ShowPrehistory(mission);
        }

        public void OnMissionCompleted(Mission completedMission)
        {
            ChangeStateMissionPoint(completedMission, MissionState.Completed);
            CheckForUnlockHero(completedMission.MissionCompletionData.UnlockedHeroes);
            AddHeroPoints(completedMission.MissionCompletionData.HeroPoints);
            UpdateDoubleMissionsStates(completedMission);
            UpdateMissionsStates(completedMission);
        }

        private void ChangeStateMissionPoint(Mission mission, MissionState missionState)
        {
            _missionsPoints.First(missionPoint => missionPoint.Mission.MissionData.ID == mission.MissionData.ID)
                .OnChangedState(missionState);
        }
        

        private void CheckForUnlockHero(List<HeroType> heroesTypes)
        {
            if (heroesTypes.Count > 0)
                _playerData.UnlockHeroes(heroesTypes.Except(_playerData.AvailableHeroes).ToList());
        }

        private void AddHeroPoints(List<Hero> heroPoints)
        {
            var allHeroPoints = new List<Hero>();
            allHeroPoints.AddRange(heroPoints);
            allHeroPoints.Add(new Hero(_mapMediator.SelectedHero, WinPoints));
            
            foreach (var heroPoint in allHeroPoints)
            {
                if (heroPoint.Points < 0 
                    || (heroPoint.Points > 0 && _playerData.AvailableHeroes.Contains(heroPoint.Type)))
                    _playerData.HeroesData.First(heroData => heroData.Type == heroPoint.Type).AddPoints(heroPoint.Points);
            }
        }

        private void UpdateMissionsStates(Mission completedMission)
        {
            foreach (var mission in _playerData.MissionsCollection)
            {
                if (mission.MissionData.TempBlockedMissionIds.Contains(completedMission.MissionData.ID))
                    mission.MissionData.TempBlockedMissionIds.Remove(completedMission.MissionData.ID);

                if (mission.MissionData.DependenciesIds.Contains(completedMission.MissionData.ID))
                {
                    mission.MissionData.DependenciesIds.Remove(completedMission.MissionData.ID);
                    
                    if (mission.MissionData.DependenciesIds.Count == 0
                        &&  _playerData.CheckActivateConditional(mission.MissionData.ID))
                    {
                            var tempBlockedMissions = _playerData.TempBlockMissions(mission.MissionData.TempBlockedMissionIds);

                            foreach (var blockedMission in tempBlockedMissions)
                            {
                                ChangeStateMissionPoint(blockedMission, MissionState.TemporarilyBlocked);
                            }
                            
                            mission.MissionData.ChangeState(MissionState.Activated);
                            ChangeStateMissionPoint(mission, MissionState.Activated);
                    }
                }
            }
        }

        private void UpdateDoubleMissionsStates(Mission completedMission)
        {
            uint secondMissionId = completedMission.MissionData.ID;
            
            if (_playerData.TryGetSecondMission(completedMission.MissionData.ID, out secondMissionId))
            {
                Mission secondMission = _playerData.MissionsCollection.First(mission => mission.MissionData.ID == secondMissionId);
                _playerData.RemoveDependencyBranchMission(secondMission);
                ChangeStateMissionPoint(secondMission, MissionState.Blocked);
            }
        }
    }
}
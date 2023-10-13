using System.Collections.Generic;
using System.Linq;
using Game;
using Heroes;
using Missions;
using Unity.VisualScripting;
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
            _mapMediator.ShowHeroesTab();
            _mapMediator.ShowPrehistory(mission);
        }

        public void OnMissionCompleted(Mission completedMission)
        {
            ChangeStateMissionPoint(completedMission, MissionState.Completed);
            CheckForUnlockHero(completedMission.MissionCompletionData.UnlockedHeroes);
            AddHeroPoints(completedMission.MissionCompletionData.HeroPoints);
            OverrideMissionsStates(completedMission, _playerData.MissionsCollection);
            OverrideDoubleMissionsStates(completedMission, _playerData.MissionsCollection, _playerData.DoubleMissionsData);
        }

        private void ChangeStateMissionPoint(Mission mission, MissionState missionState)
        {
            _missionsPoints.First(missionPoint => missionPoint.Mission.MissionData.ID == mission.MissionData.ID)
                .OnChangedState(missionState);
        }
        

        private void CheckForUnlockHero(List<HeroType> heroesTypes)
        {
            if (heroesTypes.Count > 0)
                _playerData.AvailableHeroes.AddRange(heroesTypes.Except(_playerData.AvailableHeroes));
        }

        private void AddHeroPoints(List<Hero> heroPoints)
        {
            var allHeroPoints = new List<Hero>();
            allHeroPoints.AddRange(heroPoints);
            allHeroPoints.Add(new Hero(_mapMediator.SelectedHero, WinPoints));
            
            foreach (var heroPoint in allHeroPoints)
            {
                if (heroPoint.Points < 0 ||
                    heroPoint.Points > 0 && _playerData.AvailableHeroes.Contains(heroPoint.Type))
                    _playerData.HeroesData.First(heroData => heroData.Type == heroPoint.Type).AddPoints(heroPoint.Points);
            }
        }

        private void OverrideMissionsStates(Mission currentMission, List<Mission> missions)
        {
            foreach (var mission in missions)
            {
                if (mission.MissionData.DependenciesIds.Contains(currentMission.MissionData.ID))
                {
                    mission.MissionData.DependenciesIds.Remove(currentMission.MissionData.ID);

                    if (mission.MissionData.DependenciesIds.Count == 0)
                    {
                        if (mission.MissionData.TempBlockedMissionIds.Count > 0)
                        {
                            mission.MissionData.ChangeState(MissionState.TemporarilyBlocked);
                            ChangeStateMissionPoint(mission, MissionState.TemporarilyBlocked);
                        }
                        else
                        {
                            mission.MissionData.ChangeState(MissionState.Activated);
                            ChangeStateMissionPoint(mission, MissionState.Activated);
                        }
                    }
                }
                
                if (mission.MissionData.TempBlockedMissionIds.Contains(currentMission.MissionData.ID))
                {
                    mission.MissionData.TempBlockedMissionIds.Remove(currentMission.MissionData.ID);
                    
                    if (mission.MissionData.TempBlockedMissionIds.Count == 0 
                        && mission.MissionData.DependenciesIds.Count == 0 )
                    {
                        mission.MissionData.ChangeState(MissionState.Activated);
                        ChangeStateMissionPoint(mission, MissionState.Activated);
                    }
                }
            }
        }

        private void OverrideDoubleMissionsStates(Mission currentMission, List<Mission> missions, List<DoubleMissionData> doubleMissions)
        {
            uint secondMissionId = currentMission.MissionData.ID;
            
            if (doubleMissions.Count(doubleMission => 
                    doubleMission.TryGetSecondMission(currentMission.MissionData.ID, out secondMissionId)) == 1)
            {
                Mission secondMission = missions.First(mission => mission.MissionData.ID == secondMissionId);
                ChangeStateMissionPoint(secondMission, MissionState.TemporarilyBlocked);
            }
        }
    }
}
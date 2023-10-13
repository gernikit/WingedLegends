using System;
using System.Collections.Generic;
using System.IO;
using Heroes;
using Missions;

namespace Game
{
    public class PlayerData
    {
        private List<HeroType> _availableHeroes;
        private List<Mission> _missionsCollection;
        private List<DoubleMissionData> _doubleMissionsData;
        private List<Hero> _heroesData;
        
        public PlayerData(List<HeroType> availableHeroes, List<Mission> missionCollection, List<DoubleMissionData> doubleMissionsData)
        {
            _availableHeroes = new (availableHeroes);
            _missionsCollection = new (missionCollection);
            _doubleMissionsData = doubleMissionsData;
            InitDefaultHeroesData();
        }
        
        public PlayerData(List<HeroType> availableHeroes, List<Mission> missionCollection, List<DoubleMissionData> doubleMissionsData, List<Hero> heroesData)
        {
            _availableHeroes = new (availableHeroes);
            _missionsCollection = new (missionCollection);
            _doubleMissionsData = doubleMissionsData;
            _heroesData = new(heroesData);
        }

        public List<HeroType> AvailableHeroes => _availableHeroes;
        public List<Mission> MissionsCollection => _missionsCollection;
        public List<DoubleMissionData> DoubleMissionsData => _doubleMissionsData;
        public List<Hero> HeroesData => _heroesData;

        private void InitDefaultHeroesData()
        {
            _heroesData = new ();
            
            foreach (HeroType heroType in Enum.GetValues(typeof(HeroType)))
            {
                _heroesData.Add(new Hero(heroType, 0));
            }
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
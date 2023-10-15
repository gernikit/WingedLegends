using System.Collections.Generic;
using System.Linq;
using Map;
using UnityEngine;

namespace Heroes
{
    public class HeroesTab : MonoBehaviour
    {
        [SerializeField] private MapMediator _mapMediator;
        [SerializeField] private GameObject _heroesTab;
        [SerializeField] private List<HeroPresenter> _heroes;

        public void OnSelectHero(HeroType heroType)
        {
            IsHeroChosen = true;
            SelectedHero = heroType;
            _mapMediator.HideAdviceChooseHero();
            
            _heroes.Where(hero => hero.HeroType != heroType)
                .ToList()
                .ForEach(hero => hero.SetActiveSelectedIcon(false));
        }
        
        public bool IsHeroChosen { get; private set; }
        public HeroType SelectedHero { get; private set; }

        public void UpdateHeroesTab(List<HeroType> availableHeroes, List<Hero> heroesData)
        {
            _heroes.Where(hero => availableHeroes.Contains(hero.HeroType))
                .ToList()
                .ForEach(hero =>
                {
                    Hero heroData = heroesData.First(heroData => hero.HeroType == heroData.Type);
                    hero.SetHeroInfo(heroData);
                    hero.gameObject.SetActive(true);
                });
        }

        public void ShowHeroesTab()
        {
            _heroesTab.SetActive(true);
        }
        
        public void HideHeroesTab()
        {
            _heroesTab.SetActive(false);
        }
    }
}

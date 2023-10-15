using Game;
using Heroes;
using Missions;
using UnityEngine;

namespace Map
{
    public class MapMediator : MonoBehaviour
    {
        [SerializeField] private PrehistoryPresenter _prehistoryPresenter;
        [SerializeField] private HistoryPresenter _historyPresenter;
        [SerializeField] private HeroesTab _heroesTab;
        [SerializeField] private GameObject _adviceChooseHero;

        private PlayerData _playerData;

        public void Init(PlayerData playerData)
        {
            _playerData = playerData;
        }

        public HeroType SelectedHero => _heroesTab.SelectedHero;
        
        public void ShowPrehistory(Mission mission) => _prehistoryPresenter.ShowPrehistory(mission);

        public void HidePrehistory() => _prehistoryPresenter.HidePrehistory();

        public void ShowHistory(Mission mission) => _historyPresenter.ShowHistory(mission, _playerData);

        public void HideHistory() => _historyPresenter.HideHistory();

        public void ShowHeroesTab() => _heroesTab.ShowHeroesTab();

        public void HideHeroesTab() => _heroesTab.HideHeroesTab();

        public void UpdateHeroesTab() =>
            _heroesTab.UpdateHeroesTab(_playerData.AvailableHeroes, _playerData.HeroesData);

        public void ShowAdviceChooseHero() => _adviceChooseHero.SetActive(true);
        
        public void HideAdviceChooseHero() => _adviceChooseHero.SetActive(false);
    }
}

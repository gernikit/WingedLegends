using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Heroes
{
    public class HeroPresenter : MonoBehaviour
    {
        [SerializeField] private GameObject _selectedIcon;
        [SerializeField] private TMP_Text _points;
        [SerializeField] private TMP_Text _name;
        [SerializeField] private Button _heroButton;

        [SerializeField] private HeroesTab _heroesTab;

        [SerializeField] private HeroType _heroType;

        private void OnEnable()
        {
            _heroButton.onClick.AddListener(OnSelectHero);
        }

        private void OnDisable()
        {
            _heroButton.onClick.RemoveListener(OnSelectHero);
        }

        private void OnSelectHero()
        {
            _heroesTab.OnSelectHero(_heroType);
            SetActiveSelectedIcon(true);
        }

        public HeroType HeroType => _heroType;

        public void SetHeroInfo(Hero hero)
        {
            _points.text = hero.Points.ToString();
        }

        public void SetActiveSelectedIcon(bool enabled)
        {
            _selectedIcon.SetActive(enabled);
        }
    }
}

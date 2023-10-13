using Heroes;
using Map;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Missions
{
    public class PrehistoryPresenter : MonoBehaviour
    {
        [SerializeField] private GameObject _prehistoryWindow;
        [SerializeField] private GameObject _chooseHeroPanel;
        [SerializeField] private TMP_Text _missionNameLeft;
        [SerializeField] private TMP_Text _missionNameRight;
        [SerializeField] private TMP_Text _missionPrehistoryLeft;
        [SerializeField] private TMP_Text _missionPrehistoryRight;
        [SerializeField] private Button _startMissionLeftButton;
        [SerializeField] private Button _startMissionRightButton;
        
        [SerializeField] private MapMediator _mapMediator;
        [SerializeField] private HeroesTab _heroesTab;
        
        private Mission _currentMission;
        
        private void OnEnable()
        {
            _startMissionLeftButton.onClick.AddListener(OnMissionStart);
            _startMissionRightButton.onClick.AddListener(OnMissionStart);
        }

        private void OnDisable()
        {
            _startMissionLeftButton.onClick.RemoveListener(OnMissionStart);
            _startMissionRightButton.onClick.RemoveListener(OnMissionStart);
        }

        private void OnMissionStart()
        {
            if (_heroesTab.IsHeroChosen)
            {
                HidePrehistory();
                _chooseHeroPanel.SetActive(false);
                _mapMediator.HideHeroesTab();
                _mapMediator.ShowHistory(_currentMission);
            }
            else
            {
                _chooseHeroPanel.SetActive(true);
            }
        }

        public void ShowPrehistory(Mission mission)
        {
            _currentMission = mission;
            
            _missionNameLeft.text = mission.MissionHistoryData.Name;
            _missionNameRight.text = mission.MissionHistoryData.Name;
            _missionPrehistoryLeft.text = mission.MissionHistoryData.PrehistoryText;
            _missionPrehistoryRight.text = mission.MissionHistoryData.PrehistoryText;
            
            _prehistoryWindow.SetActive(true);
        }

        public void HidePrehistory()
        {
            _prehistoryWindow.SetActive(false);
        }
    }
}

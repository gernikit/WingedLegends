using Game;
using Map;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Missions
{
    public class HistoryPresenter : MonoBehaviour
    {
        [SerializeField] private GameObject _historyWindow;
        [SerializeField] private TMP_Text _missionName;
        [SerializeField] private TMP_Text _playerSide;
        [SerializeField] private TMP_Text _enemySide;
        [SerializeField] private TMP_Text _missionHistory;
        [SerializeField] private Button _finishMissionButton;

        [SerializeField] private MapMediator _mapMediator;
        [SerializeField] private MapPresenter _mapPresenter;

        private Mission _currentMission;
        private PlayerData _playerData;

        private void OnEnable()
        {
            _finishMissionButton.onClick.AddListener(OnMissionFinish);
        }

        private void OnDisable()
        {
            _finishMissionButton.onClick.RemoveListener(OnMissionFinish);
        }

        private void OnMissionFinish()
        {
            _mapPresenter.OnMissionCompleted(_currentMission);
            HideHistory();
        }

        public void ShowHistory(Mission mission, PlayerData playerData)
        {
            _currentMission = mission;
            
            _missionName.text = mission.MissionHistoryData.Name;
            _playerSide.text = mission.MissionHistoryData.PlayerSide;
            _enemySide.text = mission.MissionHistoryData.EnemySide;
            _missionHistory.text = mission.MissionHistoryData.HistoryText;
            
            _historyWindow.SetActive(true);
        }

        public void HideHistory()
        {
            _historyWindow.SetActive(false);
        }
    }
}

using TMPro;
using UnityEngine;

namespace Missions
{
    public class HistoryView : MonoBehaviour
    {
        [SerializeField] private GameObject _historyWindow;
        [SerializeField] private TMP_Text _missionName;
        [SerializeField] private TMP_Text _playerSide;
        [SerializeField] private TMP_Text _enemySide;
        [SerializeField] private TMP_Text _missionHistory;

        public void ShowHistory(Mission mission)
        {
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

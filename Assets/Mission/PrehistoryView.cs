using TMPro;
using UnityEngine;

namespace Missions
{
    public class PrehistoryView : MonoBehaviour
    {
        [SerializeField] private GameObject _prehistoryWindow;
        [SerializeField] private TMP_Text _missionNameLeft;
        [SerializeField] private TMP_Text _missionNameRight;
        [SerializeField] private TMP_Text _missionPrehistoryLeft;
        [SerializeField] private TMP_Text _missionPrehistoryRight;

        public void ShowPrehistory(Mission mission)
        {
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

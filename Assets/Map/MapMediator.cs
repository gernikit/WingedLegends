using Missions;
using UnityEngine;

namespace Map
{
    public class MapMediator : MonoBehaviour
    {
        [SerializeField] private PrehistoryView _prehistoryView;
        [SerializeField] private HistoryView _historyView;

        private Mission currentMission;

        public void ShowPrehistory(Mission mission) => _prehistoryView.ShowPrehistory(mission);

        public void HidePrehistory() => _prehistoryView.HidePrehistory();

        public void ShowHistory(Mission mission) => _historyView.ShowHistory(mission);

        public void HideHistory() => _historyView.HideHistory();
    }
}

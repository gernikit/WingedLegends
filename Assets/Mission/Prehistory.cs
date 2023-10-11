using UnityEngine;

namespace Missions
{
    public class Prehistory : MonoBehaviour
    {
        [SerializeField] private GameObject _prehistoryWindow;

        public void ShowPrehistory(SingleMission mission)
        {
            _prehistoryWindow.SetActive(true);
        }

        public void HidePrehistory()
        {
            _prehistoryWindow.SetActive(false);
        }
    }
}

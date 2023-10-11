using Missions;
using UnityEngine;

namespace Map
{
    public class MapMediator : MonoBehaviour
    {
        [SerializeField] private Prehistory _prehistory;

        public void ShowPrehistory(SingleMission mission) => _prehistory.ShowPrehistory(mission);
    }
}

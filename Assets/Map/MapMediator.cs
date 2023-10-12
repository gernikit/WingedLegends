using Missions;
using UnityEngine;

namespace Map
{
    public class MapMediator : MonoBehaviour
    {
        [SerializeField] private Prehistory _prehistory;

        public void ShowPrehistory(Mission mission) => _prehistory.ShowPrehistory(mission);
    }
}

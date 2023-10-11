using Map;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Missions
{
    public class MissionPoint : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        
        private MapMediator _mapMediator;
        private SingleMission _mission;

        public void InitMissionPoint(MapMediator mapMediator, SingleMission mission)
        {
            _mapMediator = mapMediator;
            transform.position = new Vector2(mission.MissionData.Coordinates.X,mission.MissionData.Coordinates.Y) ;

            if (mission.MissionData.State == MissionState.Activated)
                _spriteRenderer.enabled = true;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _mapMediator.ShowPrehistory(_mission);
        }
    }
}

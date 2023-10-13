using Game;
using Map;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Missions
{
    public class MissionPoint : MonoBehaviour
    {
        [SerializeField] private TMP_Text _missionNumber;
        [SerializeField] private Button _missionButton;
        [SerializeField] private Image _completionMark;
        [SerializeField] private Image _lockMark;
        
        [SerializeField] private MapPresenter _mapPresenter;
        [SerializeField] private GameObject _background;
        
        private Mission _mission;

        private void OnEnable()
        {
            _missionButton.onClick.AddListener(OnMissionSelect);
        }

        private void OnDisable()
        {
            _missionButton.onClick.RemoveListener(OnMissionSelect);
        }

        public void OnMissionSelect()
        {
            _mapPresenter.OnMissionSelected(_mission);
        }

        public void OnChangedState(MissionState state)
        {
            if (state == MissionState.Activated)
            {
                _background.SetActive(true);
                _completionMark.enabled = false;
                _lockMark.enabled = false;
                _missionButton.enabled = true;
            }
            else if (state == MissionState.TemporarilyBlocked)
            {
                _background.SetActive(true);
                _completionMark.enabled = false;
                _lockMark.enabled = true;
                _missionButton.enabled = false;
            }
            else if (state == MissionState.Blocked)
            {
                _background.SetActive(false);
                _completionMark.enabled = false;
                _lockMark.enabled = false;
                _missionButton.enabled = false;
            }
            else if (state == MissionState.Completed)
            {
                _background.SetActive(true);
                _completionMark.enabled = true;
                _lockMark.enabled = false;
                _missionButton.enabled = false;
            }
        }

        public Mission Mission => _mission;

        public void InitMissionPoint(MapPresenter mapPresenter, Mission mission, PlayerData playerData)
        {
            _mapPresenter = mapPresenter;
            _mission = mission;
            _missionNumber.text = mission.MissionData.Number;
            transform.position = new Vector2(mission.MissionData.Coordinates.X, mission.MissionData.Coordinates.Y);

            OnChangedState(mission.MissionData.State);
        }
    }
}

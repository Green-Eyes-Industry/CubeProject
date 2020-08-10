using UnityEngine;

namespace GES
{
    public class PlayerMovemant : MonoBehaviour
    {
        private GamepadController _gamepadController;
        private LevelMode _levelMode;

        #region Prvate

        private void Start()
        {
            _gamepadController = FindObjectOfType<GamepadController>();
            _levelMode = FindObjectOfType<LevelMode>();
        }

        private void FixedUpdate()
        {
            if (_levelMode.isTimerStarted) transform.localPosition += _gamepadController.GetGamepadVector();
        }

        #endregion
    }
}
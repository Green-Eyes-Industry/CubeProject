using UnityEngine;
using UnityEngine.EventSystems;

namespace GES
{
    public class GamepadController : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
    {
        private RectTransform _gamepadBackground;
        private RectTransform _gamepadCenter;
        private Vector2 _inputVector;

        #region Methods

        public void OnDrag(PointerEventData eventData)
        {
            Vector2 pos;
            if(RectTransformUtility.ScreenPointToLocalPointInRectangle(_gamepadBackground,eventData.position,eventData.pressEventCamera, out pos))
            {
                pos.x = pos.x / _gamepadBackground.sizeDelta.x;
                pos.y = pos.y / _gamepadBackground.sizeDelta.y;

                _inputVector = new Vector2(pos.x * 2, pos.y * 2);
                _inputVector = (_inputVector.magnitude > 1.0f) ? _inputVector.normalized : _inputVector;

                _gamepadCenter.anchoredPosition = new Vector2(_inputVector.x * (_gamepadBackground.sizeDelta.x * 0.3f), _inputVector.y * (_gamepadBackground.sizeDelta.y * 0.3f));
            }
        }

        public void OnPointerDown(PointerEventData eventData) => OnDrag(eventData);

        public void OnPointerUp(PointerEventData eventData)
        {
            _inputVector = Vector2.zero;
            _gamepadCenter.anchoredPosition = _inputVector;
        }

        public Vector3 GetGamepadVector() => new Vector3(_inputVector.x, 0, _inputVector.y) * 0.05f;

        #endregion

        #region Private

        private void Start()
        {
            _gamepadBackground = GetComponent<RectTransform>();
            _gamepadCenter = _gamepadBackground.GetChild(0).GetComponent<RectTransform>();
        }

        #endregion
    }
}
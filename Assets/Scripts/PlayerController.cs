using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 _mouseCoord;
    private Rigidbody2D _rigidbody;
    private SceneController _sceneController;

    private void Start() {
        _rigidbody = GetComponent<Rigidbody2D>();
        _sceneController = FindObjectOfType<SceneController>();
    }

    private void Update()
    {
        if (_sceneController.isPlayGame)
        {
            if (Input.GetMouseButtonDown(0)) _mouseCoord = Input.mousePosition;
            if (Input.GetMouseButtonUp(0)) _rigidbody.AddForce((Input.mousePosition - _mouseCoord) / 2);
        }
    }
}
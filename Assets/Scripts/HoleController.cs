using UnityEngine;
using UnityEngine.UI;

public class HoleController : MonoBehaviour
{
    [HideInInspector] public bool isActive;
    [HideInInspector] public Color thisColor;

    private int _holeId;
    private SceneController _sceneController;

    public void CreateHole(int holeId, Color holeColor, SceneController sceneController)
    {
        isActive = true;
        _holeId = holeId;
        thisColor = holeColor;
        GetComponent<Image>().color = holeColor;
        _sceneController = sceneController;
        EnableHole(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player") return;

        DisableHole(collision.GetComponent<Image>().color == thisColor);
    }

    public void DisableHole(bool isWin)
    {
        _sceneController.ReactivatePlayer(_holeId, isWin);
        
        if (isWin)
        {
            EnableHole(false);
            _sceneController.AddScore();
        }
    }

    public void EnableHole(bool isEnable)
    {
        isActive = isEnable;
        GetComponent<CircleCollider2D>().enabled = isEnable;
        GetComponent<Image>().enabled = isEnable;
    }
}
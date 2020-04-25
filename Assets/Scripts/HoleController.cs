using System.Collections;
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
        transform.localScale = Vector3.one * 0.7f;
        StartCoroutine(EnableHole(true));
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
            StartCoroutine(EnableHole(false));
            _sceneController.AddScore();
        }
    }

    public IEnumerator EnableHole(bool isEnable)
    {
        if (!isEnable && !isActive)
        {
            GetComponent<Animation>().Play();
            yield return new WaitForSecondsRealtime(0.15f);
        }
        isActive = isEnable;
        GetComponent<CircleCollider2D>().enabled = isEnable;
        GetComponent<Image>().enabled = isEnable;
        yield return new WaitForSecondsRealtime(0.05f);
    }
}
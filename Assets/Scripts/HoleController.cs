using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoleController : MonoBehaviour
{
    [HideInInspector] public bool isActive;
    [HideInInspector] public Color thisColor;

    public void CreateHole(Color holeColor)
    {
        // Creating hole, set color and activate

        isActive = true;
        thisColor = holeColor;
        GetComponent<Image>().color = holeColor;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player") return;

        DisableHole(collision.GetComponent<Image>().color == thisColor);
    }

    public void DisableHole(bool isWin)
    {
        isActive = isWin;
        if (isWin)
        {
            GetComponent<CircleCollider2D>().enabled = false;
            GetComponent<Image>().enabled = false;
        }
    }
}
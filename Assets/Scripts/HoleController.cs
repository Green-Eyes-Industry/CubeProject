using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleController : MonoBehaviour
{
    [HideInInspector] public bool isActive;

    public void CreateHole()
    {
        // Creating hole, set color and activate

        isActive = true;
    }

    public void DisableHole(bool isWin)
    {

    }
}
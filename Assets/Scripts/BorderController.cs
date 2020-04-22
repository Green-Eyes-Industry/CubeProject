﻿using UnityEngine;

public class BorderController : MonoBehaviour
{
    private SceneController _sceneController;

    private void Start()
    {
        _sceneController = FindObjectOfType<SceneController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _sceneController.ReactivatePlayer();
    }
}
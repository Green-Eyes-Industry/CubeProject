using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 _mouseCoord;
    private Rigidbody2D _rigidbody;

    private void Start() => _rigidbody = GetComponent<Rigidbody2D>();

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) _mouseCoord = Input.mousePosition;

        if (Input.GetMouseButtonUp(0)) _rigidbody.AddForce((Input.mousePosition - _mouseCoord) / 2);
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using static System.Math;


public class Player : MonoBehaviour
{

    protected Vector2 _playerMove;

    public Collider2D _boxCollider2D;

    protected Rigidbody2D _rigidbody2D;

    //safeState
    public bool safe;

    public joystickScript _joystick;
    public float rotationSpeed = 720.0f;
    public static float velocity = 7.0f;

    protected virtual void Awake()
    {
        // get all required components
        _boxCollider2D = GetComponent<Collider2D>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    protected virtual void Move(Vector2 input)
    {
        //specification of player movement vector
        _playerMove = new Vector2(input.x, input.y);
        float inputMagnitude = Mathf.Clamp01(_playerMove.magnitude);


        // some Physical stuff
        if (_playerMove != Vector2.zero)
        {
            Quaternion rotateDirection = Quaternion.LookRotation(Vector3.forward, _playerMove);
            transform.rotation =
                Quaternion.RotateTowards(transform.rotation, rotateDirection, Time.deltaTime * rotationSpeed);
        }

        _rigidbody2D.AddForce(_playerMove * inputMagnitude);
    }

}
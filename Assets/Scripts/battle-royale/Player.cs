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
    protected RaycastHit2D hit;

    public joystickScript _joystick;
    public float rotationSpeed = 720.0f;
    public static float velocity = 7.0f;

    public LayerMask filterMask;
    private Collider2D checkCollider;
    public float scanRadius = 3f;

    //weapon section
    private bool weapon = true;
    private GameObject _weapon;
    
    protected virtual void Awake()
    {
        // get all required components
        _boxCollider2D = GetComponent<Collider2D>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _weapon = GameObject.FindWithTag("Weapon");
    }

    protected virtual void Move(Vector2 input)
    {
        //specification of player movement vector
        _playerMove = new Vector2(input.x, input.y);
        float inputMagnitude = Mathf.Clamp01(_playerMove.magnitude);
        
        
        // some Physical stuff
        if (_playerMove != Vector2.zero)
        {
            Quaternion rotateDirection= Quaternion.LookRotation(Vector3.forward, _playerMove);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotateDirection, Time.deltaTime * rotationSpeed);
        }

        _rigidbody2D.transform.Translate(new Vector3(_playerMove.x * Time.deltaTime * inputMagnitude, _playerMove.y * Time.deltaTime * inputMagnitude, 0), Space.World);
        // _rigidbody2D.AddForce(_playerMove * inputMagnitude);
    }

    protected virtual void FixedUpdate()
    {
        Vector2 input = _joystick.getValue() * velocity;
        Move(new Vector2(input.x, input.y));
    }

    protected virtual void GetWeapon()
    {
        weapon = true;
        _weapon.gameObject.SetActive(true);
    }

    protected virtual void ThrowWeapon()
    {
        weapon = false;
        _weapon.gameObject.SetActive(false);
    }
    
}



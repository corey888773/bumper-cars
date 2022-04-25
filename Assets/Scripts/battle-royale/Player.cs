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
    public bool WeaponPicked;

    protected bool gunPicked;

    protected bool sniperRifflePicked;

    protected bool shotgunPicked;
    //safeState
    public bool safe;

    public joystickScript _joystick;
    public float rotationSpeed = 720.0f;
    public static float velocity = 7.0f;

    private Collider2D checkCollider;
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
            Quaternion rotateDirection= Quaternion.LookRotation(Vector3.forward, _playerMove);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotateDirection, Time.deltaTime * rotationSpeed);
        }
        
        _rigidbody2D.AddForce(_playerMove * inputMagnitude);
    }
    
    // function to add effects after stepping on objects like weapons etc.
    protected void AddEffect(WeaponType type)
    {
        switch (type)
        {
            case WeaponType.Gun:
                print ("Gun");
                WeaponPicked = true;
                gunPicked = true;
                break;
            case WeaponType.Shotgun:
                print("Shotgun");
                WeaponPicked = true;
                shotgunPicked = true;
                break;
            case WeaponType.SniperRifle:
                print("SniperRifle");
                WeaponPicked = true;
                sniperRifflePicked = true;
                break;
            default:
                Debug.Log("no effect implemented");
                break;
        }
    }

    protected virtual void FixedUpdate()
    {
        Vector2 input = _joystick.getValue() * velocity;
        Move(new Vector2(input.x, input.y));

        if (gunPicked)
        {
            gunPicked = false;
            WeaponPicked = false;
        }

        if (shotgunPicked)
        {
            shotgunPicked = false;
            WeaponPicked = false;
        }

        if (sniperRifflePicked)
        {
            sniperRifflePicked = false;
            WeaponPicked = false;
        }
    }

}



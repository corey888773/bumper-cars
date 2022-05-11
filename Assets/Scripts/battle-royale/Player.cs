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

    public LayerMask filterMask;
    private Collider2D checkCollider;
    public float scanRadius = 3f;

    //weapon section
    private bool weapon = true;
    private GameObject _weapon;
    private bool knock;
    private float knockTime;
    
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
        if(!knock)
            Move(new Vector2(input.x, input.y));
        
        DisableMove();

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

    public void KnockBack(Vector2 force, bool torque = false)
    {
        _rigidbody2D.AddForce(-1*force);
        if(torque)
            _rigidbody2D.AddTorque(200f);
       
        knock = true;
        knockTime = Time.time;
    }

    private void DisableMove()
    {
        if (!knock)
            return;
        if (Time.time - knockTime > 0.5f)
            knock = false;
    }
}



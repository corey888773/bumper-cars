using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using static System.Math;
using Random = UnityEngine.Random;


public class Player : MonoBehaviour
{
     
    private Vector2 _playerMove;
    public Collider2D _boxCollider2D;
    private Rigidbody2D _rigidbody2D;
    public WeaponType WeaponPicked;
    public joystickScript _joystick;
    
    public float rotationSpeed = 720.0f;
    public static float velocity = 7.0f;
    private bool isAlive;
    private Collider2D checkCollider;

    //weapon section
    private bool weapon = true;
    private bool knock;
    private float knockTime;
    
    public Shotgun _Shotgun;
    public SniperRifle _SniperRiffle;
    protected virtual void Awake()
    {
        // get all required components
        _boxCollider2D = GetComponent<Collider2D>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        isAlive = true;
        WeaponPicked = WeaponType.None;
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
    }

    public WeaponType WeaponIsPicked()
    {
        return WeaponPicked;
    }
    
    // function to add effects after stepping on objects like weapons etc.
    protected void GetWeapon(WeaponType type)
    {
        WeaponPicked = type;
        switch (type)
        {
            case WeaponType.Gun:
                break;
            case WeaponType.Shotgun:
                _Shotgun.Activate(true);
                break;
            case WeaponType.SniperRifle:
                _SniperRiffle.Activate(true);
                break;
            default:
                Debug.Log("no effect implemented");
                break;
        }
    }
    
    IEnumerator HideAndShow(GameObject go, float delay, int counter)
    {
        go.transform.position = new Vector3(int.MaxValue, int.MaxValue, int.MaxValue);
        yield return new WaitForSeconds(delay);
        transform.position = GameManager.instance.spawnPositions[counter].transform.position;
        GameManager.instance.spawnPositions[counter].SpawnAvailable = false;
    }

    protected virtual void FixedUpdate()
    {
        if (Input.GetButtonDown("Fire1")) isAlive = false;
        
        Vector2 input = _joystick.getValue() * velocity;
        if(!knock)
            Move(new Vector2(input.x, input.y));
        
        DisableMove();

        if (!isAlive)
        {
            WeaponPicked = WeaponType.None;
            ThrowWeapon(WeaponType.Shotgun);
            ThrowWeapon(WeaponType.SniperRifle);
            var random = new System.Random();
            int choice = random.Next(GameManager.instance.spawnPositions.Count);
            while (!GameManager.instance.spawnPositions[choice].SpawnAvailable)
            {
                choice = random.Next(GameManager.instance.spawnPositions.Count);
            }
            StartCoroutine(HideAndShow(this.GameObject(), 0.5f, choice));
            isAlive = true;
        }
    }
    public virtual void ThrowWeapon(WeaponType type)
    {
        WeaponPicked = WeaponType.None;
        switch (type)
        {
            case WeaponType.Shotgun:
                _Shotgun.Activate(false);
                break;
            case WeaponType.SniperRifle:
                _SniperRiffle.Activate(false);
                break;
        }
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



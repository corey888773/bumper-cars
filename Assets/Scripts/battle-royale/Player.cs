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
     
    protected Vector2 _playerMove;
    
    public Collider2D _boxCollider2D;
    protected Rigidbody2D _rigidbody2D;
    public static bool WeaponPicked;

    public static bool gunPicked;

    public static bool sniperRifflePicked;

    public static bool shotgunPicked = true;
    //safeState
    public bool safe;
    protected bool isAlive;

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
    
    public GameObject _Shotgun;
    public GameObject _SniperRiffle;
    protected virtual void Awake()
    {
        // get all required components
        _boxCollider2D = GetComponent<Collider2D>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _weapon = GameObject.FindWithTag("Weapon");
        isAlive = true;
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
    
    IEnumerator HideAndShow(GameObject go, float delay, int counter)
    {
        go.transform.position = new Vector3(int.MaxValue, int.MaxValue, int.MaxValue);
        yield return new WaitForSeconds(delay);
        transform.position = GameManager.instance.spawnPositions[counter].transform.position;
        GameManager.instance.spawnPositions[counter].SpawnAvailable = false;
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

        // if (shotgunPicked)
        // {
        //     _Shotgun.SetActive(true);
        //     if (Input.GetButtonDown("Jump"))
        //     {
        //         shotgunPicked = false;
        //         WeaponPicked = false;
        //         // _Shotgun.SetActive(false);
        //     }
        // }

        if (sniperRifflePicked)
        {
            _SniperRiffle.SetActive(true);
            if (Input.GetButtonDown("Jump"))
            {
                sniperRifflePicked = false;
                WeaponPicked = false;
                _SniperRiffle.SetActive(false);
            }
        }

        if (!isAlive)
        {
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

    public virtual void GetWeapon()
    {
        weapon = true;
        _weapon.gameObject.SetActive(true);
    }

    public virtual void ThrowWeapon(WeaponType type)
    {
        weapon = false;

        switch (type)
        {
            case WeaponType.Shotgun:
                _Shotgun.SetActive(false);
                break;
            case WeaponType.SniperRifle:
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



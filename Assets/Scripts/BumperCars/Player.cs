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
    
    //Boosts
    public float BoostValue = 2.0f;
    protected float Duration = 5.0f;
    public static bool BoostPicked;
    
    protected bool activateSpeed;
    protected float speedActivateTime;

    protected bool activateMass;
    protected float massActivateTime;
    
    protected bool activateInverse;
    protected float inverseActivateTime;
    
    protected bool activateFreeze;
    protected float freezeActivateTime;
    
    //safeState
    public bool safe;

    public joystickScript _joystick;
    public float rotationSpeed = 720.0f;
    public static float velocity = 7.0f;
    public static float StartingVelocity;
    public static float StartingMass;
    
    
    public LayerMask filterMask;
    private HoleManager _holeManager; 
    private Collider2D checkCollider;
    public float scanRadius = 3f;
    
    protected virtual void Awake()
    {
        // get all required components
        _boxCollider2D = GetComponent<Collider2D>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _holeManager = FindObjectOfType<HoleManager>();
        
        StartingVelocity = velocity;
        StartingMass = _rigidbody2D.mass;
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
    
    
    //this is the function which prevents objects from spawning on top of player or within its short radius
    protected void CheckForObjectsCollisions()
    {
        checkCollider = Physics2D.OverlapCircle(transform.position, scanRadius, filterMask);
        if (checkCollider != null && checkCollider.transform != transform)
        {
            Destroy(checkCollider.gameObject);
            _holeManager.countDelta = 0;
            _holeManager.holeCount--;
        }
    }
    
    // function to spectate scan radius in unity editor
    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, scanRadius);
    }

    // function to add effects after stepping on objects like boosts, holes, etc.
    protected void AddEffect(EffectType type)
    {
        switch (type)
        {
            case EffectType.Hole:
                _rigidbody2D.velocity /= 1.3f;
                _rigidbody2D.angularDrag = 0;
                break;
            case EffectType.Speed:
                if (velocity <= StartingVelocity)
                {
                    speedActivateTime = Time.time;
                    activateSpeed = true;
                    BoostPicked = true;
                    if (activateSpeed)
                    {
                        velocity *= BoostValue;
                    }
                }
                break;
            case EffectType.Mass:
                if (_rigidbody2D.mass <= StartingMass)
                {
                    massActivateTime = Time.time;
                    activateMass = true;
                    BoostPicked = true;
                    if (activateMass)
                    {
                        _rigidbody2D.mass *= BoostValue;
                        velocity *= BoostValue;
                    }
                }
                break;
            case EffectType.Freeze:
                freezeActivateTime = Time.time;
                activateFreeze = true;
                BoostPicked = true;
                if (activateFreeze)
                {
                    _rigidbody2D.velocity /= float.MaxValue;
                    _rigidbody2D.angularDrag = 0;
                    _rigidbody2D.mass = float.MaxValue;
                }
                break;
            case EffectType.Inverse:
                inverseActivateTime = Time.time;
                activateInverse = true;
                BoostPicked = true;
                if (activateInverse)
                {
                    
                }
                break;
            case EffectType.Safe:
                safe = true;

                break;
            default:
                Debug.Log("no effect implemented");
                break;
        }
    }

    
    
    protected virtual void FixedUpdate()
    {
        if (!activateInverse)
        {
            Vector2 input = _joystick.getValue() * velocity;
            Move(new Vector2(input.x, input.y));
        }
        CheckForObjectsCollisions();

        if (activateSpeed)
        {
            if (Time.time - speedActivateTime > Duration)
            {
                velocity /= BoostValue;
                activateSpeed = false;
                BoostPicked = false;
            }
        }

        if (activateMass)
        {
            if (Time.time - massActivateTime > Duration)
            {
                _rigidbody2D.mass /= BoostValue;
                velocity /= BoostValue;
                activateMass = false;
                BoostPicked = false;
            }
        }
        
        if (activateFreeze)
        {
            if (Time.time - freezeActivateTime > Duration)
            {
                _rigidbody2D.mass = StartingMass;
                velocity = StartingVelocity;
                activateFreeze = false;
                BoostPicked = false;
            }
        }
        
        if (activateInverse)
        {
            Vector2 input = _joystick.getValue() * velocity;
            Move(new Vector2(input.x * -1, input.y * -1));
            if (Time.time - inverseActivateTime > Duration)
            {
                activateInverse = false;
                BoostPicked = false;
            }   
        }
    }
}



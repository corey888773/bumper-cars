using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Math;


public class Player : MonoBehaviour
{
     
    protected Vector2 _playerMove;
    
    protected BoxCollider2D _boxCollider2D;
    protected Rigidbody2D _rigidbody2D;
    protected RaycastHit2D hit;
    

    public joystickScript _joystick;
    public float rotationSpeed = 720.0f;
    public static float velocity = 10.0f;
    public static float StartingVelocity;
    public static float StartingMass;
    
    
    public LayerMask filterMask;
    private HoleManager _holeManager; 
    private Collider2D checkCollider;
    public float scanRadius = 3f;
    
    protected virtual void Awake()
    {
        // get all required components
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _holeManager = FindObjectOfType<HoleManager>();
        
        StartingVelocity = velocity;
        StartingMass = GameObject.Find("Player").GetComponent<Rigidbody2D>().mass;
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
    protected void CheckForObjectsCollisons()
    {
        checkCollider = Physics2D.OverlapCircle(transform.position, scanRadius, filterMask);
        if (checkCollider != null && checkCollider.transform != transform)
        {
            Destroy(checkCollider.gameObject);
            _holeManager.countDelta = 0;
            _holeManager.holeCount--;
        }
    }
    
    // function to spectate scan radius in unity ecitor
    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, scanRadius);
    }
    
    protected virtual void FixedUpdate()
    {
        Vector2 input = _joystick.getValue() * velocity;
        float y = Input.GetAxisRaw("Vertical");
        float x = Input.GetAxisRaw("Horizontal");
        Move(new Vector2(input.x, input.y));
    }
}

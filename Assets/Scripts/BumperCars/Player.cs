using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Math;


public class Player : MonoBehaviour
{
     
    protected Vector2 _playerMove;

    private BoxCollider2D _boxCollider2D;
    protected RaycastHit2D hit;

    private joystickScript _joystick;
    
    public float _rotationSpeed = 720.0f;
    public float _velocity = 10.0f;
    
    private void Awake()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _joystick = GameObject.Find("JoystickCircleDirection").GetComponent<joystickScript>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Move(Vector2 input)
    {
        float degrees = (float) Atan2(input.x, input.y);
        _playerMove = new Vector2(input.x, input.y);
        float inputMagnitude = Mathf.Clamp01(_playerMove.magnitude);
        // transform.Translate(_playerMove * Time.deltaTime, Space.World);


        if (_playerMove != Vector2.zero)
        {
            Quaternion rotateDirection= Quaternion.LookRotation(Vector3.forward, _playerMove);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotateDirection, Time.deltaTime * _rotationSpeed);
        }
        
        
        // move collision test. box creates a box on top of our object which is moved slightly into the movement direction before the player.
        //the box tests if it covers any colliders, and if so, it stops the movement.
        hit = Physics2D.BoxCast(transform.position, _boxCollider2D.size,0, new Vector2(0,_playerMove.y),
            Mathf.Abs(_playerMove.y * Time.deltaTime ), LayerMask.GetMask("Blocking"));
        if (hit.collider == null)
        {
            transform.Translate(new Vector3(0, _playerMove.y * Time.deltaTime * inputMagnitude, 0), Space.World);
        }
        hit = Physics2D.BoxCast(transform.position, _boxCollider2D.size, 0, new Vector2(_playerMove.x, 0),
            Mathf.Abs(_playerMove.x * Time.deltaTime ), LayerMask.GetMask("Blocking"));
        if (hit.collider == null)
        {
            transform.Translate(new Vector3(_playerMove.x * Time.deltaTime * inputMagnitude,0 , 0), Space.World);
        }


    }
    
    private void FixedUpdate()
    {
        Vector2 input = _joystick.getValue() * _velocity;
        float y = Input.GetAxisRaw("Vertical");
        float x = Input.GetAxisRaw("Horizontal");
        Move(new Vector2(input.x, input.y));
    }
}

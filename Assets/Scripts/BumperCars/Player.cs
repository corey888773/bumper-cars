using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Math;


public class Player : MonoBehaviour
{
     
    public static Vector2 PlayerMove;
    
    protected BoxCollider2D _boxCollider2D;
    public Rigidbody2D rigidbody2D;
    protected RaycastHit2D hit;

    public joystickScript _joystick;
    public float _rotationSpeed = 720.0f;
    public static float _velocity = 7.0f;
    
    protected virtual void Awake()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        // _joystick = GameObject.Find("JoystickCircleDirection").GetComponent<joystickScript>();
    }

    protected virtual void Move(Vector2 input)
    {
        float degrees = (float) Atan2(input.x, input.y);
        PlayerMove = new Vector2(input.x, input.y);
        
        float inputMagnitude = Mathf.Clamp01(PlayerMove.magnitude);
        // transform.Translate(_playerMove * Time.deltaTime, Space.World);


        if (PlayerMove != Vector2.zero)
        {
            Quaternion rotateDirection= Quaternion.LookRotation(Vector3.forward, PlayerMove);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotateDirection, Time.deltaTime * _rotationSpeed);
        }
        
        rigidbody2D.AddForce(PlayerMove * inputMagnitude);
        
        // // move collision test. box creates a box on top of our object which is moved slightly into the movement direction before the player.
        // //the box tests if it covers any colliders, and if so, it stops the movement.
        // hit = Physics2D.BoxCast(transform.position, _boxCollider2D.size,0, new Vector2(0,_playerMove.y),
        //     Mathf.Abs(_playerMove.y * Time.deltaTime ), LayerMask.GetMask("Blocking"));
        // if (hit.collider == null)
        // {
        //     transform.Translate(new Vector3(0, _playerMove.y * Time.deltaTime * inputMagnitude, 0), Space.World);
        // }
        // hit = Physics2D.BoxCast(transform.position, _boxCollider2D.size, 0, new Vector2(_playerMove.x, 0),
        //     Mathf.Abs(_playerMove.x * Time.deltaTime ), LayerMask.GetMask("Blocking"));
        // if (hit.collider == null)
        // {
        //     transform.Translate(new Vector3(_playerMove.x * Time.deltaTime * inputMagnitude,0 , 0), Space.World);
        // }
            
        // transform.Translate(_playerMove * Time.deltaTime * inputMagnitude, Space.World);
        
        
    }
    
    protected virtual void FixedUpdate()
    {
        Vector2 input = _joystick.getValue() * _velocity;
        float y = Input.GetAxisRaw("Vertical");
        float x = Input.GetAxisRaw("Horizontal");
        Move(new Vector2(input.x, input.y));
    }
}

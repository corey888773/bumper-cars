using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Math;


public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 10.0f; 
    protected Vector2 _playerMove;

    private BoxCollider2D _boxCollider2D;

    private joystickScript _joystick;
    
    public float rotationSpeed = 720.0f;

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
        transform.Translate(_playerMove * Time.deltaTime, Space.World);


        if (_playerMove != Vector2.zero)
        {
            Quaternion rotateDirection= Quaternion.LookRotation(Vector3.forward, _playerMove);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotateDirection, Time.deltaTime * rotationSpeed);
        }


    }
    
    private void FixedUpdate()
    {
        Vector2 input = _joystick.getValue() * speed;
        float y = Input.GetAxisRaw("Vertical");
        float x = Input.GetAxisRaw("Horizontal");
        Move(new Vector2(input.x, input.y));
    }
}

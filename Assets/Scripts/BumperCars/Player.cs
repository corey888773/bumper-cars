using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Math;

public class Player : MonoBehaviour
{
    protected Vector3 _playerMove;

    private BoxCollider2D _boxCollider2D;
    
    

    public float Velocity = 1.0f;

    private void Awake()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Move(Vector3 input)
    {
        float degrees = (float) Atan2(input.x, input.y);
        _playerMove = new Vector3(input.x, input.y, 0).normalized * Velocity;
        transform.Translate(_playerMove);
        // transform.rotation = Quaternion.Euler(new Vector3(0, 0, degrees));



    }
    
    private void FixedUpdate()
    {
        float y = Input.GetAxisRaw("Vertical");
        float x = Input.GetAxisRaw("Horizontal");
        Move(new Vector3(x, y, 0));
    }
}

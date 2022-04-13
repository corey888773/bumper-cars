using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using BumperCars;
using UnityEngine;

public class Hole : MonoBehaviour
{
    public float scanRadius = 3f;
    public LayerMask filterMask;
    private float spawnTime;
    private bool confirmed;
    private HoleManager _holeManager; 
    private Collider2D _checkCollider;
    private SpriteRenderer _spriteRenderer;
    private float countDown;
    public float duration = 5f;
    private bool savingEnabled = true;
    
    public static float red;
    public static float green;
    public static float blue;


    void Awake()
    {
        // get all required components
        _holeManager = FindObjectOfType<HoleManager>();
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _spriteRenderer.enabled = false;
        
        // note time of object spawn
        spawnTime = Time.time;
    }
    void Update()
    {
        // checks if object doesn't overlaps any other objects of specified layer
        _checkCollider = Physics2D.OverlapCircle(transform.position, scanRadius, filterMask);
        if (_checkCollider != null && _checkCollider.transform != transform)
        {
            Destroy(_checkCollider.gameObject);
            _holeManager.countDelta = 0;
            _holeManager.holeCount--;
        }
        
        // confirm hole after 1 second of existence
        if (Time.time - spawnTime > 1f && !confirmed)
            ConfirmHole();

        // destroy hole after couple seconds
        if (confirmed && Time.time - countDown > duration)
        {
            Destroy(gameObject);
        }
        
    }
    
    //function to confirm hole existence. changes layer to prevent Player erasing this objects and enables sprite
    private void ConfirmHole()
    {
        {
            gameObject.layer = 7;
            _spriteRenderer.enabled = true;
            GameManager.instance.ShowText("", 50,
                new Color(red, green, blue), transform.position, Vector3.zero, duration, TextTypes.Timer, "BOOM");
            countDown = Time.time;
            confirmed = true;
            
        } 
    }

    
    
    
    //function which activates when stepping on object. Requires collider to be set on Trigger Mode
    private void OnTriggerStay2D(Collider2D collision)
    {
        collision.SendMessage("AddEffect",EffectType.Hole);
        if (confirmed && Time.time - countDown > duration - 1 && savingEnabled)
        {
            collision.SendMessage( "AddEffect", EffectType.Safe);
            savingEnabled = false;
        }
        
    }
    
    //function to spectate scan radius in unity editor
    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, scanRadius);
    }
    
    
    
    
}

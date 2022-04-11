using System;
using System.Collections;
using System.Collections.Generic;
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
    private Player _player;
    
    
    void Awake()
    {
        // get all required components
        _holeManager = FindObjectOfType<HoleManager>();
        _player = FindObjectOfType<Player>();
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
        
        
    }
    
    //function to confirm hole existence. changes layer to prevent Player erasing this objects and enables sprite
    private void ConfirmHole()
    {
        {
            gameObject.layer = 7;
            _spriteRenderer.enabled = true;
            confirmed = true;
            GameManager.instance.ShowText("confirmed",50,Color.red,transform.position,Vector3.up * Time.deltaTime, 1f);
        } 
    }
    
    //function which activates when stepping on object. Requires collider to be set on Trigger Mode
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.SendMessage("AddEffect",EffectType.Hole);
    }
    
    //function to spectate scan radius in unity editor
    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, scanRadius);
    }
    
    
}


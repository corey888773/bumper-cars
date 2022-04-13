using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : MonoBehaviour
{
    private float spawnTime;
    private bool confirmed;
    private BoostManagerv2 _boostManager;
    private Collider2D _checkCollider;
    private SpriteRenderer _spriteRenderer;
    protected int boostPicker;

    void Awake()
    {
        _boostManager = FindObjectOfType<BoostManagerv2>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        // _spriteRenderer.enabled = false;
        spawnTime = Time.time;
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("effect not implemented");
    }

    protected void update()
    {
        // confirm hole after 1 second of existence
        if (Time.time - spawnTime > 1f && !confirmed)
            ConfirmBoost();
    }
    private void ConfirmBoost()
    {
        {
            // gameObject.layer = 7;
            // _spriteRenderer.enabled = true;
            confirmed = true;
            
        } 
    }
}



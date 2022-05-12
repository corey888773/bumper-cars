using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pellet : MonoBehaviour
{
    public float duration = 1f;
    private float instaniateTime;
    private void Start()
    {
        instaniateTime = Time.time;
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if(!collision.gameObject.CompareTag("Pellet") && !collision.gameObject.CompareTag("Player"))
            Destroy(gameObject);
    }

    private void Update()
    {
        if (Time.time - instaniateTime > duration)
        {
            Destroy(gameObject);
        }
    }
}


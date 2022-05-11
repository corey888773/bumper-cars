using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pellet : MonoBehaviour
{
    
    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if(!collision.gameObject.CompareTag("Pellet") && !collision.gameObject.CompareTag("Player"))
            Destroy(gameObject);
    }
}


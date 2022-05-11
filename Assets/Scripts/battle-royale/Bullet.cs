using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int bounces = 2;
    
    protected void OnCollisionEnter2D(Collision2D collision)
    {
        bounces--;
        if(bounces > 0)
            return;

        if(!collision.gameObject.CompareTag("Bullet") && !collision.gameObject.CompareTag("Player"))
            Destroy(gameObject);
    }
}

using System;
using Unity.Mathematics;
using UnityEngine;

namespace BumperCars
{
    public class MassManager : BoostManager
    {
        private Rigidbody2D rb;
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                rb = GameObject.Find("Player").GetComponent<Rigidbody2D>();
                if (rb.mass <= 3.0f)
                {
                    rb.mass *= 5.0f;
                }
                
            }
            Destroy(gameObject);
            BoostsCounter--;

        }
    }
}
using System;
using Unity.Mathematics;
using UnityEngine;

namespace BumperCars
{
    public class FreezeManager : BoostManager
    {
        private Rigidbody2D rb;
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                rb = GameObject.Find("Player").GetComponent<Rigidbody2D>();
                rb.Sleep();
                rb.mass = Int32.MaxValue;
            }
            Destroy(gameObject);
            BoostsCounter--;

        }
    }
}
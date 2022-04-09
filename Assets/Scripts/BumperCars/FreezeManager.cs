using System;
using Unity.Mathematics;
using UnityEngine;

namespace BumperCars
{
    public class FreezeManager : BoostManager
    {
        public static bool boosting = false;
        public static float boostTime = 0.0f;
        private Rigidbody2D rb;
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                rb = GameObject.Find("Player").GetComponent<Rigidbody2D>();
                rb.Sleep();
                rb.mass = Int32.MaxValue;
                boosting = true;
            }
            Destroy(gameObject);
            BoostsCounter--;

        }
    }
}
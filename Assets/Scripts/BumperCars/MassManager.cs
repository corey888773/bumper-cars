using System;
using Unity.Mathematics;
using UnityEngine;

namespace BumperCars
{
    public class MassManager : BoostManager
    {
        private Rigidbody2D rb;
        public static bool boosting = false;
        public static float boostTime = 0.0f;
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                rb = GameObject.Find("Player").GetComponent<Rigidbody2D>();
                if (rb.mass <= 2 * Player.StartingMass)
                {
                    rb.mass *= 7.0f;
                    boosting = true;
                }
                
            }
            Destroy(gameObject);
            BoostsCounter--;

        }
    }
}
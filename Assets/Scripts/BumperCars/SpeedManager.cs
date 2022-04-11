using System.Collections;
using UnityEngine;

namespace BumperCars
{
    public class SpeedManager : BoostManager
    {
        public static bool boosting = false;
        public static float boostTime = 0.0f;


        // IEnumerator VelocityCoroutine()
        // {
        //     Debug.Log("Velocity Up");
        //     yield return new WaitForSeconds(3);
        //     Debug.Log("Velocity Down");
        // }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                if (Player.velocity < Player.StartingVelocity + 1)
                {
                    // StartCoroutine(VelocityCoroutine());
                    Player.velocity += 4;
                    boosting = true;
                }
            }
            Destroy(gameObject);
            BoostsCounter--;
        }
    }
}
using System.Collections;
using UnityEngine;

namespace BumperCars
{
    public class SpeedManager : BoostManager
    {


        // IEnumerator VelocityCoroutine()
        // {
        //     Debug.Log("Velocity Up");
        //     yield return new WaitForSeconds(3);
        //     Debug.Log("Velocity Up");
        // }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                if (Player._velocity < 8)
                {
                    // StartCoroutine(VelocityCoroutine());
                    Player._velocity += 1;
                }
            }
            Destroy(gameObject);
            BoostsCounter--;

            // else if (collision.tag == "ComputerPlayer")
            // {
            //     if (ComputerPlayer._velocity < 14)
            //     {
            //         ComputerPlayer._velocity += 1;
            //         Destroy(gameObject);
            //         BoostsCounter--;
            //     }
            // }
        }
    }
}
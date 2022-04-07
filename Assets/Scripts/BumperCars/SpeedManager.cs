using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class SpeedManager : BoostManager
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (Player._velocity < 14)
            {
                Player._velocity += 1;
                Destroy(gameObject);
                BoostManager.BoostsCounter--;
            }
            else
            {
                Destroy(gameObject);
                BoostManager.BoostsCounter--;
            }
            
        }

        else if (collision.tag == "ComputerPlayer")
        {
            if (ComputerPlayer._velocity < 14)
            {
                ComputerPlayer._velocity += 1;
                Destroy(gameObject);
                BoostManager.BoostsCounter--;
            }
            else
            {
                Destroy(gameObject);
                BoostManager.BoostsCounter--;
            }
        }
    }
}
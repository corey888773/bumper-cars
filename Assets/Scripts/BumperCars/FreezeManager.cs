using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.Mathematics;
using UnityEngine;
using static System.Math;

public class FreezeManager : BoostManager
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Destroy(gameObject);
            BoostManager.BoostsCounter--;
        }
        else if (collision.tag == "ComputerPlayer")
        {
            ComputerPlayer._velocity += 1;
            Destroy(gameObject);
            BoostManager.BoostsCounter--;
        }
    }
}
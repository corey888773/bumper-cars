using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : Boost
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        
        if (!Player.BoostPicked)
        {
            collision.SendMessage("AddEffect", EffectType.Speed);
            Destroy(gameObject);
            BoostManagerv2.boostCount -= 1;
        }
    }
}

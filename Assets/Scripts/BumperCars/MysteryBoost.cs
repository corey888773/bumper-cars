using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class MysteryBoost : Boost
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("PlayerUnsafe") || !collision.CompareTag("PlayerSafe"))
            return;
        if (!Player.BoostPicked)
        {
            boostPicker = UnityEngine.Random.Range(0, 4);
            switch (boostPicker)
            {
                case 0:
                    collision.SendMessage("AddEffect", EffectType.Freeze);
                    break;
                case 1:
                    collision.SendMessage("AddEffect", EffectType.Mass);
                    break;
                case 2:
                    collision.SendMessage("AddEffect", EffectType.Speed);
                    break;
                case 3:
                    collision.SendMessage("AddEffect",EffectType.Inverse);
            }
            Destroy(gameObject);
            BoostManagerv2.boostCount -= 1;
        }
        
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class MysteryBoost : Boost
{
    protected override void Awake()
    {
        base.Awake();
        _effectType = EffectType.Freeze;
        boostPicker = UnityEngine.Random.Range(0, 4);
        switch (boostPicker)
        {
            case 0:
                _effectType = EffectType.Freeze;
                break;
            case 1:
                _effectType = EffectType.Mass;
                break;
            case 2:
                _effectType = EffectType.Speed;
                break;
            case 3:
                _effectType = EffectType.Inverse;
                break;
        }
    }
}
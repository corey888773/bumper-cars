using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class MassBoost : Boost
{
    protected override void Awake()
    {
        base.Awake();
        _effectType = EffectType.Mass;
    }
}
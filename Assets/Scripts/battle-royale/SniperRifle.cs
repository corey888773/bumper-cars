using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class SniperRifle : Weapon
{
    protected override void Awake()
    {
        base.Awake();
        _weaponType = WeaponType.SniperRifle;
    }
}
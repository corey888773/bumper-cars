using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShotgun : Weapon
{
    protected override void Awake()
    {
        base.Awake();
        _weaponType = WeaponType.Gun;
    }
}

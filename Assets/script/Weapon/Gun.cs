using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : WeaponBase
{
    protected override void Start()
    {
        base.Start(); 
    }

    public override void Shoot() // Измените уровень доступа на public
    {
        base.Shoot();
        // Здесь можно добавить уникальную логику для пистолета
    }
}

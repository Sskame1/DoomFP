using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : WeaponBase
{
    protected override void Start()
    {
        base.Start(); 
    }

    public override void FireBullet() // Измените уровень доступа на public
    {
        base.FireBullet();
        // Здесь можно добавить уникальную логику для пистолета
    }
}

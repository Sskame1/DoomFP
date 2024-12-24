using UnityEngine;

public class Pistol : WeaponBase
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

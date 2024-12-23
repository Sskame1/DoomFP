using System.Collections.Generic;
using UnityEngine;

// Типы оружия
public enum WeaponType
{
    Melee,
    Ranged,
    Firearm
}

// Статистика оружия
[System.Serializable]
public class WeaponStats
{
    public string weaponName;
    public WeaponType type;
    public int ammoCount;
    public int damage;
    public float fireRate;
    public float range;
    public GameObject bulletPrefab;

    public WeaponStats(string weaponName, WeaponType type, int ammoCount, int damage, float fireRate, float range, GameObject bulletPrefab,bool inInventory)
    {
        this.weaponName = weaponName;
        this.type = type;
        this.ammoCount = ammoCount;
        this.damage = damage;
        this.fireRate = fireRate;
        this.range = range;
        this.bulletPrefab = bulletPrefab;
    }
}

// Базовый класс для оружия
public abstract class WeaponBase : MonoBehaviour
{
    public WeaponStats stats;   

    protected virtual void Start()
    {
        Debug.Log($"Weapon Initialized: {stats.weaponName}, Type: {stats.type}, Ammo: {stats.ammoCount}, Damage: {stats.damage}");
    }

    public virtual void Shoot()
    {
        if (stats.ammoCount > 0)
        {
            FireBullet();
            stats.ammoCount--;
            Debug.Log($"{stats.weaponName} fired! Remaining ammo: {stats.ammoCount}");
        }
        else
        {
            Debug.Log("Out of ammo!");
        }
    }

    public virtual void FireBullet()
    {
        if (stats.bulletPrefab != null)
        {
            GameObject bullet = Instantiate(stats.bulletPrefab, transform.position, transform.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = transform.forward * 20f; // Пример скорости
            }
        }
    }
}

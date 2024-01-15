using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponsClass 
{
    public int id;
    public string weaponName ;
    public int damage;
    public string weaponType;
    public Sprite sprite;


    public float fireRate = 1.0f;
    public float bulletSpeed = 20.0f;
    public float bulletCount ;

    [Header("Visuals")]
    public GameObject bulletPrefab;
    public GameObject weaponItemPrefab;

    public AudioClip shootSound;

    public WeaponsClass()
    {
    }

    // Constructor with parameters
    public WeaponsClass(WeaponsClass WeaponsClass)
    {
        id = WeaponsClass.id;
        weaponName = WeaponsClass.weaponName;
        damage = WeaponsClass.damage;
        weaponType = WeaponsClass.weaponType;
        sprite = WeaponsClass.sprite;
        fireRate = WeaponsClass.fireRate;
        bulletPrefab = WeaponsClass.bulletPrefab;
        bulletCount = WeaponsClass.bulletCount;
        bulletSpeed = WeaponsClass.bulletCount;
        shootSound = WeaponsClass.shootSound;
        weaponItemPrefab = WeaponsClass.weaponItemPrefab;
    }


}


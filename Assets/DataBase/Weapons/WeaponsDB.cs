using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponsStats", menuName = "WeaponsDB")]
public class WeaponsDB : ScriptableObject
{
    [SerializeField] WeaponsClass[] Weapon;

    public WeaponsClass getWeapon(int i)
    {
        return Weapon[i];
    }

}

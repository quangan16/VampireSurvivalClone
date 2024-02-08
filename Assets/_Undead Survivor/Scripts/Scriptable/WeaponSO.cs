using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GunSO/GunData")]
public class WeaponSO : ScriptableObject
{
    public RangedData[] rangedWeapons;
    public MeleeData[] meleeWeapons;

    public RangedData GetRangedData(int index)
    {
        return rangedWeapons[index];
    }

    public MeleeData GetMeleeData (int index)
    { 
        return meleeWeapons[index] ;
    }
}

[Serializable]
public class RangedData : IWeaponData
{
    public string name;
    public RangedWeapon weapon;
    public GameObject bulletPrefab;
    public int damage;
    public int maxPenatration;
    public float attackRate;
    public float attackRange;
    public float knockbackDistance;
    public float muzzleVelocity;
    public float intervalBurstRate;
    public int bulletBusrtQuantity;
}

[Serializable]
public class MeleeData : IWeaponData
{
    public string name;
    public MeleeWeapon weapon;
    public int damage;
    public float knockbackDistance;
    public float attackRate;
    public float attackRange;
}


public interface IWeaponData
{
    
}




using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : WeaponBase
{
    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] protected Transform muzzlePos;
    [SerializeField] public float muzzleVelocity;
    [SerializeField] protected int maxPenetration;
    protected RangedData rangedWeaponData;
    protected SpriteRenderer weaponRenderer;
    public override void InitData(int index)
    {
        rangedWeaponData = DataManager.Instance.weaponSO.rangedWeapons[index] ;
        WeaponCooldown = 0f;
        Damage = DataManager.Instance.weaponSO.rangedWeapons[index].damage;
        bulletPrefab = DataManager.Instance.weaponSO.rangedWeapons[index].bulletPrefab;
        maxPenetration = DataManager.Instance.weaponSO.rangedWeapons[index].maxPenatration;
        AttackRate = DataManager.Instance.weaponSO.rangedWeapons[index].attackRate;
        AttackRange = DataManager.Instance.weaponSO.rangedWeapons[index].attackRange;
        KnockbackDistance = DataManager.Instance.weaponSO.rangedWeapons[index].knockbackDistance;
        muzzleVelocity = DataManager.Instance.weaponSO.rangedWeapons[index].muzzleVelocity;
      

    }


    public void Awake()
    {
        weaponRenderer = GetComponent<SpriteRenderer>();
    }
    


    public override void Use()
    {
        Shoot();
    }

    public virtual void Shoot()
    {
        
    }

    
    
    
    
}

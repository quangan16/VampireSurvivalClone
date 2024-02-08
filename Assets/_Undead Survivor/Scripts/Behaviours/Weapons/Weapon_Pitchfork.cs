using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Pitchfork : RangedWeapon
{
    


    public void Awake()
    {
        weaponRenderer = GetComponent<SpriteRenderer>();
        
    }

    public void OnEnable()
    {
        OnWeaponReady += ShowWeapon;
    }

    public void OnDisable()
    {
        OnWeaponReady -= ShowWeapon;
    }


    public override void Use()
    {
        Shoot();
    }

    public virtual void Shoot()
    {
        if (CanAttack)
        {
            CanAttack = false;
            WeaponCooldown = AttackRate;
            AudioSystem.Instance.PlayOnce("Throw");
            GameManager.Instance.player.playerAnim.Play(AnimHash.RANGED_TRIDENT_ATTACK, 1, 0.0f);
            GameObject newBullet = Instantiate(bulletPrefab, muzzlePos.position, Quaternion.LookRotation(
                bulletPrefab.transform.forward, GameManager.Instance.player.AimDirection));
            if (newBullet.TryGetComponent(out BulletController bulletCtl))
            {
                bulletCtl.bulletDirection = GameManager.Instance.player.AimDirection.normalized;
                bulletCtl.bulletSpeed = muzzleVelocity;
                bulletCtl.bulletDamage = Damage;
                bulletCtl.bulletPenetration = maxPenetration;
                bulletCtl.knockbackDist = KnockbackDistance;
            }
           
        }
    }

 
    
    

    public void HideWeapon()
    {
        try
        {
            if (!CanAttack && weaponRenderer.enabled)
            {
                weaponRenderer.enabled = false;
            }
        }
        catch
        {
            print("lol");
        }

     
       
    }

    public void ShowWeapon()
    {
        try
        {
            if (CanAttack && !weaponRenderer.enabled)
            {
                weaponRenderer.enabled = true;
            }
        }
        catch (NullReferenceException e)
        {

            PrintOutExection();
        }
    }

    public void PrintOutExection()
    {
        print("its ok");
    }

  
    
    
}

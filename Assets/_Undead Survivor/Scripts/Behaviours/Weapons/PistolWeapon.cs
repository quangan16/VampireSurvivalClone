using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolWeapon : RangedWeapon
{
 


    public override void Use()
    {
        Shoot();
    }

    public virtual void Shoot()
    {
        if (CanAttack)
        {
            AudioSystem.Instance.PlayOnce("Shoot");
            GameManager.Instance.player.playerAnim.Play(AnimHash.RANGED_ATTACK, 1, 0.0f);
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

            CanAttack = false;
            WeaponCooldown = AttackRate;
        }
        
    }
   
    
    
    
    
}

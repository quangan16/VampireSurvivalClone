using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRange : EnemyBase
{

   [SerializeField] private GameObject bulletPrefab;
   [SerializeField] private float bulletSpeed;
   [SerializeField] private Vector3 aimDirection;
   public override void Update()
   {
      base.Update();
      if (attackCooldown > 0)
      {
         attackCooldown -= Time.deltaTime;
        
      }
      else 
      {
         canAttack = true;
      }

      if (Vector2.Distance(transform.position, GameManager.Instance.player.transform.position) < attackRange)
      {
         Attack();
      }
   }
   public void Attack()
   {
      if (GameManager.Instance.player.IsAlive)
      {
         if (isAlive && canAttack)
         {
            canAttack = false;
            attackCooldown = attackSpeed;
            aimDirection = (GameManager.Instance.player.transform.position - transform.position).normalized;
            GameObject newBullet = Instantiate(bulletPrefab, transform.position + direction.normalized,
               Quaternion.LookRotation(Vector3.forward,
                  aimDirection));
            if (newBullet.TryGetComponent(out BulletController bulletCtl))
            {

               bulletCtl.bulletDirection = aimDirection;
               bulletCtl.bulletDamage = attackDamage;
               bulletCtl.bulletSpeed = this.bulletSpeed;
               bulletCtl.bulletOwner = BulletOwner.ENEMY;
            }


         }
      }
      
   }

   
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : EnemyBase
{
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
   }
   public void Attack(Collider2D col)
   {
      if (GameManager.Instance.player.IsAlive)
      {
         if (isAlive && canAttack)
         {
            IDamageable damageableTarget = col.gameObject.GetComponent<IDamageable>();
            if (damageableTarget is not null)
            {
               damageableTarget.TakeDamage(damage: attackDamage);
               AudioSystem.Instance.PlayOnce("Hit0");
               canAttack = false;
               attackCooldown = attackSpeed;
            }
         }
      }
   }

   public void OnTriggerStay2D(Collider2D other)
   {
      if (other.CompareTag("Player"))
      {
         Attack(other);
      }
      
   }
}

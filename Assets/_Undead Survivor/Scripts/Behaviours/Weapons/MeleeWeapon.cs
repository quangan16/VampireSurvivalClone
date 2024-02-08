using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : WeaponBase
{
    [SerializeField] private Transform attackPos;
    public override void Use()
    {
       
       
        Collider2D[] hitObject = Physics2D.OverlapCircleAll(attackPos.position, AttackRange, 1<<7);
        if (hitObject.Length > 0)
        {
            int random = Random.Range(0, 2);
            AudioSystem.Instance.PlayOnce("MeleeHit" + random);
            foreach (var enemyHit in hitObject)
            {
                if (enemyHit.TryGetComponent(out EnemyBase enemyCtl))
                {
                    enemyCtl.TakeDamage(damageIncome:Damage);
                    Vector2 knockbackDirection = (enemyCtl.gameObject.transform.position - GameManager.Instance.player.transform.position).normalized;
                    enemyCtl.GetKnockback(knockbackDirection, KnockbackDistance);
                }
            }

          
        }
        else
        {
            AudioSystem.Instance.PlayOnce("Woosh");
        }

        WeaponCooldown = AttackRate;
    }

    public override void InitData(int index)
    {
        MeleeData data = DataManager.Instance.weaponSO.meleeWeapons[index] as MeleeData;
       
        Damage = data.damage;
        AttackRate = data.attackRate;
        AttackRange = data.attackRange;
        KnockbackDistance = data.knockbackDistance;
        WeaponCooldown = 0f;
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPos.position, AttackRange);
    }
}

using System.Collections;
using UnityEngine;


using System.Collections;
using UnityEngine;

public class RiffleWeapon : RangedWeapon
{
    public int bulletBurstMax = 3;
    public float burstIntervalTime = 0.4f;
    public float burstTimeCooldown = 0;
    public int bulletBurstLeft;
    private Coroutine shootCoroutine;

    public override void InitData(int index)
    {
        base.InitData(1);
        burstIntervalTime = DataManager.Instance.weaponSO.rangedWeapons[index].intervalBurstRate;
        bulletBurstMax = DataManager.Instance.weaponSO.rangedWeapons[index].bulletBusrtQuantity;
    }

    public override void Use()
    {
        shootCoroutine = StartCoroutine(Shoot());
    }

    public new IEnumerator Shoot()
    {
        if (shootCoroutine != null)
        {
            StopCoroutine(shootCoroutine);
        }
        
        if (CanAttack)
        {
            bulletBurstLeft = bulletBurstMax;
            WeaponCooldown = AttackRate;
            while (bulletBurstLeft > 0)
            {
                if (burstTimeCooldown <= 0)
                {
                    --bulletBurstLeft;
                    AudioSystem.Instance.PlayOnce("Shoot");
                    GameManager.Instance.player.playerAnim.Play(AnimHash.RANGED_RIFFLE_ATTACK, 1, 0.0f);
                    GameObject newBullet = Instantiate(bulletPrefab, muzzlePos.position,
                        Quaternion.LookRotation(transform.forward, GameManager.Instance.player.AimDirection));
                    if (newBullet.TryGetComponent(out BulletController bulletCtl))
                    {
                        bulletCtl.bulletDirection = GameManager.Instance.player.AimDirection.normalized;
                        bulletCtl.bulletSpeed = muzzleVelocity;
                        bulletCtl.bulletDamage = Damage;
                        bulletCtl.bulletPenetration = maxPenetration;
                        bulletCtl.knockbackDist = KnockbackDistance;
                    }

                    burstTimeCooldown = burstIntervalTime;
                }
                else
                {
                    burstTimeCooldown -= Time.deltaTime;
                }
               
                yield return null;
            }

            burstTimeCooldown = 0f;


        }

       
           
    }

       

       
    
}

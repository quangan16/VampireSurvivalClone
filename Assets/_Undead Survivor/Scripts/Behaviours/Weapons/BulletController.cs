using System;
using UnityEngine;


public class BulletController : MonoBehaviour
{
    [SerializeField] public Vector3 bulletDirection;
    [SerializeField] public float bulletSpeed;
    [SerializeField] public int bulletDamage;
    [SerializeField] public int bulletPenetration;
    [SerializeField] public float knockbackDist;
    [SerializeField] public BulletOwner bulletOwner;
    public void Update()
    {
        Move();
    }

    public void Move()
    {
        transform.position += bulletDirection * (bulletSpeed * Time.deltaTime);
    }

    public void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (bulletOwner == BulletOwner.PLAYER)
        {
            if (other.CompareTag("Enemy") && other.TryGetComponent(out IDamageable damageableTarget))
            {
                AudioSystem.Instance.PlayOnce("RangeHit");
                if (bulletPenetration >0)
                {
                    bulletPenetration--;
                    damageableTarget.GetKnockback(bulletDirection, knockbackDist);
                    damageableTarget.TakeDamage(bulletDamage);
                   
                    
                }
                if (bulletPenetration <= 0)
                {
                    gameObject.GetComponent<Collider2D>().enabled = false;
                    Destroy(gameObject);
                }
               

            }
        }
        else
        {
            if (other.CompareTag("Player") && other.TryGetComponent(out IDamageable damageableTarget))
            {
                if (bulletPenetration <= 1)
                {
                    AudioSystem.Instance.PlayOnce("Hit1");
                    damageableTarget.GetKnockback(bulletDirection, knockbackDist);
                    damageableTarget.TakeDamage(bulletDamage);
                    Destroy(gameObject);
                }
                else
                {
                    bulletPenetration--;
                }
              

            }
        }
        
    }
}

public enum BulletOwner{
    PLAYER,
    ENEMY,
}

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour, IDamageable
{
   
   [SerializeField] protected float moveSpeed;
   [SerializeField] protected int attackDamage;
   [SerializeField] protected bool isAlive;
   [SerializeField] protected int maxHealth;
   [SerializeField] protected int currentHealth;
   [SerializeField] protected Vector3 direction;
   [SerializeField] protected float attackRange;
   [SerializeField] protected bool canAttack;
   [SerializeField] protected float attackSpeed;
   [SerializeField] protected float attackCooldown;
   [SerializeField] protected Transform currentTarget;
   [SerializeField] private Animator enemyAnimator;
   [SerializeField] private Collider2D enemyCollider;
   [SerializeField] private GameObject expGemPrefab;

   [SerializeField] private GameObject duplicatePrefab;
   [SerializeField] private bool canDuplicate;

   public enum FaceDirection
   {
      RIGHT,
      LEFT
   }

   private FaceDirection faceDir;
   public bool IsAlive => isAlive;
   public int CurrentHealth => currentHealth;

   public void Start()
   {
      Init();
   }
   
   public void Init()
   {
      
      canAttack = false;
      isAlive = true;
      gameObject.layer = 7;
      currentHealth = maxHealth;
      currentTarget = (GameManager.Instance.player)? GameManager.Instance.player.transform: null;
   }

   public void ChaseTarget(Transform target)
   {
      if (target is not null)
      {
         direction = (target.position - transform.position).normalized;
         if (isAlive && Vector2.Distance(transform.position, target.position) > attackRange)
         {
            transform.position += direction * (moveSpeed * Time.deltaTime);
         }
      }
      else
      {
         Debug.Log("Null enemy's target");
      }
      
      
   }

   public void UpdateFaceDirection()
   {
      if (direction.x >= 0 && faceDir == FaceDirection.LEFT)
      {
         faceDir = FaceDirection.RIGHT;
         transform.Rotate(Vector3.up, 180, Space.Self);
      }
      else if (direction.x < 0 && faceDir == FaceDirection.RIGHT)
      {
         faceDir = FaceDirection.LEFT;
         transform.Rotate(Vector3.up, -180, Space.Self);
      }
   }
   

   public virtual void Update()
   {
      ChaseTarget(currentTarget);
      UpdateFaceDirection();
   }
   
   
   

   public virtual void TakeDamage(int damageIncome)
   {
      if (isAlive)
      {
         currentHealth -= damageIncome;
         enemyAnimator.SetTrigger("Hit");
         if (currentHealth <= 0)
         {
            Die();
         }
      }
     
   }

   public void GetKnockback(Vector2 knockbackDirection, float knockbackDistance)
   {
      StartCoroutine(OnKnockedBack(knockbackDirection, knockbackDistance));
   }

   public virtual IEnumerator OnKnockedBack(Vector2 knockbackDirection, float knockbackDistance)
   {
      
      Vector2 startPos = transform.position;
      Vector2 targetPos = startPos + knockbackDirection * knockbackDistance;
      float t = 0f;
      while (t < 1 && isAlive )
      {
         t += (Time.deltaTime * 2) / knockbackDistance;
         transform.position = Vector2.Lerp(startPos, targetPos, 1-((1-t) * (1-t)));
         yield return null;
      }
      
   }
   
   
   public void Die()
   {
      isAlive = false;
      enemyCollider.enabled = false;
      enemyAnimator.SetBool("isAlive", isAlive);
      if (canDuplicate)
      {
         SpawnDuplicate();
      }

      GameManager.Instance.enemyKilled++;
      UIManager.Instance.UpdateKilledEnemy();
      Destroy(gameObject, 2.0f);
   }

   public void SpawnExpGem()
   {
      GameObject newGemExp = Instantiate(expGemPrefab, transform.position, Quaternion.identity);
      newGemExp.GetComponent<GemExp>().OnEmerge(transform.position.y - 0.5f);
   }

   public void SpawnDuplicate()
   {
      if (duplicatePrefab is not null)
      {
         GameObject newEnemy1 = Instantiate(duplicatePrefab, transform.position, Quaternion.identity);
         GameObject newEnemy2 = Instantiate(duplicatePrefab, transform.position, Quaternion.identity);
         StartCoroutine(AddForce(newEnemy1, _direction: new Vector2(direction.y, -direction.x)));
         StartCoroutine(AddForce(newEnemy2, _direction: new Vector2(-direction.y, direction.x)));
      }
      
      IEnumerator AddForce(GameObject go,Vector2 _direction)
      {
         float t = 0f;
         while (t < .5f)
         {
            t += Time.deltaTime;
            go.transform.position += (Vector3)_direction *  Time.deltaTime;
            yield return null;
         }
      }
   }
   




}

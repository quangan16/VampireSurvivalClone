using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, IDamageable
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private ContactFilter2D _filter;
    [SerializeField] private Vector2 moveDirection;
    [SerializeField] public Animator playerAnim;
    
    [SerializeField] private int maxHealth;
    [SerializeField] private GameObject leftHand;
    [SerializeField] public MeleeWeapon meleeWeapon;
    [SerializeField] private GameObject rightHand;
    [SerializeField] public RangedWeapon rangedWeapon;

    [SerializeField] public int currentRangedWeaponIndex;
    
    [SerializeField] private bool isMoving;
    [SerializeField] private HealthBar playerHealthBar;
    [SerializeField] public int experience;
    [SerializeField] public int maxLevelExperience;
    [SerializeField] public int currentLevel;
    [SerializeField] private AnimationClip clip;

    [SerializeField] private InputActionReference movementInput, meleeAttackInput, rangedAttackInput, pointerPositionInput;
    [field: SerializeField] public Vector2 AimDirection { get; private set; }
    public enum FaceDirection
    {
        RIGHT,
        LEFT
    }
    private FaceDirection faceDir;

    [SerializeField] public float expRange;

    [field: SerializeField] public int CurrentHealth { get; protected set; }

    [field: SerializeField] public bool IsAlive { get; protected set; }



    
  

    public void OnEnable()
    {
        rangedAttackInput.action.performed += RangedAttack;
        meleeAttackInput.action.performed += MeleeAttack;
    }

    public void OnDisable()
    {
        rangedAttackInput.action.performed -= RangedAttack;
        meleeAttackInput.action.performed -= MeleeAttack;
    }

    public void Start()
    {
        Init();
        AddAnimEvent();
    }

    

    public void Init()
    {
        IsAlive = true;
        isMoving = false;
        faceDir = FaceDirection.RIGHT;
        playerAnim.Play(AnimHash.IDLE, -1, 0.0f);
        CurrentHealth = maxHealth;
        experience = 0;
        currentLevel = 1;
        maxLevelExperience = 10;
        currentRangedWeaponIndex = 0;
        TakeMeleeWeapon(0);
        TakeRangedWeapon(currentRangedWeaponIndex);
        AddAnimEvent();
    }

    public void TakeDamage(int damageIncome)
    {
        if (IsAlive)
        {
            CurrentHealth -= damageIncome;
            if (CurrentHealth <= 0)
            {
                Die();
            }
        }
    }

    public void ChangeGun()
    {
        if (Input.GetAxisRaw("Mouse ScrollWheel") < 0f && currentRangedWeaponIndex<
            DataManager.Instance.weaponSO.rangedWeapons.Length - 1)
        {
            TakeRangedWeapon(++currentRangedWeaponIndex);
        }
        else if (Input.GetAxisRaw("Mouse ScrollWheel") > 0f && currentRangedWeaponIndex > 0)
        {
            TakeRangedWeapon(--currentRangedWeaponIndex);
        }

       
    }

    public void TakeMeleeWeapon(int weaponIndex)
    {
        MeleeWeapon tempMeleeWeapon = DataManager.Instance.weaponSO.GetMeleeData(weaponIndex).weapon;
        meleeWeapon = Instantiate(tempMeleeWeapon.gameObject, leftHand.transform.GetChild(0).transform).GetComponent<MeleeWeapon>();
        meleeWeapon.InitData(weaponIndex);
        
    }

    public void TakeRangedWeapon(int weaponIndex)
    {
        if (rangedWeapon != null)
        {
            Destroy(rangedWeapon.gameObject);
        }
        RangedWeapon tempRangedWeapon = DataManager.Instance.weaponSO.GetRangedData(weaponIndex).weapon;
        rangedWeapon = Instantiate(tempRangedWeapon.gameObject, rightHand.transform.GetChild(0).transform)
            .GetComponent<RangedWeapon>();
        rangedWeapon.InitData(weaponIndex);

    }

    
    
    public void Update()
    {
        ChangeGun();
        Move();
        // MeleeAttack();
        Aim();
        // RangedAttack();

    }

    public void FixedUpdate()
    {
        GetGem();
        UpdateHealthBar();
    }

    public void Move()
    {

        if (IsAlive)
        {
            // Vector2 moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
            moveDirection = movementInput.action.ReadValue<Vector2>();
            isMoving = (moveDirection != Vector2.zero);
            playerAnim.SetBool("isMoving", isMoving);
            transform.position += (Vector3)moveDirection * (moveSpeed * Time.deltaTime);
            if (AimDirection.x >= 0 && faceDir == FaceDirection.LEFT)
            {
                faceDir = FaceDirection.RIGHT;
                transform.Rotate(Vector3.up, 180);


            }
            else if (AimDirection.x < 0 && faceDir == FaceDirection.RIGHT)
            {
                faceDir = FaceDirection.LEFT;
                transform.Rotate(Vector3.up, -180);
            }

            if (moveDirection.x * AimDirection.x < 0)
            {
                playerAnim.SetFloat("faceDirection", -1);
            }
            else
            {
                playerAnim.SetFloat("faceDirection", 1);
            }
        }
       
         
        
    }

    public void GetExpGem()
    {
        
    }

    public void Aim()
    {
        
        AimDirection = (Camera.main.ScreenToWorldPoint(pointerPositionInput.action.ReadValue<Vector2>()) - rightHand.transform.position).normalized;
        float angle = Mathf.Atan2(AimDirection.y, AimDirection.x) * Mathf.Rad2Deg;
        rightHand.transform.rotation = Quaternion.Euler(0, 0, angle);
        
        if (AimDirection.x >= 0 && rightHand.transform.localScale.y == -1)
        {
            rightHand.transform.localScale = Vector3.one;
        }
        else if(AimDirection.x < 0 && rightHand.transform.localScale.y == 1)
        {
            rightHand.transform.localScale = Vector3.Scale(Vector3.one, new Vector3(1,-1,1));
        }
        
    }

    public void MeleeAttack(InputAction.CallbackContext context)
    {
        
        if (IsAlive)
        {
            // if (Input.GetMouseButtonDown(1))
            // {
                if (meleeWeapon.CanAttack)
                {
                    playerAnim.CrossFade(AnimHash.MELEE_ATTACK, 0.0f, 2, 0.0f);
                    
                }
            // }
        }
        
    }

    public void UseMeleeWeapon()
    {
        meleeWeapon.Use();
    }

    public void RangedAttack(InputAction.CallbackContext context)
    {
        if (IsAlive)
        {
            print(context.phase);
            // if (Input.GetMouseButton(0))
            // {
                if (rangedWeapon.CanAttack)
                {
                   
                    rangedWeapon.Use();
                }
            // }
        }
       
    }

    public void Die()
    {
        IsAlive = false;
        UIManager.Instance.ShowFinal();
        playerAnim.CrossFade(AnimHash.DIE, 0.1f, 0, 0.0f);
    }

    public void GetKnockback(Vector2 knockbackDir, float knockbackDist)
    {
        return ;
    }

    public void UpdateHealthBar()
    {
        playerHealthBar.UpdateDisplay((float)CurrentHealth/maxHealth);
    }
    

    

    public void GetGem()
    {
        Collider2D[] col= Physics2D.OverlapCircleAll(transform.position, expRange, 1<<6);
        if (col is not null)
        {
            foreach (var gem in col)
            {
                if (gem.TryGetComponent(out GemExp gemExp))
                {
                    gemExp.OnAchieve(transform);

                }
            }
          

           
        }
    }

    public void AddExp(int expIncome)
    {
        experience += expIncome;
        if (experience >= maxLevelExperience)
        {
            AudioSystem.Instance.PlayOnce("LevelUp");
            //TODO: Open Upgrade Skill
            maxLevelExperience += 5 * currentLevel;
            ++currentLevel;
            experience = 0;
           
        }

        UIManager.Instance.UpdateExpBar();
    }

    public void AddAnimEvent()
    {
        AnimationEvent animationEvent = new AnimationEvent();
        animationEvent.functionName = "HideWeapon";
        animationEvent.time = 0.08f;
        clip.AddEvent(animationEvent);
    }



    public void HideWeapon()
    {
        if (rangedWeapon is Weapon_Pitchfork)
        {
            rangedWeapon.GetComponent<Weapon_Pitchfork>().HideWeapon();
        }
    }
   
   
}

public class AnimHash
{
    public static readonly int IDLE = Animator.StringToHash("Idle");
    public static readonly int MELEE_ATTACK = Animator.StringToHash("MeleeAttack");
    public static readonly int RANGED_ATTACK = Animator.StringToHash("RangedAttack");
    public static readonly int RANGED_RIFFLE_ATTACK = Animator.StringToHash("RangedRiffleAttack");
    public static readonly int RANGED_TRIDENT_ATTACK = Animator.StringToHash("RangedTridentAttack");
    public static readonly int MOVE = Animator.StringToHash("Move");
    public static readonly int DIE = Animator.StringToHash("Die");
}



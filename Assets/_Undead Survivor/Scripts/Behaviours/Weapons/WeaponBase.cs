
using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    public static event Action OnWeaponReady; 
    public int Damage
    {
        get;
        protected set;
    }

    [field: SerializeField]
    public float AttackRate
    {
        get;
        set;
    }

    [field: SerializeField]
    public float AttackRange
    {
        get;
        protected set;
    }

    [field: SerializeField]
    public float KnockbackDistance
    {
        get;
        protected set;
    }

    [field: SerializeField]
    public WeaponType WeaponType
    {
        get;
        protected set;
    }

    [field: SerializeField]
    public float WeaponCooldown { get; protected set; }
    
    [field: SerializeField]
    public bool CanAttack { get; protected set; }

    public abstract void InitData(int index);
    
    public abstract void Use();

    public virtual void Update()
    {
        EvaluateWeaponCooldown();
    }

    protected virtual void EvaluateWeaponCooldown()
    {
        CanAttack = (WeaponCooldown <= 0);
        if (WeaponCooldown > 0)
        {
            WeaponCooldown -= Time.deltaTime;
           
        }
        else
        {
            OnWeaponReady?.Invoke();
            
        }
    }

 

}

public enum WeaponType
{
    RANGED,
    MELEE
}

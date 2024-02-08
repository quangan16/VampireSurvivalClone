using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{ 
    public void TakeDamage(int damage);

    public void GetKnockback(Vector2 knockbackDirection, float knockbackDistance);
    
    [field: SerializeField] public int CurrentHealth { get; }
    [field: SerializeField] public bool IsAlive { get; }
}

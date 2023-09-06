using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable 
{
    public bool isInvincible { get; set; }
    public float health { get; set; }
    public bool isDead { get; set; }
    public void TakeDamage(float damage);
}

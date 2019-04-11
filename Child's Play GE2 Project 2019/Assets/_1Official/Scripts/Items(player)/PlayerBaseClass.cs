using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseClass : MonoBehaviour
{
    [Header("Player Option")]
    [SerializeField] private int hitPoints = 100;
    
    public int HitPoints { get => hitPoints; set => hitPoints = value; }

    // Update is called once per frame
    protected void Update()
    {
        
    }

    protected virtual void Die()
    {
        Destroy(this.gameObject);
    }

    public virtual void TakeDamage(int damageValue)
    {
        this.hitPoints -= damageValue;

        if (this.HitPoints <= 0)
        {
            Die();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseClass : MonoBehaviour
{
    [SerializeField] private int hitPoints = 100;
    
    public int HitPoints { get => hitPoints; set => hitPoints = value; }

    // Update is called once per frame
    protected void Update()
    {
        
    }

    private void Die()
    {
        Destroy(this.gameObject);
    }

    public void TakeDamage(int damageValue)
    {
        this.hitPoints -= damageValue;

        if (this.HitPoints <= 0)
        {
            Die();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseClass : MonoBehaviour
{
    [SerializeField] private int hitPoints;
    
    public int HitPoints { get => hitPoints; set => hitPoints = value; }

    // Update is called once per frame
    void Update()
    {
        if (this.HitPoints < 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(this.gameObject);
    }
}

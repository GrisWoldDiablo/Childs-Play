using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;


//[RequireComponent(typeof(Tower))]
public class Laser : Projectile
{
    [SerializeField] private float secondPerTick;
    [SerializeField] private float secondItLast;

    public override void Damage(GameObject enemyGO)
    {
        Enemy enemy = enemyGO.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.DamageOverTime(damageValue, secondPerTick, secondItLast);
        }
    }

}

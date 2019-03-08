using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;


//[RequireComponent(typeof(Tower))]
public class Laser : Projectile
{
    [SerializeField] private float secondPerTick;
    [SerializeField] private float secondItLast;

    new void Update()
    {
        base.Update();
        HittingTarget();
    }

    public override void Damage(Transform enemy)
    {
        enemy.GetComponent<Enemy>().DamageOverTime(damageValue, secondPerTick, secondItLast);
    }

}

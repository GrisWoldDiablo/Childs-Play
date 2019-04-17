using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWorker : Enemy
{
    protected override void Start()
    {
        SetAnimWalking();

        EnemyManager.GetInstance().AddEnemyToList(this as Enemy);
        ogHP = HitPoints;
    }
    protected override void Die()
    {
        base.Die();

        Destroy(this.gameObject, 5);
        transform.Rotate(Vector3.up * Random.Range(-180, 180));
    }
    protected override void UpdateHealthBar()
    {
        //No health bars
    }
}

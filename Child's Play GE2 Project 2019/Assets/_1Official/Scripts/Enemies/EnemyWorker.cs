using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWorker : Enemy
{
    protected override void Die()
    {
        base.Die();

        Destroy(this.gameObject, 5);
        transform.Rotate(Vector3.up * Random.Range(-180, 180));
    }
}

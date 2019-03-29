using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlyer : Enemy
{
    new private void Start()
    {
        base.Start();
        GameObject parent = this.transform.parent.gameObject;
        this.transform.SetParent(null);
        Destroy(parent);

    }

    public override void SetAttacking(Item target)
    {
        //Nothing happens here for now, this enemy dodge barriers.
        //Might attack other stuff later.
    }

    protected override void Die()
    {
        base.Die();

        Destroy(this.gameObject, 5);
        transform.Rotate(Vector3.up * Random.Range(-180, 180));
    }
}

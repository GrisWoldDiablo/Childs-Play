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
}

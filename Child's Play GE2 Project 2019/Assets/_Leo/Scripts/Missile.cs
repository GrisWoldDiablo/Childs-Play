using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Projectile
{

    public override void AssignTarget(Transform tar)
    {
        _target = tar;
        direction = _target.position - this.transform.position;
        UpdatTargetLocation();
        _target = new GameObject().transform;
        _target.transform.position = targetLocation;
    }    

    // Update is called once per frame
    new void Update()
    {
        HittingTarget();
    }

    public override void HittingTarget()
    {
        if (_target == null)
        {
            Destroy(this.gameObject);
            return;
        }
                
        float currentDistance = projectileSpeed * Time.deltaTime;       

        this.transform.Translate(direction.normalized * currentDistance, Space.World);
        this.transform.LookAt(_target);

    }

    private void OnTriggerEnter(Collider other)
    {
        HitTarget(other);
    }    

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AoERadius);
    }
}


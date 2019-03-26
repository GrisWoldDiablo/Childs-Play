using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Projectile
{
    public override void AssignTarget(Transform target)
    {
        _target = target;
        this.transform.LookAt(_target);
        rb.velocity = this.transform.forward * projectileSpeed;
        transform.rotation = Quaternion.LookRotation(rb.velocity);
        Debug.DrawRay(this.transform.position, rb.velocity, Color.red, 0.5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AoERadius);
    }
}


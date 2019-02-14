using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    protected float projectileSpeed;
    [SerializeField]
    protected float AoERadius = 0.0f;
    [SerializeField]
    protected GameObject impactVFX;

    protected Transform _target;
    protected Vector3 direction;

    public virtual void AssignTarget(Transform target)
    {
        _target = target;        
    }

    public virtual void HittingTarget()
    {
        if (_target == null)
        {
            Destroy(this.gameObject);
            return;
        }
        direction = _target.position - this.transform.position;
        float currentDistance = projectileSpeed * Time.deltaTime;
                
        if (direction.magnitude <= currentDistance) 
        {            
            this.HitTarget();
            return;
        }

        this.transform.Translate(direction.normalized * currentDistance, Space.World);

        //this.transform.LookAt(direction); // UNCOMMENT IF you want missiles that seek target
    }

    public void HitTarget()
    {
        //TODO: spawn effect
        //TODO: destroy effect

        if (AoERadius > 0f)
        {
            Explode();
        }
        else
        {
            Destroy(_target.gameObject);
        }
        Destroy(this.gameObject);
    }

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(this.transform.position, AoERadius);
        foreach (Collider col in colliders)
        {
            if (col.tag == "Enemy")
            {
                Damage(col.transform);
            }
        }
    }

    void Damage(Transform enemy)
    {
        Destroy(enemy.gameObject);
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    private Transform _target;

    public float projectileSpeed;

    public float AoERadius = 0.0f;

    public void Seek(Transform tar)
    {
        _target = tar;
    }

    // Update is called once per frame
    void Update()
    {
        if(_target == null)
        {
            Destroy(this.gameObject);
            return;
        }

        Vector3 direction = _target.position - this.transform.position;
        float currentDistance = projectileSpeed * Time.deltaTime;

        if(direction.magnitude <= currentDistance)
        {
            HitTarget();
            return;
        }

        this.transform.Translate(direction.normalized * currentDistance, Space.World);
    }

    void HitTarget()
    {
        //TODO: spawn effect
        //TODO: destroy effect

        if(AoERadius > 0f)
        {
            Explode();
        }
        else
        {

        }
        Destroy(this.gameObject);
    }

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(this.transform.position, AoERadius);
        foreach (Collider col in colliders)
        {
            if(col.tag == "Enemy")
            {
                Damage(col.transform);
            }
        }
    }

    void Damage(Transform enemy)
    {
        Destroy(enemy.gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AoERadius);
    }
}

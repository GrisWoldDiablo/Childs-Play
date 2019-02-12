using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Projectile
{
    //private Transform _target;

    //public float projectileSpeed;

    //public float AoERadius = 0.0f;

    //private Vector3 direction;

    public void Seek(Transform tar)
    {
        _target = tar;
        //direction = _target.position - this.transform.position;
    }

    private void Start()
    {
        Destroy(this.gameObject, 5f);
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
        //direction = _target.position - this.transform.position;
        float currentDistance = projectileSpeed * Time.deltaTime;

        if(direction.magnitude <= 1f)//(direction.magnitude <= currentDistance)
        {
            HitTarget();
            Debug.Log("hitTarget called");
            return;
        }

        this.transform.Translate(direction.normalized * currentDistance, Space.World);

        //this.transform.LookAt(direction);
    }

    private void OnTriggerEnter(Collider other)
    {
        HitTarget();
    }

    void HitTarget()
    {
        //TODO: spawn effect
        //TODO: destroy effect

        if(AoERadius > 0f)
        {
            Explode();
            Debug.Log("explode Called");
        }
        else
        {

        }
        //Destroy(this.gameObject);
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

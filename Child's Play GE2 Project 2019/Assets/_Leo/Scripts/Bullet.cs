using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Projectile
{
    //private Transform _target;

    //public float projectileSpeed = 70f;
    //public GameObject impactVFX;

    public void Seek(Transform target)
    {
        _target = target;
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
        float distanceThisFrame = projectileSpeed * Time.deltaTime;

        if(direction.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
    }

    private void HitTarget()
    {
        //GameObject effectInstance = Instantiate(impactVFX, this.transform.position, this.transform.rotation);
        //Destroy(effectInstance, 2f);

        Destroy(_target.gameObject);
        Destroy(this.gameObject);
    }
}

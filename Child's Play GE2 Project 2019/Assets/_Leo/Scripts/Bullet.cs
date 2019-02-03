using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform bulletTarget;

    public float bulletSpeed = 70f;
    public GameObject impactVFX;

    public void Seek(Transform target)
    {
        bulletTarget = target;
    }

    // Update is called once per frame
    void Update()
    {
        if(bulletTarget == null)
        {
            Destroy(this.gameObject);
            return;
        }

        Vector3 direction = bulletTarget.position - this.transform.position;
        float distanceThisFrame = bulletSpeed * Time.deltaTime;

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

        Destroy(bulletTarget.gameObject);
        Destroy(this.gameObject);
    }
}

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
    protected ParticleSystem impactVFX;

    [SerializeField] protected Vector3 targetLocation;
    protected Transform _target;
    protected Vector3 direction;
    [SerializeField] protected int damageValue = 10;

    public int DamageValue { get => damageValue; set => damageValue = value; }

    protected void Update()
    {
        if (_target != null)
        {
            UpdatTargetLocation();
        }
    }

    public void UpdatTargetLocation()
    {
        targetLocation = _target.position;
    }

    public virtual void AssignTarget(Transform target)
    {
        _target = target;
        UpdatTargetLocation();
    }

    public virtual void HittingTarget()
    {
        if (_target == null)
        {
            _target = new GameObject().transform;
            _target.transform.position = targetLocation;
            return;
        }
        direction = _target.position - this.transform.position;
        float currentDistance = projectileSpeed * Time.deltaTime;
        
        
        // Retired,  using OnTriggerEnter
        //if (direction.magnitude <= currentDistance) 
        //{            
        //    this.HitTarget();
        //    return;
        //}

        this.transform.Translate(direction.normalized * currentDistance, Space.World);

        //this.transform.LookAt(direction); // UNCOMMENT IF you want missiles that seek target
    }

    public void HitTarget(Collider other)
    {
        //TODO: spawn effect
        //TODO: destroy effect

        if (AoERadius > 0f)
        {
            Explode();
        }
        else
        {
            if (other.CompareTag("Enemy"))
            {
                Damage(other.transform);
            }
        }
        if (!_target.CompareTag("Enemy"))
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
                //if (AoERadius > 0f)
                //{
                //    Explode();
                //}
                //else
                //{
                //    HitEnemy();
                //}
                Damage(col.transform);
            }
        }
    }

    void Damage(Transform enemy)
    {
        enemy.GetComponent<Enemy>().TakeDamage(damageValue);
        //Debug.Log($"{enemy.GetInstanceID()} : {enemy.GetComponent<Enemy>().HitPoints}");
        //Destroy(enemy.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            HitTarget(other);
        }
        else if (other.gameObject.CompareTag("TilePath"))
        {
            HitTarget(other);
        }
    }

}


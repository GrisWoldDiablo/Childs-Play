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
    [SerializeField] protected Transform _target;
    protected Vector3 direction;
    [SerializeField] protected int damageValue = 10;
    private Rigidbody rb;
    public int DamageValue { get => damageValue; set => damageValue = value; }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(this.gameObject, 5.0f);
    }

    protected void Update()
    {
        MoveProjectile();
        //if (_target != null)
        //{
        //    UpdatTargetLocation();
        //}
        //else
        //{
        //    Destroy(_target.gameObject);
        //    Destroy(this.gameObject);
        //}
    }

    private void MoveProjectile()
    {
        rb.AddForce(direction * projectileSpeed * Time.deltaTime, ForceMode.VelocityChange);
    }

    //public void UpdatTargetLocation()
    //{
    //    targetLocation = _target.position;
    //    
    //}

    public virtual void AssignTarget(Transform target)
    {
        _target = target;
        direction = _target.position + _target.forward - this.transform.position;
        this.transform.LookAt(_target);
        //UpdatTargetLocation();
    }

    //public virtual void HittingTarget()
    //{
    //    rb.AddForce(direction * projectileSpeed * Time.deltaTime, ForceMode.VelocityChange);

    //    //if (_target == null && this.GetType() != typeof(Laser))
    //    //{
    //    //    _target = new GameObject().transform;
    //    //    _target.transform.position = targetLocation;
    //    //    return;
    //    //}
    //    //direction = _target.position - this.transform.position;
    //    //float currentDistance = projectileSpeed * Time.deltaTime;


    //    // Retired,  using OnTriggerEnter
    //    //if (direction.magnitude <= currentDistance) 
    //    //{            
    //    //    this.HitTarget();
    //    //    return;
    //    //}

    //    //this.transform.Translate(direction.normalized * currentDistance, Space.World);
    //    //if (Vector3.Distance(this.transform.position,_target.position) < 0.1f)
    //    //{
    //    //    Destroy(this.gameObject);
    //    //}
    //    //this.transform.LookAt(direction); // UNCOMMENT IF you want missiles that seek target
    //}

    public virtual void HitTarget(Collider other)
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
                Damage(other.gameObject);
            }
        }
        //if (!_target.CompareTag("Enemy"))
        //{
        //    Destroy(_target.gameObject);
        //}
        Destroy(this.gameObject);
    }

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(this.transform.position, AoERadius);
        foreach (Collider col in colliders)
        {
            if (col.tag == "Enemy")
            {
                Damage(col.gameObject);
            }
        }
    }

    public virtual void Damage(GameObject enemyGO)
    {
        Enemy enemy = enemyGO.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damageValue); 
        }
        //Debug.Log($"{enemy.GetInstanceID()} : {enemy.GetComponent<Enemy>().HitPoints}");
        //Destroy(enemy.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            HitTarget(other);
        }
        else if (other.gameObject.CompareTag("TilePath") || other.gameObject.CompareTag("Terrain"))
        {
            HitTarget(other);
        }
    }

}


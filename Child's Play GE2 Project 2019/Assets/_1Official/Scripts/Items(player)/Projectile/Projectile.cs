using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
    protected Rigidbody rb;
    public int DamageValue { get => damageValue; set => damageValue = value; }

    [SerializeField] private AudioSource myAudioSource;


    [SerializeField]
    private ParticleSystem _projectileFX;

    [SerializeField] private bool hasHitOnce = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(this.gameObject, 5.0f);
    }
    
    public virtual void AssignTarget(Transform target)
    {
        _target = target;
        this.transform.LookAt(_target);
        rb.velocity = this.transform.forward * projectileSpeed + _target.GetComponent<NavMeshAgent>().velocity;
        transform.rotation = Quaternion.LookRotation(rb.velocity);
        Debug.DrawRay(this.transform.position, rb.velocity, Color.red, 0.5f);
    }
    
    public virtual void HitTarget(Collider other)
    {
        //TODO: spawn effect
        PlayVFX();
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

        Destroy(this.gameObject);
    }

    void Explode()
    {
        AudioSource.PlayClipAtPoint(SoundManager.GetInstance().GetAudioClip(Sound.missileSfx), this.transform.position);
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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasHitOnce)
        {
            return;
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            HitTarget(other);
            hasHitOnce = true;
        }
        else if (other.gameObject.CompareTag("TilePath") || other.gameObject.CompareTag("Terrain"))
        {
            HitTarget(other);
        }
    }

    public void PlayVFX()
    {
        if (impactVFX == null)
        {
            return;
        }

        impactVFX.transform.parent = null;
        impactVFX.transform.position += Vector3.up;
        impactVFX.Play();
        Destroy(impactVFX.gameObject , impactVFX.main.duration);
        _projectileFX.Play();
    }
}


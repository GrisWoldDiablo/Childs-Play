using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public Transform towerTarget;


    public Transform pivot;
    public float rotationSpeed = 10f;
    public string enemyTag = "Enemy";


    //FIRING PART
    private GameObject projectilePrefab;
    public Transform projectileSpawnPoint;

    private float range;// = 15f;
    private float rateOfFire;// = 1f;
    private float CountdownToNextFire = 0f;
    private float innerRadius;


    public Tower_SO tower_SO;

    private void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);

        SO_Reference();

    }

    void SO_Reference()
    {         
         range = tower_SO.range;
         rateOfFire = tower_SO.rateOfFire;
        innerRadius = tower_SO.innerRadius;
         projectilePrefab = tower_SO.projectilePrefab;
    }




    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        float shortestDistance = Mathf.Infinity;

        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(this.transform.position, enemy.transform.position);

            if(distanceToEnemy < shortestDistance && distanceToEnemy >= innerRadius)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if(nearestEnemy != null && shortestDistance <= range && shortestDistance >= innerRadius)
        {
            towerTarget = nearestEnemy.transform;
        }
        else
        {
            towerTarget = null;
        }
    }

    private void Update()
    {
        if(towerTarget == null)
        {
            return;
        }

        //Target Lock
        Vector3 direction = towerTarget.position - this.transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(pivot.rotation, lookRotation, rotationSpeed * Time.deltaTime).eulerAngles;
        pivot.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        //FIRING PART
        if(CountdownToNextFire <= 0f)
        {
            Shoot();
            CountdownToNextFire = 1f / rateOfFire;
        }

        CountdownToNextFire -= Time.deltaTime;
    }

    private void Shoot()
    {
        GameObject projectileGameObject = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);

        Missile missile = projectileGameObject.GetComponent<Missile>();

        if (missile != null)
        {
            missile.Seek(towerTarget);
        }

        //Bullet bullet = projectileGameObject.GetComponent<Bullet>();

        //if(bullet != null)
        //{
        //    bullet.Seek(towerTarget);
        //}
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, range);
        Gizmos.DrawWireSphere(this.transform.position, innerRadius);
        
    }
}

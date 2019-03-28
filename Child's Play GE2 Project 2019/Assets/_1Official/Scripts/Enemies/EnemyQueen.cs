using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AntChild
{
    [SerializeField] private GameObject antGO;
    [SerializeField] private float spawnWeight;

    public GameObject AntGO { get => antGO; }
    public float SpawnWeight { get => spawnWeight; }
}

public class EnemyQueen : Enemy
{
    [Header("Spawning options")]
    [SerializeField, Range(1, 5)] private float secBetweenEggLaying = 2.0f;
    [SerializeField, Range(5, 15)] private float eggHatchTime = 10.0f;
    [Header("Set weight in descending order, highest to lowest.")]
    [SerializeField] private AntChild[] antChildren;
    [SerializeField] private GameObject eggGO;

    new public void Start()
    {
        base.Start();
        InvokeRepeating("SpawningChildren", secBetweenEggLaying, secBetweenEggLaying);
    }

    public void SpawningChildren()
    {
        float randomNumber = Random.Range(0.0f, 100.0f);
        int spawnIndex = -1;

        float weightValue = 0;
        for (int i = 0; i < antChildren.Length; i++)
        {
            weightValue += antChildren[i].SpawnWeight;
            if (randomNumber <= weightValue)
            {
                spawnIndex = i;
                break;
            }
        }

        if (spawnIndex >= 0)
        {
            StartCoroutine(SpawnAnt(spawnIndex));
        }
        
    }

    private IEnumerator SpawnAnt(int spawnIndex)
    {
        const float DISTANCE_CAST = 1.5f;
        float leftDistance = DISTANCE_CAST;
        float rightDistance = DISTANCE_CAST;
        if (Physics.Raycast(this.transform.position + (this.transform.up), -this.transform.right, out RaycastHit hit, DISTANCE_CAST))
        {
            Debug.DrawLine(this.transform.position + (this.transform.up), hit.point, Color.blue, 3.0f);
            leftDistance = hit.distance - 0.5f;
        }
        else if (Physics.Raycast(this.transform.position + (this.transform.up), this.transform.right, out hit, DISTANCE_CAST))
        {
            Debug.DrawLine(this.transform.position + (this.transform.up), hit.point, Color.blue, 3.0f);
            rightDistance = hit.distance - 0.5f;
        }
        float randomPosition = Random.Range(-leftDistance, rightDistance);

        Vector3 spawnPosition = this.transform.position + (this.transform.right * randomPosition);
        Quaternion spawnRotation = this.transform.rotation;

        GameObject egg = Instantiate(eggGO, spawnPosition - this.transform.forward, spawnRotation, null);
        egg.GetComponent<Animator>().SetFloat("Speed", Random.Range(1 - 0.2f, 1 + 0.2f));
        egg.transform.Rotate(Vector3.up * Random.Range(-180.0f, 180.0f));
        float randomHatching = Random.Range(0, eggHatchTime) + eggHatchTime;
        Destroy(egg, randomHatching);

        yield return new WaitForSeconds(randomHatching);
        if (isDying)
        {
            yield break;
        }

        GameObject ant = Instantiate(antChildren[spawnIndex].AntGO, spawnPosition, spawnRotation, null);
        ant.transform.Rotate(Vector3.up * Random.Range(-180.0f, 180.0f));
        ant.GetComponentInChildren<Enemy>().EMMCode.SetNode(this.eMMCode.CurrentDestination);
        ant.GetComponentInChildren<EnemyMovementMechanics>().MoveToNode();

    }

    public override void Die()
    {
        if (isDying)
        {
            return;
        }
        var cols = GetComponents<Collider>();
        foreach (Collider col in cols)
        {
            col.enabled = false;
        }

        eMMCode.NavMeshAgent.enabled = false;
        isDying = true;
        SetAnimRetreating();
        Destroy(this.gameObject, 15);

        EnemyManager.GetInstance().RemoveEnemyFromList(this as Enemy);
        GameManager.GetInstance().MyMoney.MoneyChange(value);
        CancelInvoke("SpawningChildren");
    }
}

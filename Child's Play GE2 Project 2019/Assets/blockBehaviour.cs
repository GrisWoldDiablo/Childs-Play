using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class blockBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        NavMeshAgent enemy = other.GetComponent<NavMeshAgent>();

        enemy.isStopped = true;

    }

    private void OnTriggerStay(Collider other)
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject enemyO = other.game; 
        NavMeshAgent enemy = other.GetComponent<NavMeshAgent>();

        enemy.isStopped = false;

    }
}

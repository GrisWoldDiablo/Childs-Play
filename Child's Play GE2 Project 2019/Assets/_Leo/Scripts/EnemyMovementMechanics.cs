using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovementMechanics : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnemyAgentNaveMeshSetup()
    {
        this._navMeshAgent = GetComponent<NavMeshAgent>();

        this._navMeshAgent.speed = 3;
        this._navMeshAgent.angularSpeed = 1200;
        this._navMeshAgent.acceleration = 20;
    }

    //public
}

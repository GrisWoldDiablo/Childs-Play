<<<<<<< Updated upstream:Child's Play GE2 Project 2019/Assets/_Alex/Scripts/Item.cs
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private GameObject rangeGO;

    public GameObject RangeGO { get => rangeGO; set => rangeGO = value; }

    // Start is called before the first frame update
    void Awake()
    {
        if (rangeGO != null)
        {
            rangeGO.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
=======
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovementMechanics : MonoBehaviour
{

    private NavMeshAgent _navMeshAgent;

    [SerializeField]
    private Transform _finalDestination;
    // Start is called before the first frame update
    void Start()
    {
        EnemyAgentNaveMeshSetup();
        MoveToDestination();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnemyAgentNaveMeshSetup()
    {
        this._navMeshAgent = GetComponent<NavMeshAgent>();

        this._navMeshAgent.speed = 1;
        this._navMeshAgent.angularSpeed = 1200;
        this._navMeshAgent.acceleration = 20;
    }

    public void MoveToDestination()
    {
        this.transform.position = _navMeshAgent.nextPosition;
        this._navMeshAgent.SetDestination(_finalDestination.position);
    }

    //public
}
>>>>>>> Stashed changes:Child's Play GE2 Project 2019/Assets/_Leo/Scripts/EnemyMovementMechanics.cs

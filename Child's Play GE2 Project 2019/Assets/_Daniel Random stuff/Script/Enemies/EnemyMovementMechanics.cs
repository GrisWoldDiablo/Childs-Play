using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


/// <summary>
/// The enemy will move trought a list of nodes.
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovementMechanics : MonoBehaviour
{

    //Events
    public delegate void DestinationReached();
    public event DestinationReached OnFood;
    public event DestinationReached OnFinalNode; 

    //Game Object Components
    private NavMeshAgent _navMeshAgent;
    private Rigidbody _myRigidBody;
   
    //Node & Position
    protected Node currentDestination;
    protected Vector3 nextDestination;

    //Other
    private float initialMovementSpeed;
    private bool attacking;
    public float InitialMovementSpeed { get => initialMovementSpeed; }
    public bool Attacking { get => attacking; }

    // [SerializeField] private float damage = 1; No Longer Used.

    private float inititalStoppingDistance;
    [SerializeField] private float attackAvoidanceRadious = 0.09375f;
    [SerializeField] private float attackStopingDistance = 5f;
    [SerializeField] private float agentAvoidanceRadious = 0.15f;

    void Start()
    {
        currentDestination = GameObject.FindGameObjectWithTag("Spawn").GetComponent<Node>();
        EnemyAgentNaveMeshSetup();
        SetNode(currentDestination);
        _myRigidBody = gameObject.GetComponent<Rigidbody>();
        _myRigidBody.isKinematic = false;
        attacking = false;
    }

    void Update()
    {
        
        
    }

    /// <summary>
    /// Basic setup for navmesh agent
    /// </summary>
    public void EnemyAgentNaveMeshSetup()
    {  
        if (_navMeshAgent == null)
        {
            this._navMeshAgent = GetComponent<NavMeshAgent>();
            this._myRigidBody = GetComponent<Rigidbody>();
            _myRigidBody.isKinematic = true;
            //this._navMeshAgent.speed = 5;
            //this._navMeshAgent.velocity = new Vector3 (0, 0, 0);
            this._navMeshAgent.angularSpeed = 1200;
            this._navMeshAgent.acceleration = 20;
            this._navMeshAgent.radius = agentAvoidanceRadious; //THIS CONTROLS THE AGENT AVOIDANCE RADIUS.

            initialMovementSpeed = _navMeshAgent.speed;
            inititalStoppingDistance = _navMeshAgent.stoppingDistance;
        }
        
        this._navMeshAgent.enabled = true;
        this._navMeshAgent.isStopped = false;

    }

    /// <summary>
    /// Finds the next node in the path
    /// </summary>
    public void GetNextNode(Node currentlyEnteredNode)
    {
        //Don't do anything if the currentNode is not the same as the enteredNode.
        if (currentDestination != currentlyEnteredNode)
        {
            //Debug.LogError("Error Getting Next Node");
            return;
        }
        if (currentDestination == null)
        {
            //Debug.LogError("Cannot find current node");
            return;
        }

        Node nextNode = currentDestination.GetNextNode();
        if (nextNode == null)
        {
            //Debug.LogError("Next node is NULL");
            if (_navMeshAgent.enabled)
            {
                FinalNodeReached();
            }
            return;
        }

        Debug.Assert(nextNode != currentDestination);
        SetNode(nextNode);
        MoveToNode();
    }

    /// <summary>
    /// Sets the node to navigate to
    /// </summary>
    /// <param name="node">The node that the enemy will navigate to</param>
    public void SetNode(Node node)
    {
        //Debug.Log("We Are setting the node");
        currentDestination = node;
    }

    /// <summary>
    /// Make the enemy move towards the currentDestinationNode
    /// </summary>
    public void MoveToNode()
    {
        NavigateTo(currentDestination.transform.position);
    }

    /// <summary>
    /// Make the enemy move towards any transform passed on to it.
    /// </summary>
    public void NavigateTo(Vector3 nextPoint)
    {
        if (_navMeshAgent.isOnNavMesh)
        {
            //Debug.Log("Its time to move");
            _navMeshAgent.SetDestination(nextPoint);
        }
    }

    /// <summary>
    /// Enemy succesfully scapes with your food.
    /// </summary>
    internal void FinalNodeReached()
    { 
        _navMeshAgent.isStopped = true;
        //OnFinalNode(); //event
        Destroy(this.gameObject);
    }

    /// <summary>
    /// Damage Done to any wall.
    /// </summary>
    /// No Longer used.
    //public float AttackDamage()
    //{
    //    return damage;
    //}


    /// <summary>
    /// Movement behaviour when encountering a wall.
    /// </summary>
    public void StopAndGo()
    {
        if (_navMeshAgent.isStopped)
        {
            _navMeshAgent.isStopped = false;     
        }
        else
        {
            _navMeshAgent.isStopped = true;
        }
    }

    private void SetStoppingDistance(float d)
    {
        _navMeshAgent.stoppingDistance = d;
    }
    private void SetAvoidanceRadious(float s)
    {
        _navMeshAgent.radius = s;
    }

    public void AttackStance(Vector3 target)
    {
        this.NavigateTo(target);
        this.SetStoppingDistance(attackStopingDistance);
        this.SetAvoidanceRadious(attackAvoidanceRadious);
        this.attacking = true;
    }

    public void MoveOnStance()
    {
        this.MoveToNode();
        this.SetStoppingDistance(inititalStoppingDistance);
        this.SetAvoidanceRadious(agentAvoidanceRadious);
        this.attacking = false;
    }

}



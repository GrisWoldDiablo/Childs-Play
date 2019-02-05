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
   
    //Node & Position
    protected Node currentDestination;
    protected Vector3 nextDestination;

    //States
    public enum MovementStates {ClearRoad, BlockedRoad, Attacking, InFood}
    private MovementStates myStates;
    public MovementStates MyStates { get => myStates; protected set => myStates = value; }

    /// <summary>
    /// Returns true if the path is blocked, dictated by the NavMeshStatus property of the navMesh agent.
    /// </summary>
    private bool isPathBlocked { get { return _navMeshAgent.pathStatus == NavMeshPathStatus.PathPartial; } }


    //Other
    private float initialMovementSpeed;
    public float InitialMovementSpeed { get => initialMovementSpeed; }
  

    void Start()
    {
        currentDestination = GameObject.FindGameObjectWithTag("Spawn").GetComponent<Node>();
        EnemyAgentNaveMeshSetup();
        SetNode(currentDestination);
    }

    void Update()
    {
        StatusUpdate();
    }

    private void StatusUpdate()
    {
        switch (MyStates)
        {
            case MovementStates.ClearRoad:
                break;
            case MovementStates.BlockedRoad:
                break;
            case MovementStates.Attacking:
                break;
            case MovementStates.InFood:
                break;
            default:
                break;
        }
    }

    public void EnemyAgentNaveMeshSetup()
    {  
        if (_navMeshAgent == null)
        {
            this._navMeshAgent = GetComponent<NavMeshAgent>();
            this._navMeshAgent.speed = 5;
            //this._navMeshAgent.velocity = new Vector3 (0, 0, 0);
            this._navMeshAgent.angularSpeed = 1200;
            this._navMeshAgent.acceleration = 20;

            initialMovementSpeed = _navMeshAgent.speed;
            myStates = isPathBlocked ? MovementStates.BlockedRoad : MovementStates.ClearRoad;
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
            return;
        }
        if (currentDestination == null)
        {
            Debug.LogError("Cannot find current node");
            return;
        }

        Node nextNode = currentDestination.GetNextNode();
        if (nextNode == null)
        {
            Debug.LogError("Next node is NULL");
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
        Debug.Log("We Are setting the node");
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
            Debug.Log("Its time to move");
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
}



using System;
using System.Collections;
using System.Collections.Generic;
using Map.Nodes;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    /// <summary>
    /// The enemy will move trought a list of nodes.
    /// </summary>
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyMovementMechanics : MonoBehaviour
    {
        private NavMeshAgent _navMeshAgent;


		protected Node currentDestination;
        protected Vector3 nextDestination;

        public float initialMovementSpeed { get; private set; }


        void Start()
        {
            currentDestination = GameObject.FindGameObjectWithTag("Spawn").GetComponent<Node>();
            EnemyAgentNaveMeshSetup();
            SetNode(currentDestination);
        }

        void Update()
        {  
            /*
            // If the path becomes invalid, repath the agent to the destination.
            bool validStalePath = _navMeshAgent.isOnNavMesh && _navMeshAgent.enabled &&
                                  (!_navMeshAgent.hasPath && !_navMeshAgent.pathPending);

            // Compare against squared stopping distance on agent.
            // We intentionally do not pre-square this value so that it can be changed at runtime dynamically
            float squareStoppingDistance = _navMeshAgent.stoppingDistance * _navMeshAgent.stoppingDistance;
            if (validStalePath)
            {
                if (Vector3.SqrMagnitude(nextDestination - this.transform.position) < squareStoppingDistance && currentDestination.GetNextNode() != null)
                {
                    Debug.Log("stalled path Validated");
                    // Proceed if we're at our destination
                    GetNextNode(currentDestination);
                   
                }
                else
                {
                    // Otherwise try repath
                    _navMeshAgent.SetDestination(nextDestination);
                }   
            }
            */
        }

        public void EnemyAgentNaveMeshSetup()
        {
            if (_navMeshAgent == null)
            {
                this._navMeshAgent = GetComponent<NavMeshAgent>();
                this._navMeshAgent.speed = 3;
                this._navMeshAgent.angularSpeed = 1200;
                this._navMeshAgent.acceleration = 20;

                initialMovementSpeed = _navMeshAgent.speed;
            }
       
            this._navMeshAgent.enabled = true;
            this._navMeshAgent.isStopped = false;
        }

        /// <summary>
		/// Finds the next node in the path
		/// </summary>
		public void GetNextNode(Node currentlyEnteredNode)
        {
            //Don't do anything is the currentNode is not the same as the enteredNode.
            if (currentDestination != currentlyEnteredNode)
            {
                Debug.Log("Don't do anything if the calling node is the same as the m_CurrentNode");
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
                    _navMeshAgent.isStopped = true;
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
		/// <param name="node">The node that the agent will navigate to</param>
		public void SetNode(Node node)
        {
            Debug.Log("We Are setting the node");
            currentDestination = node;
        }

        /// <summary>
		/// Moves the enemy to a position in the currentDestionation --> Meant to be random
		/// </summary>
		public void MoveToNode()
        {
            Vector3 nodePosition = currentDestination.transform.position; //To be changed for random point nearby.
            nodePosition.y = currentDestination.transform.position.y;
            currentDestination.transform.position = nodePosition;
            NavigateTo(currentDestination.transform.position);
        }

        protected void NavigateTo(Vector3 nextPoint)
        {
            //LazyLoad();
            if (_navMeshAgent.isOnNavMesh)
            {
                Debug.Log("Its time to move");
                _navMeshAgent.SetDestination(nextPoint);
            }
        }
    }
}


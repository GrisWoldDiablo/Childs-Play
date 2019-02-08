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
        var enemy = other.gameObject.GetComponent<EnemyMovementMechanics>();    
        enemy.StopAndGo();
    }
       

    private void OnTriggerStay(Collider other)
    {
        var enemy = other.gameObject.GetComponent<EnemyMovementMechanics>();
        enemy.AttackWall();
    }

    private void OnTriggerExit(Collider other)
    {
        var enemy = other.gameObject.GetComponent<EnemyMovementMechanics>();
        enemy.StopAndGo();
    }
}

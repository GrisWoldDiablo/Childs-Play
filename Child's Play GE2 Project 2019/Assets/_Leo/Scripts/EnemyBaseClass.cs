using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(
    typeof(EnemyAnimation),
    typeof(EnemyMovementMechanics),
    typeof(Rigidbody)
    )]
public class EnemyBaseClass : MonoBehaviour
{
    public bool hasFocus = false;
    private EnemyAnimation _enemyAnimation;
    // Start is called before the first frame update
    void Start()
    {
        _enemyAnimation = GetComponent<EnemyAnimation>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetAnimWalking()
    {
        _enemyAnimation.SetWalking();
    }

    public void SetAnimAttacking()
    {
        _enemyAnimation.SetAttacking();
    }

    public void SetAnimDizzy()
    {
        _enemyAnimation.SetDizzy();
    }

    public void SetAnimRetreating()
    {
        _enemyAnimation.SetRetreating();
    }

    public void Kill()
    {
        this.SetAnimRetreating();
        Destroy(this.gameObject, 5);
    }
    
}

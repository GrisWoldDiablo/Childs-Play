using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimation : MonoBehaviour
{
    private Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetWalking()
    {
        _animator.SetTrigger("Walking");
    }

    public void SetAttacking()
    {
        _animator.SetTrigger("Attacking");
    }

    public void SetDizzy()
    {
        _animator.SetTrigger("Dizzy");
    }

    public void SetRetreating()
    {
        _animator.SetTrigger("Retreating");
    }

}

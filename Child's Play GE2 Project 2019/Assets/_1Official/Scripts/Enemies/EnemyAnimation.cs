using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimation : MonoBehaviour
{
    private Animator _animator;
    [SerializeField,Range(0.21f,2.0f)] private float animSpeedRandom = 1;
    private const float randSpeedBound = 0.2f;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _animator.SetFloat("Speed", Random.Range(animSpeedRandom - randSpeedBound, animSpeedRandom + randSpeedBound));
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

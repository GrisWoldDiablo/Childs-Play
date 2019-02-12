using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(
    typeof(EnemyAnimation),
    typeof(EnemyMovementMechanics)
    )]
public class EnemyBaseClass : MonoBehaviour
{
    public bool hasFocus = false;
    private EnemyAnimation _enemyAnimation;

    [SerializeField] private Item target;
    private GameObject targetGO;
    [SerializeField] protected bool isAttacking = false;
    [SerializeField] private int damage = 1;
    [SerializeField] private float attackSpeed = 0.5f;
    [SerializeField] private float attackCountDown = 0;

    private EnemyMovementMechanics eMMCode;

    private void Awake()
    {
        _enemyAnimation = GetComponent<EnemyAnimation>();
        eMMCode = GetComponent<EnemyMovementMechanics>();
    }

    // Start is called before the first frame update
    void Start()
    {
        SetAnimWalking();
    }

    // Update is called once per frame
    protected void Update()
    {
        if (this.isAttacking)
        {
            Attack();
        }
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

    public void Die()
    {
        SetAnimRetreating();
        Destroy(this.gameObject, 5);
    }

    public void SetAttacking(Item target)
    {
        isAttacking = true;
        this.targetGO = target.gameObject;
        this.target = target;
        SetAnimAttacking();
        eMMCode.AttackStance(target.transform.position);
    }

    public void Attack()
    {
        if (targetGO == null)
        {
            target = null;
            isAttacking = false;
            ResumeWalking();
            return;
        }

        #region Attack Counter
        if (attackCountDown > 0f)
        {
            attackCountDown -= Time.deltaTime;
            return;
        }
        attackCountDown = attackSpeed;
        #endregion

        target.Health -= damage;

    }

    private void ResumeWalking()
    {
        SetAnimWalking();
        eMMCode.MoveOnStance();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (targetGO == null)
        {
            if (other.CompareTag("Item"))
            {
                SetAttacking(other.GetComponent<Item>());
            }
        }
    }


}

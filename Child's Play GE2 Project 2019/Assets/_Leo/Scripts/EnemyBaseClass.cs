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
    [SerializeField] private int hitPoints = 100;
    [SerializeField] private int foodBites = 5;
    private bool isDying = false;
    [SerializeField] private bool asEaten = false;

    private EnemyMovementMechanics eMMCode;

    public int HitPoints { get => hitPoints; set => hitPoints = value; }
    public bool IsDying { get => isDying; }

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
        if (hitPoints <= 0)
        {
            Die();
        }
        else if (this.isAttacking)
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
        if (isDying)
        {
            return;
        }
        var cols = GetComponents<Collider>();
        foreach (Collider col in cols)
        {
            col.enabled = false;
        }
        transform.Rotate(Vector3.up * Random.Range(-360, 360));
        eMMCode.StopAndGo();
        isDying = true;
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

        target.HitPoints -= damage;

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
        if (other.CompareTag("Food"))
        {
            if (!asEaten)
            {
                EatFood(other.GetComponent<Food>());
            } 
        }
    }

    private void EatFood(Food _food)
    {
        asEaten = true;
        _food.HitPoints -= foodBites;
        //Debug.Log(_food.HitPoints);
    }


}

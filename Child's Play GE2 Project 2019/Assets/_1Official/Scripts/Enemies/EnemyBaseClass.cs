using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(
    typeof(EnemyAnimation),
    typeof(EnemyMovementMechanics)
    )]
public class EnemyBaseClass : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int hitPoints = 100;
    [SerializeField] private int damage = 1;
    [SerializeField] private float attackSpeed = 0.5f;
    [SerializeField] private int foodBites = 5;
    [SerializeField] private int value = 10;

    protected bool hasFocus = false;
    private EnemyAnimation _enemyAnimation;
    private Item target;
    private GameObject targetGO;
    protected bool isAttacking = false;

    private float attackCountDown = 0;
    private bool isDying = false;
    private bool asEaten = false;

    private EnemyMovementMechanics eMMCode;

    public int HitPoints { get => hitPoints; set => hitPoints = value; }
    public bool IsDying { get => isDying; }
    public bool HasFocus { get => hasFocus; set => hasFocus = value; }

    private int currentDamageOvertime;

    [Header("Health Bar")]
    [SerializeField] private Color startingHealthColor;
    [SerializeField] private Color endHealthColor;
    private Image healthBar;
    private float ogHP;
    public Image HealthBar { get => healthBar; set => healthBar = value; }

    private void Awake()
    {
        _enemyAnimation = GetComponent<EnemyAnimation>();
        eMMCode = GetComponent<EnemyMovementMechanics>();
    }

    // Start is called before the first frame update
    protected void Start()
    {
        SetAnimWalking();

        EnemyManager.GetInstance().AddEnemyToList(this as Enemy);
        healthBar = GetComponentInChildren<Image>();
        healthBar.gameObject.SetActive(GameManager.GetInstance().ShowHealthBars);
        ogHP = hitPoints;
    }

    // Update is called once per frame
    protected void Update()
    {
        if (this.isAttacking)
        {
            Attack();
        }
    }

    public void TakeDamage(int damageValue)
    {
        this.hitPoints -= damageValue;
        UpdateHealthBar();
        if (hitPoints <= 0)
        {
            Die();
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
        //eMMCode.StopAndGo();
        eMMCode.NavMeshAgent.enabled = false;
        isDying = true;
        SetAnimRetreating();
        Destroy(this.gameObject, 5);
        transform.Rotate(Vector3.up * Random.Range(-180, 180));
        EnemyManager.GetInstance().RemoveEnemyFromList(this as Enemy);
        GameManager.GetInstance().MyMoney.MoneyChange(value);
    }

    public virtual void SetAttacking(Item target)
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
            attackCountDown = 0f;
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

        target.TakeDamage(damage);

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
        _food.TakeDamage(foodBites);
    }

    public void LeaveWithFood()
    {
        EnemyManager.GetInstance().RemoveEnemyFromList(this as Enemy);
        Destroy(this.gameObject);
    }

    public void DamageOverTime(int damageValue, float tickSpeed, float lastTime)
    {
        StartCoroutine(DamageOverTimeRoutine(damageValue, tickSpeed, lastTime));
    }

    private IEnumerator DamageOverTimeRoutine(int damageValue, float tickSpeed, float lastTime)
    {
        float currentTime = Time.time;
        float endTime = currentTime + lastTime;
        while (currentTime <= endTime && this.hitPoints > 0)
        {
            this.TakeDamage(damageValue);
            yield return new WaitForSeconds(tickSpeed);
            currentTime = Time.time;
        }
    }

    private void UpdateHealthBar()
    {
        if (healthBar == null)
        {
            return;
        }
        healthBar.fillAmount = hitPoints / ogHP;
        healthBar.color = Color.Lerp(endHealthColor, startingHealthColor, healthBar.fillAmount);
    }
}

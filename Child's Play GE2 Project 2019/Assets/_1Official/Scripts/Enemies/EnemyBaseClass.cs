using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    //private GameManager gmCode;


    public IEnumerator DamageOverTime(int damageValue, float tickSpeed, float lastTime)
    {
        float currentTime = Time.time;
        float endTime = currentTime + lastTime;
        while (currentTime <= endTime)
        {
            //Debug.Log($"{gameObject.GetInstanceID()} Take DOT:{damageValue}, HP:{this.hitPoints}");
            this.TakeDamage(damageValue);
            //Debug.Log($"{gameObject.GetInstanceID()} wait for {tickSpeed}");
            yield return new WaitForSeconds(tickSpeed);
            Debug.Log($"{gameObject.GetInstanceID()} Next Tick");
            currentTime = Time.time;
        }
    }

    private void Awake()
    {
        _enemyAnimation = GetComponent<EnemyAnimation>();
        eMMCode = GetComponent<EnemyMovementMechanics>();
        //gmCode = GameObject.FindObjectOfType<GameManager>();
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

    public void TakeDamage(int damageValue)
    {
        this.hitPoints -= damageValue;
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
        transform.Rotate(Vector3.up * Random.Range(-360, 360));
        eMMCode.StopAndGo();
        isDying = true;
        SetAnimRetreating();
        Destroy(this.gameObject, 5);
        GameManager.GetInstance().MyMoney.MoneyChange(value);
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


}

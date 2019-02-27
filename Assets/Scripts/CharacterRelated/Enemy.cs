using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : NPC
{
    private static Enemy instance;

    public static Enemy MyInstance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<Enemy>();

            return instance;
        }
    }

    [SerializeField]
    private CanvasGroup healthGroup;
    [SerializeField]
    private float initAggroRange;

    public int enemyDamage;

    //od tu
    [SerializeField]
    private string enemyQuestName;
    private QuestManager questmanager;
    private bool isCounted = false;
    
    //do tu

    private IStates currentState;

    [SerializeField]
    private LootTable lootTable;

    public float MyAttackRange { get; set; }
    public float MyAttackTime { get; set; }
    public float MyAggroRange { get; set; }
    public Vector3 MyStartPosition { get; set; }

    public bool InRange
    {
        get
        {
            return Vector2.Distance(transform.position, MyTarget.position) < MyAggroRange;
        }
    }

    protected void Awake()
    {
        MyStartPosition = transform.position;
        MyAggroRange = initAggroRange;
        MyAttackRange = 0.8f;
        ChangeState(new IdleState());
    }

    protected override void Start()
    {
        questmanager = FindObjectOfType<QuestManager>();
        base.Start();
    }

    protected override void Update()
    {
        if (IsAlive)
        {
            if (!IsAttacking)
            {
                MyAttackTime += Time.deltaTime;
            }

            currentState.Update();
        }

        if (!IsAlive && !isCounted)
        {
            questmanager.MyKilledEnemy = enemyQuestName;
            isCounted = true;
        }

        base.Update();
    }

    public override Transform Select()
    {
        healthGroup.alpha = 1;
        return base.Select();
    }

    public override void DeSelect()
    {
        healthGroup.alpha = 0;
        base.DeSelect();
    }

    public override void TakeDamage(float damage, Transform source)
    {
        if (!(currentState is EvadeState))
        {
            SetTarget(source);
            base.TakeDamage(damage, source);
            OnHealthChanged(health.MyCurrentValue);
        }
    }

    public void ChangeState(IStates newState)
    {
        if (currentState != null)
            currentState.Exit();

        currentState = newState;
        currentState.Enter(this);

    }

    public void SetTarget(Transform target)
    {
        if (MyTarget == null && !(currentState is EvadeState))
        {
            float distance = Vector2.Distance(transform.position, target.position);
            MyAggroRange = initAggroRange;
            MyAggroRange += distance;
            MyTarget = target;
        }
    }

    public void Reset()
    {

        this.MyTarget = null;
        this.MyAggroRange = initAggroRange;
        this.MyHealth.MyCurrentValue = this.MyHealth.MyMaxValue;
        OnHealthChanged(health.MyCurrentValue);
    }

    public override void Interact()
    {
        if (!IsAlive)
        {
            lootTable.ShowLoot();
        }
    }

    public override void StopInteract()
    {
        LootWindow.MyInstance.Close();
    }
}

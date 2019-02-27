using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]

public abstract class Character : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    protected Stats health;

    public Transform MyTarget { get; set; }

    public Stats MyHealth { get { return health; } }

    [SerializeField]
    private float initHealth;

    private Rigidbody2D myRigidbody;

    private Vector2 direction;
    protected Coroutine attackRoutine;

    public Animator MyAnimator { get; set; }

    public bool IsAttacking{ get; set; }

    [SerializeField]
    protected Transform hitBox;

    public bool IsMoving
    {   get { return Direction.x != 0 || Direction.y != 0; }   }

    public Vector2 Direction
    {
        get
        {
            return direction;
        }

        set
        {
            direction = value;
        }
    }

    public float MySpeed
    {
        get
        {
            return speed;
        }

        set
        {
            speed = value;
        }
    }

    public bool IsAlive { get { return health.MyCurrentValue > 0; } }

    // Use this for initialization - overriden
    protected virtual void Start ()
    {

        health.Initialize(initHealth, initHealth);
        myRigidbody = GetComponent<Rigidbody2D>();
        MyAnimator = GetComponent<Animator>();
	}
	
	// Update is called once per frame - overriden
	protected virtual void Update ()
    {
        HandleLayers();     
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        if (IsAlive)
            myRigidbody.velocity = Direction.normalized * MySpeed;
    }

    public virtual void HandleLayers()
    {
        if (IsAlive)
        {
            //provjerava krecemo li se
            if (IsMoving)
            {
                //salje u walk
                ActivateLayer("WalkLayer");
                MyAnimator.SetFloat("MovingX", Direction.x);
                MyAnimator.SetFloat("MovingY", Direction.y);
            }
            else if (IsAttacking)
            {
                //salje u attack
                ActivateLayer("AttackLayer");
            }
            else
            {
                //vraca u idle
                ActivateLayer("IdleLayer");
            }
        }
        else
        {
            ActivateLayer("DeathLayer");
        }
    }

    public virtual void ActivateLayer(string layerName)
    {
        for (int i = 0; i < MyAnimator.layerCount; i++)
            MyAnimator.SetLayerWeight(i, 0);

        MyAnimator.SetLayerWeight(MyAnimator.GetLayerIndex(layerName), 1);
    }

    public virtual void TakeDamage(float damage, Transform source)
    {
        health.MyCurrentValue -= damage;

        if (health.MyCurrentValue <= 0)
        {
            Direction = Vector2.zero;
            myRigidbody.velocity = Direction;
            MyAnimator.SetTrigger("Die");
        }
    }
}

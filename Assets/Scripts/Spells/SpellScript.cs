using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellScript : MonoBehaviour
{
    int damage;
    private Rigidbody2D spellsRigidbody;
    public Transform MyTarget { get; private set; }
    private Transform source;

    [SerializeField]
    private float spellSpeed;

	// Use this for initialization
	void Start ()
    {
        spellsRigidbody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void Initialized(Transform target, int dmg, Transform sour)
    {
        this.MyTarget = target;
        this.damage = dmg;
        this.source = sour;
    }

    private void FixedUpdate()
    {
        if (MyTarget != null)
        {
            Vector2 direction = MyTarget.position - transform.position;
            spellsRigidbody.velocity = direction.normalized * spellSpeed;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "SpellHitBox" && collision.transform == MyTarget) //da enemyji mogu blokati reba removati poslje && sve sto se nalazi
        {
            Character c = collision.GetComponentInParent<Enemy>();
            spellSpeed = 0;
            c.TakeDamage(damage, source);
            GetComponent<Animator>().SetTrigger("Impact");
            spellsRigidbody.velocity = Vector2.zero;
            MyTarget = null;
        }
    }
}

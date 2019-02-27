using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private static Player instance;

    public static Player MyInstance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<Player>();

            return instance;
        }
    }

    //DODANO

    private int armor = 0, intelect = 0, stamina = 0, strenght = 0;

    //DO TU

    [SerializeField]
    private Stats mana;

    [SerializeField]
    private Transform[] exitPoints;

    [SerializeField]
    private Block[] blocks;

    private float initMana = 80;
    private int exitIndex = 1;
    private Vector3 min, max;
    private IInteractable interactable;

    [SerializeField]
    private GearSocket[] gearSockets;

    public int MyGold { get; set; }

    public IInteractable MyInteractable
    {
        get
        {
            return interactable;
        }

        set
        {
            interactable = value;
        }
    }

    public int MyArmor
    {
        get
        {
            return armor;
        }

        set
        {
            armor = value;
        }
    }

    public Stats MyMana
    {
        get
        {
            return mana;
        }

        set
        {
            mana = value;
        }
    }


    // Use this for initialization
    protected override void Start()
    {
        //tu bilo ovo
        MyGold = 500;
        MyMana.Initialize(initMana, initMana);
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        GetInput();

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, min.x, max.x), Mathf.Clamp(transform.position.y, min.y, max.y), transform.position.z);
        base.Update();
    }

    private void GetInput()
    {
        Direction = Vector2.zero;
        if (Input.GetKey(KeybindManager.MyInstance.Keybinds["Up"]))
        {
            exitIndex = 0;
            Direction += Vector2.up;
        }
        else if (Input.GetKey(KeybindManager.MyInstance.Keybinds["Down"]))
        {
            exitIndex = 1;
            Direction += Vector2Int.down;
        }
        else if (Input.GetKey(KeybindManager.MyInstance.Keybinds["Left"]))
        {
            exitIndex = 2;
            Direction += Vector2.left;
        }
        else if (Input.GetKey(KeybindManager.MyInstance.Keybinds["Right"]))
        {
            exitIndex = 3;
            Direction += Vector2.right;
        }

        if (IsMoving)
            StopAttack();

        foreach (string action in KeybindManager.MyInstance.ActionBinds.Keys)
        {
            if (Input.GetKeyDown(KeybindManager.MyInstance.ActionBinds[action]))
            {
                UIManager.MyInstance.ClickActionButton(action);
            }
        }

        ////testiranje
        //if (Input.GetKeyDown(KeyCode.I))
        //    health.MyCurrentValue -= 10;
        //if (Input.GetKeyDown(KeyCode.O))
        //    health.MyCurrentValue += 10;
        //if (Input.GetKeyDown(KeyCode.K))
        //    MyMana.MyCurrentValue -= 10;
        //if (Input.GetKeyDown(KeyCode.L))
        //    MyMana.MyCurrentValue += 10;
    }

    public void SetLimits(Vector3 min, Vector3 max)
    {
        this.min = min; this.max = max;
    }

    private IEnumerator Attack(string spellName)
    {
        if (mana.MyCurrentValue >= 10)
        {
            Transform currentTarget = MyTarget;
            Spell newSpell = SpellBook.MyInstance.CastSpell(spellName);
            IsAttacking = true;

            //dodano
            if (spellName == "FireArrow")
            {
                MyMana.MyCurrentValue -= 20;
            }
            else if (spellName == "FrostArrow")
            {
                MyMana.MyCurrentValue -= 15;
            }
            else if (spellName == "LightningArrow")
            {
                MyMana.MyCurrentValue -= 10;
            }
            else if (spellName == "FireSlash")
            {
                MyMana.MyCurrentValue -= 20;
            }
            else if (spellName == "FrostSlash")
            {
                MyMana.MyCurrentValue -= 15;
            }
            else if (spellName == "LightningSlash")
            {
                MyMana.MyCurrentValue -= 10;
            }
            else if (spellName == "FireBall")
            {
                MyMana.MyCurrentValue -= 10;
            }
            else if (spellName == "FrostBolt")
            {
                MyMana.MyCurrentValue -= 15;
            }
            else if (spellName == "LightningStrike")
            {
                MyMana.MyCurrentValue -= 10;
            }

            MyAnimator.SetBool("Attack", IsAttacking);

            foreach (GearSocket g in gearSockets)
            {
                g.MyAnimator.SetBool("Attack", IsAttacking);
            }

            yield return new WaitForSeconds(newSpell.MyCastTime);

            if (currentTarget != null && InLineOfSight())
            {
                SpellScript oneOfSpells = Instantiate(newSpell.MySpellPrefab, exitPoints[exitIndex].position, Quaternion.identity).GetComponent<SpellScript>();
                oneOfSpells.Initialized(currentTarget, newSpell.MyDamage, transform);
            }

            //SpellScript oneOfSpells = Instantiate(newSpell.MySpellPrefab, exitPoints[exitIndex].position, Quaternion.identity).GetComponent<SpellScript>();
            // oneOfSpells.MyTarget = PlayerTarget;

            StopAttack();
        }
    }

    public void CastSpell(string spellName)
    {
        Block();

        if (MyTarget != null && MyTarget.GetComponentInParent<Character>().IsAlive && !IsAttacking && !IsMoving && InLineOfSight())
        {
            attackRoutine = StartCoroutine(Attack(spellName));
        }
    }

    private bool InLineOfSight()
    {
        if (MyTarget != null)
        {
            Vector3 targetDirection = (MyTarget.transform.position - transform.position).normalized;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, targetDirection, Vector2.Distance(transform.position, MyTarget.transform.position), 256);

            if (hit.collider == null)
                return true;
        }

        return false;
    }

    private void Block()
    {
        foreach (Block b in blocks)
            b.Deactivate();

        blocks[exitIndex].Activate();
    }

    public void StopAttack()
    {

        SpellBook.MyInstance.StopCasting();

        IsAttacking = false;
        MyAnimator.SetBool("Attack", IsAttacking);

        foreach (GearSocket g in gearSockets)
        {
            g.MyAnimator.SetBool("Attack", IsAttacking);
        }

        if (attackRoutine != null)
        {
            StopCoroutine(attackRoutine);
        }
    }

    public override void HandleLayers()
    {
        base.HandleLayers();

        if (IsMoving)
        {
            foreach (GearSocket g in gearSockets)
            {
                g.SetXAndY(Direction.x, Direction.y);
            }
        }
    }

    public override void ActivateLayer(string layerName)
    {
        base.ActivateLayer(layerName);

        foreach (GearSocket g in gearSockets)
        {
            g.ActivateLayer(layerName);
        }
    }

    public void Interact()
    {
        if (MyInteractable != null)
        {
            MyInteractable.Interact();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" || collision.tag == "Interactable")
        {
            MyInteractable = collision.GetComponent<IInteractable>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" || collision.tag == "Interactable")
        {
            if (MyInteractable != null)
            {
                MyInteractable.StopInteract();
                MyInteractable = null;
            }
        }
    }
}

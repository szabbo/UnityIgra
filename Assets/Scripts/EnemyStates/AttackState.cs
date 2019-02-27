using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IStates 
{
    private Enemy parent;
    private float attackCooldown = 3;
    private float extraRange = 0.1f;

    private Enemy testEn;

    void Start()
    {
        testEn = MonoBehaviour.FindObjectOfType<Enemy>();
    }

    public void Enter(Enemy parent)
    {
        this.parent = parent;
    }

    public void Exit()
    {

    }

    public void Update()
    {
        if (Player.MyInstance.MyHealth.MyCurrentValue <= 0)
        {
            //Time.timeScale = 0f;
            //Debug.Log("Umro si konjino glupa :P!");
            DeathScreen.MyInstance.ActivateDeathScreen();
        }
        else if (parent.MyAttackTime >= attackCooldown && !parent.IsAttacking)
        {
            parent.MyAttackTime = 0;
            parent.StartCoroutine(Attack());
            //Player.MyInstance.MyHealth.MyCurrentValue -= EnemyDmg.MyInstance.MyEnemy.enemyDamage;
            //Player.MyInstance.MyHealth.MyCurrentValue -= 20;
            //Player.MyInstance.MyHealth.MyCurrentValue -= testEn.enemyDamage;
            if(parent.gameObject.name == "Enemy1")
            {
                Player.MyInstance.MyHealth.MyCurrentValue -= 20;
            }
            else if (parent.gameObject.name == "Enemy2")
            {
                Player.MyInstance.MyHealth.MyCurrentValue -= 25;
            }
            else if (parent.gameObject.name == "Enemy3")
            {
                Player.MyInstance.MyHealth.MyCurrentValue -= 30;
            }
            else if (parent.gameObject.name == "Enemy4")
            {
                Player.MyInstance.MyHealth.MyCurrentValue -= 35;
            }
            else if (parent.gameObject.name == "Enemy5")
            {
                Player.MyInstance.MyHealth.MyCurrentValue -= 35;
            }
            else if (parent.gameObject.name == "Enemy6")
            {
                Player.MyInstance.MyHealth.MyCurrentValue -= 35;
            }
            else if (parent.gameObject.name == "Enemy7")
            {
                Player.MyInstance.MyHealth.MyCurrentValue -= 40;
            }
            else if (parent.gameObject.name == "Enemy8")
            {
                Player.MyInstance.MyHealth.MyCurrentValue -= 40;
            }
            else if (parent.gameObject.name == "Enemy9")
            {
                Player.MyInstance.MyHealth.MyCurrentValue -= 40;
            }
            else if (parent.gameObject.name == "Enemy10")
            {
                Player.MyInstance.MyHealth.MyCurrentValue -= 45;
            }
        }

        if (parent.MyTarget != null)
        {
            float distance = Vector2.Distance(parent.MyTarget.position, parent.transform.position);

            if (distance >= parent.MyAttackRange + extraRange && !parent.IsAttacking)
                parent.ChangeState(new FollowState());
        }
        else
        {
            parent.ChangeState(new IdleState());
        }
    }

    public IEnumerator Attack()
    {
        parent.IsAttacking = true;
        parent.MyAnimator.SetTrigger("Attack");

        yield return new WaitForSeconds(parent.MyAnimator.GetCurrentAnimatorStateInfo(2).length);

        parent.IsAttacking = false;
    }
}

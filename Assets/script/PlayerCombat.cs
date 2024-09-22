using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
   public Animator animator;
    // Update is called once per frame

    public Transform AttackPoint;
    public LayerMask EnemyLayer;

    public float AttackRange = 0.5f;
    public int attackDamage = 40;

    public float attackRate = 3f;
    float nextAttackTime = 0f;


    void Update()
    {   
        if (Time.time >= nextAttackTime) 
        {
            if(Input.GetKeyDown(KeyCode.Space))
                {
                    Attack();
                    nextAttackTime = Time.time + 1f / attackRate;
                    

                }
        }

    }

    void Attack()
    {
        //play an attack animation
        animator.SetTrigger("Attack");

        //detect enemies ir range of attack
        Collider [] hitEnemies = Physics.OverlapSphere (AttackPoint.position, AttackRange, EnemyLayer);


        //damage them   
        foreach (Collider enemy in hitEnemies)
        {
           enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }

    }

    private void OnDrawGizmos()
    {
        if(AttackPoint == null) 
            return;

        Gizmos.DrawWireSphere(AttackPoint.position, AttackRange);
    }

}

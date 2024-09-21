using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
   public Animator animator;
    // Update is called once per frame

    public Transform AttackPoint;
    public float AttackRange = 0.5f;
    public LayerMask EnemyLayer;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
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
            Debug.Log(" Aray ko boss " + enemy.name);
        }

    }

    private void OnDrawGizmos()
    {
        if(AttackPoint == null) 
            return;

        Gizmos.DrawWireSphere(AttackPoint.position, AttackRange);
    }

}

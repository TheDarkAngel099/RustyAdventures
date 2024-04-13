using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform[] patrolPoints;
    public int currentPatrolPoint;
    public Animator anim;

    public enum AIState
    {
        Idle , 
        Patrolling,

        Chasing,
        Attacking
    };
    public AIState currentState;
    public float waitAtPoint = 2f;
    private float waitCounter ;

    public float chaseRange;

    public float attackRange = 1;
    public float timeBetweenAttacks = 1;
    private float attackCounter;

   
 

    void Start()
    {

        waitCounter = waitAtPoint;
        
    }



    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, PlayerController.instance.transform.position);


        switch (currentState)
        {
            case AIState.Idle:
                anim.SetBool("IsMoving", false);

                if (waitCounter >0 )
                {
                    waitCounter -= Time.deltaTime;
                }
                else
                {
                    currentState = AIState.Patrolling;
                    agent.SetDestination(patrolPoints[currentPatrolPoint].position);

                }

                if(distanceToPlayer <= chaseRange)
                {
                    currentState = AIState.Chasing;
                    
                }

               
                break;

            case AIState.Patrolling:


                if(agent.remainingDistance <= .2f)
                {
                    currentPatrolPoint++;
                    if(currentPatrolPoint >= patrolPoints.Length)
                    {
                        currentPatrolPoint = 0;
                    }

                    currentState = AIState.Idle;
                    waitCounter = waitAtPoint;

                }

                anim.SetBool("IsMoving", true);

                if(distanceToPlayer <= chaseRange)
                {
                    currentState = AIState.Chasing;
                }

                break;  

            
            case AIState.Chasing:

                anim.SetBool("IsMoving", true);
                agent.SetDestination(PlayerController.instance.transform.position);

                if(distanceToPlayer <= attackRange)
                {
                    currentState = AIState.Attacking;
                    anim.SetTrigger("Attack");
                    anim.SetBool("IsMoving", false);
                    agent.velocity = Vector3.zero;
                    agent.isStopped = true;

                    attackCounter = timeBetweenAttacks;

                }

                if (distanceToPlayer > chaseRange)
                {
                    currentState = AIState.Idle;
                    waitCounter = waitAtPoint;
                    agent.velocity = Vector3.zero;
                    agent.SetDestination(transform.position);
                    

                }


                break;  

            case AIState.Attacking:

                transform.LookAt(PlayerController.instance.transform, Vector3.up);
                transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);

                attackCounter -= Time.deltaTime;
                if(attackCounter <= 0)
                {
                    if(distanceToPlayer < attackRange)
                    {
                        anim.SetTrigger("Attack");
                        attackCounter = timeBetweenAttacks;
                    }
                    else
                    {
                        currentState = AIState.Idle;
                        waitCounter = waitAtPoint;

                        agent.isStopped = false;
                        
                    }

                }


                break;   
        }
             
        
    }

    
}

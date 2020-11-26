using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; //needed?

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float chaseRange = 5f;

    NavMeshAgent navMeshAgent;

    float distanceToTarget = Mathf.Infinity; // to prevent enemy from seeing 0 as dtt
    bool isProvoked = false;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        distanceToTarget = Vector3.Distance(target.position, transform.position);
        if (isProvoked)
        {
            engageTarget();
        }
        else if (distanceToTarget <= chaseRange)
        {
            isProvoked = true;
        }
    }

    private void engageTarget()
    {
        if (distanceToTarget >= navMeshAgent.stoppingDistance) 
        {
            chaseTarget();
        }

        if (distanceToTarget <= navMeshAgent.stoppingDistance) //if close to target
        {
            attackTarget();
        }
    }

    private void chaseTarget()
    {
        GetComponent<Animator>().SetBool("attack", false);
        GetComponent<Animator>().SetTrigger("move");
        navMeshAgent.SetDestination(target.position);
    }


    private void attackTarget()
    {
        GetComponent<Animator>().SetBool("attack", true);
        Debug.Log(name + " has seeked and is destroying " + target.name);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;// or new Color(1, 1, 0, 0.75F);
        Gizmos.DrawWireSphere(transform.position, chaseRange); //midpoint, radius
    }

}
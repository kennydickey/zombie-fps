using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; //needed?

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float chaseRange = 5f;
    [SerializeField] float turnSpeed = 5f;

    NavMeshAgent navMeshAgent;

    float distanceToTarget = Mathf.Infinity; // to prevent enemy from seeing 0 as dtt
    bool isProvoked = false;

    EnemyHealth health; //declaring use of enemy health script

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        health = GetComponent<EnemyHealth>(); //
    }

    void Update()
    {
        if (health.IsDead())
        {
            //turns off enemy component when dead
            enabled = false; //this.enabled, the "this" (enemyAI), is implied
            navMeshAgent.enabled = false; //let navMesh know it is dead and to stop running
        }
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

    public void OnDamageTaken()
    {
        isProvoked = true;
    }

    private void engageTarget()
    {
        FaceTarget();
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
    }

    private void FaceTarget()
    {
        //normalized returns data with length as 1, or 0 if too small
        Vector3 direction = (target.position - transform.position).normalized;
        //extract values from calculated direction to get rotation val!
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        //rotate smoothly between 2 vectors using current rotate, look rotate, and speed in time
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;// or new Color(1, 1, 0, 0.75F);
        Gizmos.DrawWireSphere(transform.position, chaseRange); //midpoint, radius
    }

}
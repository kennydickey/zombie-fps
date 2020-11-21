using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; //needed?

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float chaseRange = 20f;

    NavMeshAgent navMeshAgent;

    float distanceToTarget = Mathf.Infinity; // to prevent enemy from seeing 0 as dtt


    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        distanceToTarget = Vector3.Distance(target.position, transform.position);
        if (distanceToTarget <= chaseRange)
        {
        navMeshAgent.SetDestination(target.position);
        }
    }
}

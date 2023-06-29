using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class minimapicon : MonoBehaviour
{
    private Transform target;
    private NavMeshAgent navAgent;

    public void Awake()
    {
        target = GameObject.FindGameObjectWithTag("cart").transform;
        navAgent = GetComponent<NavMeshAgent>();
    }

    public void Update()
    {
        navAgent.SetDestination(target.position);
    }
}
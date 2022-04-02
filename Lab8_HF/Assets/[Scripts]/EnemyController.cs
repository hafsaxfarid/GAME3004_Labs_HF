using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
    public Transform player;

    private NavMeshAgent enemyAgent;

    void Start()
    {
        enemyAgent = GetComponent<NavMeshAgent>();
    }


    void Update()
    {
        enemyAgent.SetDestination(player.position);
    }
}

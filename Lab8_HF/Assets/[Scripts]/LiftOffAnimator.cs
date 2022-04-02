using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class LiftOffAnimator : MonoBehaviour
{
    private Animator animator;
    private EnemyController enemyController;
    private Transform player;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        enemyController = transform.parent.GetComponent<EnemyController>();
        player = enemyController.player;//.transform;
    }

    void Update()
    {
        if( Vector3.Distance(player.position, transform.position) < 2.1f )
        {
            // turn to face player
            Vector3 targetPosition = new Vector3(player.position.x, transform.position.y, player.position.z);

            // look at target (player)
            transform.LookAt(targetPosition);

            animator.SetInteger("AnimationState", 2); // punch if in range
        }
        else
        {
            animator.SetInteger("AnimationState", 1); // go back to walking when not in range
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlaneController : MonoBehaviour
{
    public Transform playerSpawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.gameObject;
            var characterController = player.GetComponent<CharacterController>();

            characterController.enabled = false;

            player.transform.position = playerSpawnPoint.position;

            characterController.enabled = true;
        }
    }
}

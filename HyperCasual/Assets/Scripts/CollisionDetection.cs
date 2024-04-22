using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    private PlayerController _playerController;
    Transform obtaclesParent;

    private void Awake()
    {
        SetCollisionDetectionParameters();
        obtaclesParent = GameObject.FindGameObjectWithTag("Obstacles").GetComponent<Transform>();
    }

    private void SetCollisionDetectionParameters()
    {
        _playerController = GetComponent<PlayerController>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.IsChildOf(obtaclesParent))
        {
            transform.position = _playerController.characterStartingPosition;
            _playerController.ChangeStateTo(_playerController.obstacleHitState);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            if (CompareTag("Player"))
            {
                GameBehaviour.Instance.Notifications.PostNotification(Game_Events.FinishRunning);
            }

            _playerController.ChangeStateTo(_playerController.runningFinishState);
        }
    }
}

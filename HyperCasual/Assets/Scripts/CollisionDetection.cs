using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    private PlayerController _playerController;

    private void Awake()
    {
        SetCollisionDetectionParameters();
    }

    private void SetCollisionDetectionParameters()
    {
        _playerController = gameObject.GetComponent<PlayerController>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.IsChildOf(GameObject.FindGameObjectWithTag("Obstacles").GetComponent<Transform>()))
        {
            transform.position = _playerController.characterStartingPosition;
            _playerController.ChangeStateTo(_playerController.obstacleHitState);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            if (gameObject.CompareTag("Player"))
            {
                GameBehaviour.Instance.Notifications.PostNotification(NotificationManager.EVENT_TYPE.FinishRunning);
            }

            _playerController.ChangeStateTo(_playerController.runningFinishState);
        }
    }
}

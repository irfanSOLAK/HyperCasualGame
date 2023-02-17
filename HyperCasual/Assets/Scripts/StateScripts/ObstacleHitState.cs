using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleHitState : MonoBehaviour, IPlayerState
{
    private PlayerController _playerController;

    public void Handle(PlayerController playerController)
    {
        if (!_playerController)
            _playerController = playerController;

        _playerController.CharacterForwardSpeed = 0;
        StartCoroutine(StartGame());
    }

    public void DeactivateState()
    {
        _playerController = null;
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(0.1f);
        _playerController.StartRunning();
    }
}

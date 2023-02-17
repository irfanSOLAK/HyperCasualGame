using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishState : MonoBehaviour, IPlayerState
{
    private PlayerController _playerController;

    public void Handle(PlayerController playerController)
    {
        if (!_playerController)
            _playerController = playerController;

        _playerController.CharacterForwardSpeed = 0;
        gameObject.SetActive(false);
    }

    public void DeactivateState()
    {
        _playerController = null;
    }
}

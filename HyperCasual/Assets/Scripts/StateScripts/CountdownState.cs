using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountdownState : MonoBehaviour, IPlayerState
{
    private PlayerController _playerController;

    public void Handle(PlayerController playerController)
    {
        if (!_playerController)
            _playerController = playerController;

        _playerController.CharacterForwardSpeed = 0;

        StartIdleAnimationAtRandomTime();
    }

    private void StartIdleAnimationAtRandomTime()
    {
        if (!CompareTag("Player"))
        {
            float animationStartTime = Random.Range(0, 1f);
            GetComponent<Animator>().SetFloat("AnimationStartTime", animationStartTime);
        }
    }

    public void DeactivateState()
    {
        _playerController = null;
    }
}

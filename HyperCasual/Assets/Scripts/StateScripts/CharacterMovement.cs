using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour, IPlayerState
{
    private PlayerController _playerController;

    private float _characterHorizontalSpeed;
    private float _characterXAxisDisplacement;
    private Vector2 _characterXAxisLimits;

    private float _characterXAxisNextPosition;

    private IPlayerInput _characterMovementController;

    private Rigidbody _characterRigidbody;

    public void Handle(PlayerController playerController)
    {
        if (!_playerController)
            _playerController = playerController;

        SetCharacterMovementParameters();
        SetMovementController();
    }

    private void SetCharacterMovementParameters()
    {
        _characterRigidbody = GetComponent<Rigidbody>();
        _playerController.CharacterForwardSpeed = GameBehaviour.Instance.GameParameters.characterForwardSpeed;
        _characterHorizontalSpeed = GameBehaviour.Instance.GameParameters.characterHorizontalSpeed;
        _characterXAxisDisplacement = GameBehaviour.Instance.GameParameters.characterXAxisDisplacement;
        _characterXAxisLimits = GameBehaviour.Instance.GameParameters.characterXAxisLimits;
    }

    void SetMovementController()
    {
        if (gameObject.CompareTag("Player"))
        {
            if (gameObject.GetComponent<RunningGameInputUI>() == null)
                gameObject.AddComponent<RunningGameInputUI>();

            _characterMovementController = gameObject.GetComponent<RunningGameInputUI>();
        }
        else
        {
            if (gameObject.GetComponent<AI>() == null)
                gameObject.AddComponent<AI>();

            _characterMovementController = gameObject.GetComponent<AI>();
        }
    }

    public void DeactivateState()
    {
        _playerController = null;
    }


    private void Update()
    {
        if (_playerController)
        {
            CalculateCharacterXAxisNextPosition(_characterMovementController.GiveInput());
        }
    }

    private void CalculateCharacterXAxisNextPosition(float newXPos)
    {
        _characterXAxisNextPosition = Mathf.Clamp(transform.position.x + newXPos * _characterXAxisDisplacement,
        _characterXAxisLimits.x,
        _characterXAxisLimits.y);
    }

    private void FixedUpdate()
    {
        if (_playerController)
        {
            Move();
        }
    }
    private void Move()
    {
        _characterRigidbody.MovePosition(
            new Vector3(Mathf.Lerp(transform.position.x, _characterXAxisNextPosition, _characterHorizontalSpeed * Time.fixedDeltaTime),
            transform.position.y,
            transform.position.z + _playerController.CharacterForwardSpeed * Time.fixedDeltaTime)
            );
    }
}
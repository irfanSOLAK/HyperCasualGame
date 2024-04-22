using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour, IPlayerState
{
    private PlayerController _playerController;
    private Rigidbody _rigidbody;

    private IPlayerInput _movementController;

    private float _horizontalSpeed;
    private float _xAxisDisplacement;
    private float _xAxisNextPosition;

    private Vector2 _xAxisLimits;


    public void Handle(PlayerController playerController)
    {
        if (!_playerController)
            _playerController = playerController;

        SetCharacterMovementParameters();
        SetInputController();
    }

    private void SetCharacterMovementParameters()
    {
        _rigidbody = GetComponent<Rigidbody>();
        var parameters = GameBehaviour.Instance.GameParameters;
        _playerController.CharacterForwardSpeed = parameters.characterForwardSpeed;
        _horizontalSpeed = parameters.characterHorizontalSpeed;
        _xAxisDisplacement = parameters.characterXAxisDisplacement;
        _xAxisLimits = parameters.characterXAxisLimits;
    }

    void SetInputController()
    {
        _movementController = CompareTag("Player") ? GetRunningGameInputUI() : GetAIInput();
    }

    IPlayerInput GetRunningGameInputUI()
    {
        if (gameObject.GetComponent<RunningGameInputUI>() == null)
            gameObject.AddComponent<RunningGameInputUI>();

        return GetComponent<RunningGameInputUI>();
    }

    IPlayerInput GetAIInput()
    {
        if (gameObject.GetComponent<AI>() == null)
            gameObject.AddComponent<AI>();

        return GetComponent<AI>();
    }

    public void DeactivateState()
    {
        _playerController = null;
    }


    private void Update()
    {
        if (_playerController)
        {
            CalculateCharacterXAxisNextPosition(_movementController.GiveInput());
        }
    }

    private void CalculateCharacterXAxisNextPosition(float input)
    {
        float newXPos = transform.position.x + input * _xAxisDisplacement;
        _xAxisNextPosition = Mathf.Clamp(newXPos, _xAxisLimits.x, _xAxisLimits.y);
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
        Vector3 newPosition = new Vector3(
            Mathf.Lerp(transform.position.x, _xAxisNextPosition, _horizontalSpeed * Time.fixedDeltaTime),
            transform.position.y,
            transform.position.z + _playerController.CharacterForwardSpeed * Time.fixedDeltaTime);

        _rigidbody.MovePosition(newPosition);

    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[SelectionBase]
[RequireComponent(typeof(CollisionDetection))]
public class PlayerController : Listener
{
    private GameStateManager _gameStateManager;
    public IPlayerState countdownState, runningStartState, obstacleHitState, runningFinishState, paintState;
    [HideInInspector] public Vector3 characterStartingPosition;
    Animator _characterAnimator;

    public IPlayerState CurrentState
    {
        get { return _gameStateManager.CurrentState; }
    }

    private bool _isPainting;
    public bool IsPainting
    {
        get
        {
            return _isPainting;
        }
        set
        {
            _isPainting = value;
            _characterAnimator.SetBool("Painting", IsPainting);
        }
    }

    private float _CharacterForwardSpeed;
    public float CharacterForwardSpeed
    {
        get
        {
            return _CharacterForwardSpeed;
        }
        set
        {
            _CharacterForwardSpeed = value;
            _characterAnimator.SetFloat("CharacterSpeed", CharacterForwardSpeed);
        }
    }

    private void Awake()
    {
        SetCharacterParameters();
        AwakeGameStateManager();
    }

    private void SetCharacterParameters()
    {
        characterStartingPosition = transform.position;
        _characterAnimator = GetComponent<Animator>();
    }

    private void AwakeGameStateManager()
    {
        _gameStateManager = new GameStateManager(this);
        obstacleHitState = gameObject.AddComponent<ObstacleHitState>();
        runningFinishState = gameObject.AddComponent<FinishState>();
    }

    public void ChangeStateTo(IPlayerState playerState)
    {
        _gameStateManager.Transition(playerState);
    }

    public override void OnDisable()
    {
        base.OnDisable();
        _gameStateManager.DeactivatePreviousState();
    }

    public override void AddEventListeners()
    {
        GameBehaviour.Instance.Notifications.AddListener(NotificationManager.EVENT_TYPE.Countdown, this);
        GameBehaviour.Instance.Notifications.AddListener(NotificationManager.EVENT_TYPE.StartRunning, this);
        GameBehaviour.Instance.Notifications.AddListener(NotificationManager.EVENT_TYPE.FinishRunning, this);
        GameBehaviour.Instance.Notifications.AddListener(NotificationManager.EVENT_TYPE.PaintWall, this);
    }

    public override void RemoveEventListeners()
    {
        GameBehaviour.Instance.Notifications.RemoveListener(NotificationManager.EVENT_TYPE.Countdown, this);
        GameBehaviour.Instance.Notifications.RemoveListener(NotificationManager.EVENT_TYPE.StartRunning, this);
        GameBehaviour.Instance.Notifications.RemoveListener(NotificationManager.EVENT_TYPE.FinishRunning, this);
        GameBehaviour.Instance.Notifications.RemoveListener(NotificationManager.EVENT_TYPE.PaintWall, this);
    }

    public void Countdown(float parameter = 0)
    {
        if (countdownState == null)
            countdownState = gameObject.AddComponent<CountdownState>();

        ChangeStateTo(countdownState);
    }

    public void StartRunning(float parameter = 0)
    {
        if (runningStartState == null)
            runningStartState = gameObject.AddComponent<CharacterMovement>();

        ChangeStateTo(runningStartState);
    }

    public void FinishRunning(float parameter = 0)
    {
        if (runningFinishState == null)
            runningFinishState = gameObject.AddComponent<FinishState>();
    }

    public void PaintWall(float parameter = 0)
    {
        if (paintState == null)
            paintState = gameObject.AddComponent<PaintState>();

        ChangeStateTo(paintState);
    }
}
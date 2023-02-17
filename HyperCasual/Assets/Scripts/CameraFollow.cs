using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : Listener
{
    [SerializeField] Transform _targetToFollow;
    Vector3 _cameraToTargetDistance;
    Vector3 _cameraStartingPosition;

    private bool _isFollowingTarget;

    private void Awake()
    {
        SetCameraParameters();
        transform.position = _cameraStartingPosition;
    }

    private void SetCameraParameters()
    {
        _cameraToTargetDistance = GameBehaviour.Instance.GameParameters.cameraToTargetDistance;
        _cameraStartingPosition = GameBehaviour.Instance.GameParameters.startingPosition;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        FollowTarget();
    }

    private void FollowTarget()
    {
        transform.position = Vector3.Lerp(transform.position, TargetPosition(), Time.deltaTime);
    }

    private Vector3 TargetPosition()
    {
        return _isFollowingTarget ? _targetToFollow.position + _cameraToTargetDistance : _cameraStartingPosition;
    }

    public override void AddEventListeners()
    {
        GameBehaviour.Instance.Notifications.AddListener(NotificationManager.EVENT_TYPE.Countdown, this);
        GameBehaviour.Instance.Notifications.AddListener(NotificationManager.EVENT_TYPE.FinishRunning, this);
    }

    public override void RemoveEventListeners()
    {
        GameBehaviour.Instance.Notifications.RemoveListener(NotificationManager.EVENT_TYPE.Countdown, this);
        GameBehaviour.Instance.Notifications.RemoveListener(NotificationManager.EVENT_TYPE.FinishRunning, this);
    }

    public void Countdown(float parameter = 0)
    {
        _isFollowingTarget = true;
    }

    public void FinishRunning(float parameter = 0)
    {
        _isFollowingTarget = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : Listener
{
    [SerializeField] Transform _targetToFollow;
    Vector3 _cameraToTargetDistance;
    Vector3 _cameraStartingPosition;

    private bool _isFollowingTarget;

    NotificationManager notification;

    private void Awake()
    {
        SetCameraParameters();
        transform.position = _cameraStartingPosition;
        notification = GameBehaviour.Instance.Notifications;
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

    public override void AddThisToEventListener()
    {
        notification.AddListener(Game_Events.Countdown, this);
        notification.AddListener(Game_Events.FinishRunning, this);
    }

    public override void RemoveThisFromEventListener()
    {
        notification.RemoveListener(Game_Events.Countdown, this);
        notification.RemoveListener(Game_Events.FinishRunning, this);
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

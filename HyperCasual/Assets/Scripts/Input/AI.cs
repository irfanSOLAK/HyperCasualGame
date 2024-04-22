using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour, IPlayerInput
{
    [SerializeField] int numberOfRays = 7;
    [SerializeField] float rayLength = 1;
    [SerializeField] Vector3 rayOffset;

    const int MoveLeftValue = -1;
    const int MoveRightValue = 1;
    const int NoMovement = 0;

    const float UpperMovementLimit = 0.9f;
    const float LowerMovementLimit = 0.5f;
    float movementLimit = 1f;

    private AIInputHelper inputHelper;
    private Vector2 characterXAxisLimits;
    private void OnEnable()
    {
        inputHelper = new AIInputHelper(transform, numberOfRays, rayLength, rayOffset);
        characterXAxisLimits = GameBehaviour.Instance.GameParameters.characterXAxisLimits;
    }

    public float GiveInput()
    {
        inputHelper.DetectObjects(out float moveLeft, out float moveRight);
        return DecideMovement(moveLeft, moveRight);
    }

    private float DecideMovement(float moveLeft, float moveRight)
    {
        return (moveRight != moveLeft) ? ChooseDirectionBasedOnObstacles(moveLeft, moveRight) : (moveRight != 0) ? MoveTowardsCenter() : NoMovement;
    }

    private float ChooseDirectionBasedOnObstacles(float moveLeft, float moveRight)
    {
        return moveRight > moveLeft ? SelectDirectionBasedOnUpperLimit() : SelectDirectionBasedOnLowerLimit();
    }

    private float SelectDirectionBasedOnUpperLimit()
    {
        movementLimit = (transform.position.x >= characterXAxisLimits.y * movementLimit) ? LowerMovementLimit : UpperMovementLimit;
        return (movementLimit == LowerMovementLimit) ? MoveLeftValue : MoveRightValue;
    }
    private float SelectDirectionBasedOnLowerLimit()
    {
        movementLimit = (transform.position.x <= characterXAxisLimits.x * movementLimit) ? LowerMovementLimit : UpperMovementLimit;
        return (movementLimit == LowerMovementLimit) ? MoveRightValue : MoveLeftValue;
    }
    private float MoveTowardsCenter()
    {
        return transform.position.x >= 0 ? MoveLeftValue : MoveRightValue;
    }


    private void OnDrawGizmosSelected()
    {
        for (int i = 0; i < numberOfRays; i++)
        {
            Quaternion characterRotation = transform.rotation;
            Quaternion rayRotation = Quaternion.AngleAxis(90 - i * 180 / (numberOfRays - 1), transform.up);
            Vector3 direction = characterRotation * rayRotation * Vector3.forward;
            Gizmos.DrawRay(transform.position + rayOffset, rayLength * direction);
        }
    }
}

class AIInputHelper
{
    private Transform transform;
    private int numberOfRays;
    private float rayLength;
    private Vector3 rayOffset;

    private Transform obstacleParentTransform;
    private Transform playersParentTransform;

    Vector3 rayStartingPosition;

    const float obstacleMovementWeight = 1f;
    const float playerMovementWeight = 0.5f;

    public AIInputHelper(Transform transform, int numberOfRays, float rayLength, Vector3 rayOffset)
    {
        this.transform = transform;
        this.numberOfRays = numberOfRays;
        this.rayLength = rayLength;
        this.rayOffset = rayOffset;

        obstacleParentTransform = GameObject.FindGameObjectWithTag("Obstacles").GetComponent<Transform>();
        playersParentTransform = GameObject.FindGameObjectWithTag("PlayersParent").GetComponent<Transform>();

    }

    public void DetectObjects(out float moveLeft, out float moveRight)
    {
        rayStartingPosition = transform.position + rayOffset;
        moveLeft = 0;
        moveRight = 0;

        for (int i = 0; i < numberOfRays; i++)
        {
            float rayAngle = 90 - i * 180 / (numberOfRays - 1);
            var direction = Quaternion.AngleAxis(rayAngle, transform.up) * transform.forward;

            if (Physics.Raycast(rayStartingPosition, direction, out RaycastHit hit, rayLength))
            {
                Debug.DrawLine(rayStartingPosition, hit.point, Color.green);
                var hitTransform = hit.collider.transform;

                if (hitTransform.IsChildOf(obstacleParentTransform))
                {
                    _ = rayAngle <= 0 ? moveRight += obstacleMovementWeight : moveLeft += obstacleMovementWeight;
                }
                else if (hitTransform.IsChildOf(playersParentTransform))
                {
                    _ = rayAngle <= 0 ? moveRight += playerMovementWeight : moveLeft += playerMovementWeight;
                }
            }
        }
    }
}
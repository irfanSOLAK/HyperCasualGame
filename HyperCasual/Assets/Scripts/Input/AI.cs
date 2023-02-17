using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour, IPlayerInput
{
    public int numberOfRays = 7;
    public float rayLength = 1;

    public Vector3 rayOffset;
    Vector3 rayStartingPosition;

    const int _leftMovement = -1;
    const int _rightMovement = 1;
    const int _noMovement = 0;

    float _movementLimitsScaleUpper = 9 / 10f;
    float _movementLimitsScaleLower = 5 / 10f;
    float _movementLimitsScale = 1f;

    public float GiveInput()
    {
        DetectObjects(out float moveLeft, out float moveRight);
        return DecideMovement(moveLeft, moveRight);
    }

    private void DetectObjects(out float moveLeft, out float moveRight)
    {
        float rayAngle = 90;
        rayStartingPosition = transform.position + rayOffset;
        moveLeft = 0;
        moveRight = 0;
        RaycastHit hit;

        for (int i = 0; i < numberOfRays; i++)
        {
            rayAngle = 90 - i * 180 / (numberOfRays - 1);

            if (Physics.Raycast(rayStartingPosition, Quaternion.AngleAxis(rayAngle, transform.up) * transform.forward, out hit, rayLength))
            {
                Debug.DrawLine(rayStartingPosition, hit.point, Color.green);

                if (hit.collider.transform.IsChildOf(GameObject.FindGameObjectWithTag("Obstacles").GetComponent<Transform>()))
                {
                    if (rayAngle <= 0)
                    {
                        moveRight++;
                    }

                    if (rayAngle >= 0)
                    {
                        moveLeft++;
                    }
                }

                if (hit.collider.transform.IsChildOf(GameObject.FindGameObjectWithTag("PlayersParent").GetComponent<Transform>()))
                {
                    if (rayAngle <= 0)
                    {
                        moveRight += 0.5f;
                    }

                    if (rayAngle >= 0)
                    {
                        moveLeft += 0.5f;
                    }
                }
            }
        }
    }

    private float DecideMovement(float moveLeft, float moveRight)
    {
        if (moveRight != moveLeft)
        {
            if (moveRight > moveLeft)
            {
                if (gameObject.transform.position.x >= GameBehaviour.Instance.GameParameters.characterXAxisLimits.y * _movementLimitsScale)
                {
                    _movementLimitsScale = _movementLimitsScaleLower;
                    return _leftMovement;

                }
                else
                {
                    _movementLimitsScale = _movementLimitsScaleUpper;
                    return _rightMovement;
                }
            }
            else
            {
                if (gameObject.transform.position.x <= GameBehaviour.Instance.GameParameters.characterXAxisLimits.x * _movementLimitsScale)
                {
                    _movementLimitsScale = _movementLimitsScaleLower;
                    return _rightMovement;
                }
                else
                    _movementLimitsScale = _movementLimitsScaleUpper;
                {
                    return _leftMovement;
                }

            }
        }
        else if (moveRight != 0)
        {
            if (transform.position.x >= 0)
            {
                return _leftMovement;
            }
            else
            {
                return _rightMovement;
            }
        }
        else
        {
            return _noMovement;
        }
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
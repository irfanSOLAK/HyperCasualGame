using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningGameInputUI : MonoBehaviour, IPlayerInput
{
    private float _touchXAxisInitialPosition;
    private float _touchXAxisCurrentPosition;
    private float _touchSensitivity = 1 / 4f;

    private float _characterXAxisMovement;
    public float GiveInput()
    {
        return _characterXAxisMovement;
    }

    // Update is called once per frame
    void Update()
    {
        ControlWithTouchInput();
        ControlWithKeyboardInput();
    }

    private void ControlWithTouchInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ProcessFirstTouch();
        }
        else if (Input.GetMouseButton(0))
        {
            ProcessTouching();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            ProcessLastTouch();
        }
    }

    private void ProcessFirstTouch()
    {
        _touchXAxisInitialPosition = Input.mousePosition.x;
    }

    private void ProcessTouching()
    {
        _touchXAxisCurrentPosition = Input.mousePosition.x - _touchXAxisInitialPosition;
        _touchXAxisInitialPosition = Input.mousePosition.x;
        _characterXAxisMovement = _touchXAxisCurrentPosition * _touchSensitivity;
    }
    private void ProcessLastTouch()
    {
        _touchXAxisCurrentPosition = 0;
        _characterXAxisMovement = _touchXAxisCurrentPosition * _touchSensitivity;
    }

    private void ControlWithKeyboardInput()
    {
        if (Input.GetButton("Horizontal"))
        {
            _characterXAxisMovement = Input.GetAxisRaw("Horizontal");
        }
        else if (Input.GetButtonUp("Horizontal"))
        {
            _characterXAxisMovement = 0;
        }
    }
}
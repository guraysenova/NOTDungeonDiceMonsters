using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    float movementSpeed, zoomSpeed, rotationSpeed , shiftSpeedMultiplier;

    [SerializeField]
    float restrictionLeft, restrictionRight, restrictionForward, restrictionBack, restrictionTop, restrictionBottom;

    [SerializeField]
    float cameraMinAngle, cameraMaxAngle;

    Vector3 movementVector , midMouseMovementVector;

    Vector3 rotationVector;

    float shiftVal = 0f;

    [SerializeField]
    GameObject cameraRotationObj;

    void Update()
    {
        movementVector = Vector3.zero;

        RotateCamera();
        MoveForward();
        MoveBackward();
        MoveLeft();
        MoveRight();
        Zoom();
        Shift();
        MiddleMouse();
        Move();
    }

    private void MiddleMouse()
    {
        if (Input.GetMouseButtonDown(2))
        {
            midMouseMovementVector = Vector3.zero;

            midStartPos = new Vector3();
            midEndPos = new Vector3();

            midStartPos = Input.mousePosition;
            midEndPos = Input.mousePosition;
        }
        if (Input.GetMouseButton(2))
        {
            midEndPos = Input.mousePosition;

            midMouseMovementVector = midEndPos - midStartPos;

            midStartPos = Input.mousePosition;
            midEndPos = Input.mousePosition;

            if (midMouseMovementVector.x >= 0.05f)
            {
                movementVector += cameraRotationObj.transform.right;
            }
            if (midMouseMovementVector.x <= -0.05f)
            {
                movementVector -= cameraRotationObj.transform.right;
            }
            if (midMouseMovementVector.y >= 0.05f)
            {
                movementVector += cameraRotationObj.transform.forward;
            }
            if (midMouseMovementVector.y <= -0.05f)
            {
                movementVector -= cameraRotationObj.transform.forward;
            }
        }
        if (Input.GetMouseButtonUp(2))
        {
            midStartPos = new Vector3();
            midEndPos = new Vector3();
        }
    }

    void Shift()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            shiftVal = 1f;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            shiftVal = 0f;
        }
    }

    void RotateCamera()
    {
        if (Input.GetMouseButtonDown(1))
        {
            //get mouse movement;
            PressDown();
        }
        if (Input.GetMouseButton(1))
        {
            HoldPressDown();
            Rotate();
        }
        if (Input.GetMouseButtonUp(1))
        {
            PressUp();
        }
    }

    Vector3 startPos = new Vector3();
    Vector3 endPos = new Vector3();

    Vector3 midStartPos = new Vector3();
    Vector3 midEndPos = new Vector3();

    void PressDown()
    {
        startPos = new Vector3();
        endPos = new Vector3();
        SetStartPos();
    }
    void HoldPressDown()
    {
        SetEndPosition();
        rotationVector = endPos - startPos;
        SetStartPos();
    }

    void PressUp()
    {
        startPos = new Vector3();
        endPos = new Vector3();
        //Input.mousePosition.Set(0, 0, 0);
    }

    void SetStartPos()
    {
        startPos = Input.mousePosition;
        endPos = Input.mousePosition;
    }

    void SetEndPosition()
    {
        endPos = Input.mousePosition;
    }

    void MoveForward()
    {
        if (Input.GetKey(KeyCode.W))
        {
            movementVector += cameraRotationObj.transform.forward;
        }
    }

    void MoveBackward()
    {
        if (Input.GetKey(KeyCode.S))
        {
            movementVector -= cameraRotationObj.transform.forward;
        }
    }

    void MoveLeft()
    {
        if (Input.GetKey(KeyCode.A))
        {
            movementVector -= cameraRotationObj.transform.right;
        }
    }

    void MoveRight()
    {
        if (Input.GetKey(KeyCode.D))
        {
            movementVector += cameraRotationObj.transform.right;
        }
    }

    void Zoom()
    {
        movementVector -= Vector3.up * Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
    }

    void Move()
    {
        movementVector *= (movementSpeed * Time.deltaTime);

        if(shiftVal >= 0.1f)
        {
            if (movementVector.magnitude >= 0.1f)
            {
                transform.Translate(movementVector * (shiftSpeedMultiplier * shiftVal), Space.World);
                transform.position = new Vector3(Mathf.Clamp(transform.position.x, restrictionLeft, restrictionRight),
                                                 Mathf.Clamp(transform.position.y, restrictionBottom, restrictionTop),
                                                 Mathf.Clamp(transform.position.z, restrictionBack, restrictionForward));
            }
        }
        else
        {
            if (movementVector.magnitude >= 0.1f)
            {
                transform.Translate(movementVector , Space.World);
                transform.position = new Vector3(Mathf.Clamp(transform.position.x, restrictionLeft, restrictionRight),
                                                 Mathf.Clamp(transform.position.y, restrictionBottom, restrictionTop),
                                                 Mathf.Clamp(transform.position.z, restrictionBack, restrictionForward));
            }
        }
    }

    void Rotate()
    {
        float valX = (-rotationVector.y * rotationSpeed * Time.deltaTime) + transform.localEulerAngles.x;
        valX = MyMathf.ClampAngle(valX, cameraMinAngle, cameraMaxAngle);
        float valY = (rotationVector.x * rotationSpeed * Time.deltaTime) + transform.localEulerAngles.y;
        transform.localRotation = Quaternion.Euler(valX, valY, 0);
        cameraRotationObj.transform.rotation = Quaternion.Euler(0, valY, 0);
    }
}

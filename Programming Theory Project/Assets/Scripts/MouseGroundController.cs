using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MouseGroundController : GroundController // INHERITANCE
{
    [SerializeField] float mouseSensitivity = 200f;
    [SerializeField] float keySensitivity = 10f;
    [SerializeField] Vector2 xAndYVal;
    [SerializeField] Vector2 rotation;
    [SerializeField] Vector2 adjusted;
    [SerializeField] bool gyroSupported;
    [SerializeField] Vector3 gyroStart;
    [SerializeField] Toggle GyroToggle;

    private Gyroscope gyro;
    private bool gyroToggle;

    private void Start()
    {
        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;
            //gyroStart = gyro.attitude.eulerAngles;
        }

        gyroSupported = SystemInfo.supportsGyroscope && gyroToggle;
    }

    protected override bool GyroSupported()
    {
        return gyroSupported;
    }

    protected override float GetGyroYStart()
    {
        return gyroStart.y;
    }

    // Mouse Input Y
    public float getYInput1()
    {
        float mouseVertical = Input.GetAxis("Mouse Y") * mouseSensitivity;

        return mouseVertical;
    }

    // Mouse Input X
    public float getXInput1()
    {
        float mouseHorizontal = -Input.GetAxis("Mouse X") * mouseSensitivity;

        return mouseHorizontal;
    }


    // Arrow Key Input Y
    public float getYInput2()
    {
        float y = 0;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            y = Vector3.up.y * keySensitivity;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            y = Vector3.down.y * keySensitivity;
        }
        xAndYVal.y = y;

        return y;
    }

    // Arrow Key Input X
    public float getXInput2()
    {
        float x = 0;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            x = -Vector3.left.x * keySensitivity;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            x = -Vector3.right.x * keySensitivity;
        }
        xAndYVal.x = x;

        return x;
    }

    //Acceleration Y Input
    public float getYInput3()
    {
        float mouseVertical = Input.acceleration.y;// * tiltSensitivity;

        return mouseVertical;
    }

    //Acceleration X Input
    public float getXInput3()
    {
        float mouseHorizontal = -Input.acceleration.x;// * tiltSensitivity;

        return mouseHorizontal;
    }

    protected override float getYInput() // POLYMORPHISM
    {
        float y = 0;

        y = getYInput1();  // allows both mouse and arrow key control

        if (y == 0)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                y = Vector3.up.y * keySensitivity;
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                y = Vector3.down.y * keySensitivity;
            }
        }
        xAndYVal.y = y;

        return y;
    }

    protected override float getXInput() // POLYMORPHISM
    {
        float x = 0;

        x = getXInput1();  // allows both mouse and arrow key control

        if (x == 0)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                x = -Vector3.left.x * keySensitivity;
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                x = -Vector3.right.x * keySensitivity;
            }
        }
        xAndYVal.x = x;

        return x;
    }

    protected override float getQXInput()
    {
        rotation.x = (float)Math.Round(gyro.attitude.eulerAngles.x, 2);
        if(rotation.x > 180)
        {
            rotation.x = rotation.x - 360;
        }
        adjusted.x = rotation.x;
        return rotation.x;
    }

    protected override float getQYInput()
    {
        rotation.y = (float)Math.Round(gyro.attitude.eulerAngles.y, 2);
        if (rotation.y > 180)
        {
            rotation.y = rotation.y - 360;
        }
        adjusted.y = (float)Math.Round(rotation.y - gyroStart.y, 2);
        return rotation.y;
    }

    public void DoGyroToggle()
    {
        if (GyroToggle.isOn)
        {
            gyroToggle = true;
            gyroStart = gyro.attitude.eulerAngles;
        } else
        {
            gyroToggle = false;
        }

        gyroSupported = SystemInfo.supportsGyroscope && gyroToggle;
    }
}

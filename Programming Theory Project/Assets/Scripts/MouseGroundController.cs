using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseGroundController : GroundController // INHERITANCE
{
    [SerializeField] float mouseSensitivity = 200f;
    [SerializeField] float keySensitivity = 10f;
 
    public float getYInput1()
    {
        float mouseVertical = Input.GetAxis("Mouse Y") * mouseSensitivity;

        return mouseVertical;
    }

    public float getXInput1()
    {
        float mouseHorizontal = -Input.GetAxis("Mouse X") * mouseSensitivity;

        return mouseHorizontal;
    }


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

        return y;
    }

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

        return x;
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

        return x;
    }
}

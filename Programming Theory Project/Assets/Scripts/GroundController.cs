using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class GroundController : MonoBehaviour // INHERITANCE
{
    private float clampAngle = 5f;
    private float verticalRotation;
    private float horizontalRotation;
    private bool firstTime = true;

    [SerializeField] Vector2 XAndY;

    //Start() not called because in child class

    // Update is called once per frame
    void Update()
    {
        if(firstTime)
        {
            Debug.Log("First time: " + (transform.localEulerAngles.x - 0.5f));
            verticalRotation = transform.localEulerAngles.x - 0.5f;
            horizontalRotation = transform.eulerAngles.y;
            transform.localRotation = Quaternion.Euler(-verticalRotation, 0f, horizontalRotation);
            firstTime = false;
        }
        
        XAndY = GetKeyInput();
        
        if(XAndY.y == 0f && XAndY.x == 0f && GyroSupported()) {
            GetGyroInput();
        }
    }

    Vector2 GetKeyInput()
    {
        float y;
        float x;

        y = getYInput();
        x = getXInput();

        verticalRotation += y * Time.deltaTime;
        horizontalRotation += x * Time.deltaTime;

        verticalRotation = Mathf.Clamp(verticalRotation, -clampAngle, clampAngle);
        horizontalRotation = Mathf.Clamp(horizontalRotation, -clampAngle, clampAngle);

        transform.localRotation = Quaternion.Euler(verticalRotation, 0f, horizontalRotation);

        return new Vector2(y, x);
    }

    Vector2 GetGyroInput()
    {
        float y;
        float x;
        float flat;

        y = getQYInput() - GetGyroYStart();
        x = getQXInput();
        flat = 360 - GetGyroYStart();

        verticalRotation = y;
        horizontalRotation = x;

        if(y > clampAngle)
        {
            verticalRotation = clampAngle;
        }
        if (y < -clampAngle)
        {
            verticalRotation = -clampAngle;
        }
        if (x > clampAngle)
        {
            horizontalRotation = clampAngle;
        }
        if (x < -clampAngle)
        {
            horizontalRotation = -clampAngle;
        }
        transform.localRotation = Quaternion.Euler(-verticalRotation, 0f, horizontalRotation);

        return new Vector2(-y, x);
    }

    protected abstract float getYInput();  // POLYMORPHISM
    protected abstract float getXInput();  // POLYMORPHISM
    protected abstract float getQXInput();  // POLYMORPHISM
    protected abstract float getQYInput();  // POLYMORPHISM
    protected abstract bool GyroSupported();  // POLYMORPHISM
    protected abstract float GetGyroYStart();  // POLYMORPHISM
}

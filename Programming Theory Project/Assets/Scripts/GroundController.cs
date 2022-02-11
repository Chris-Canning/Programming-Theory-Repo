using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GroundController : MonoBehaviour // INHERITANCE
{
    private float clampAngle = 5f;

    private float verticalRotation;
    private float horizontalRotation;

    // Start is called before the first frame update
    void Start()
    {
        verticalRotation = transform.localEulerAngles.x - 0.5f;
        horizontalRotation = transform.eulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        verticalRotation += getYInput() * Time.deltaTime;
        horizontalRotation += getXInput() * Time.deltaTime;

        verticalRotation = Mathf.Clamp(verticalRotation, -clampAngle, clampAngle);
        horizontalRotation = Mathf.Clamp(horizontalRotation, -clampAngle, clampAngle);

        transform.localRotation = Quaternion.Euler(verticalRotation, 0f, horizontalRotation);
    }

    protected abstract float getYInput();  // POLYMORPHISM
    protected abstract float getXInput();  // POLYMORPHISM

}

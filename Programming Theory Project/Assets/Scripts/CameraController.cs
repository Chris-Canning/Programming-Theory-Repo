using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] BallController player;
    [SerializeField] float sensitivity = 300f;
    [SerializeField] float clampAngle = 5f;

    private float verticalRotation;
    private float horizontalRotation;
    private float mouseScroll;

    private void Start()
    {
        verticalRotation = transform.localEulerAngles.x - 0;
        horizontalRotation = transform.eulerAngles.y;
    }

    private void Update()
    {
        Look();
        Debug.DrawRay(transform.position, transform.forward * 2, Color.red);
    }

    void Look()
    {
        float mouseVertical = -Input.GetAxis("Mouse Y");
        float mouseHorizontal = Input.GetAxis("Mouse X");

        verticalRotation += mouseVertical * sensitivity * Time.deltaTime;
        horizontalRotation += mouseHorizontal * sensitivity * Time.deltaTime;

        verticalRotation = Mathf.Clamp(verticalRotation, -clampAngle, clampAngle);

        transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        player.transform.rotation = Quaternion.Euler(0f, horizontalRotation, 0f);

        mouseScroll = Input.GetAxis("Mouse ScrollWheel");
        if(mouseScroll != 0f)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + mouseScroll);
        }
    }

}

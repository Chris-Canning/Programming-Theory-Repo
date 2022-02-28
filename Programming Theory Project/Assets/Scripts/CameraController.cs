using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] BallController player;
    [SerializeField] float sensitivity = 300f;
    [SerializeField] float clampAngle = 20f;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float velocity = 20f;
    [SerializeField] float keySensitivity = 200f;

    private float verticalRotation;
    private float horizontalRotation;
    private float mouseScroll;
    GameObject bullet;
    GameObject enemy;

    private Touch theTouch;
    private Vector2 touchStartPos;
    private Vector2 touchEndPos;
    public string direction;
    [SerializeField] int touchCount;
    [SerializeField] TouchPhase touchPhase;
    [SerializeField] Vector3 enemyAngle;

    private void Start()
    {
        verticalRotation = transform.localEulerAngles.x - 0;
        horizontalRotation = transform.eulerAngles.y;
        StartCoroutine(FireBullet());
        enemy = GameObject.FindGameObjectWithTag("Enemy");
    }

    private void Update()
    {
        if(enemy == false)
        {
            enemy = GameObject.FindGameObjectWithTag("Enemy");
        }
        Look1();
        Debug.DrawRay(transform.position, transform.forward * 2, Color.red);
    }

    public IEnumerator FireBullet()
    {
        //Debug.Log("Fired bullet");
        while (true)
        {
            bullet = Instantiate(bulletPrefab, transform.position + new Vector3(0f, 0f, 0f), Quaternion.identity);
            bullet.GetComponent<Rigidbody>().velocity = velocity * transform.forward;

            yield return new WaitForSeconds(1f);
        }

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

    void Look1()
    {
        float y = 0;
        float x = 0;
        string dir;

        dir = GetTouch();

        if (Input.GetKey(KeyCode.UpArrow) || dir == "Up")
        {
            y = Vector3.up.y;
        }

        if (Input.GetKey(KeyCode.DownArrow) || dir == "Down")
        {
            y = Vector3.down.y;
        }

        if (Input.GetKey(KeyCode.LeftArrow) || dir == "Left")
        {
            x = Vector3.left.x;
        }

        if (Input.GetKey(KeyCode.RightArrow) || dir == "Right")
        {
            x = Vector3.right.x;
        }
        if (dir == "Tapped" || dir == "None")
        {
            x = 0;
            y = 0;
        }

        verticalRotation += y * Time.deltaTime * keySensitivity;
        horizontalRotation += x * Time.deltaTime * keySensitivity;

        verticalRotation = Mathf.Clamp(verticalRotation, -clampAngle, clampAngle);

        Vector3 posA;
        Vector3 posB;
        Vector3 direct;

        if (enemy)
        {
            posA = transform.position;
            posB = enemy.transform.position;
            direct = posB - posA;
            enemyAngle = direct;
            player.transform.rotation = Quaternion.LookRotation(direct);
        } else {
            transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
            player.transform.rotation = Quaternion.Euler(0f, horizontalRotation, 0f);
        }
    }

    string GetTouch()
    {
        direction = "None";

        touchCount = Input.touchCount;

        if (touchCount > 0)
        {
            theTouch = Input.GetTouch(0);
            touchPhase = theTouch.phase;

            if (theTouch.phase == TouchPhase.Began || theTouch.phase == TouchPhase.Stationary)
            {
                touchStartPos = theTouch.position;
            }

            if (theTouch.phase == TouchPhase.Moved || theTouch.phase == TouchPhase.Ended)
            {
                touchEndPos = theTouch.position;

                float x = touchEndPos.x - touchStartPos.x;
                float y = touchEndPos.y - touchStartPos.y;

                if (Mathf.Abs(x) == 0 && Mathf.Abs(y) == 0)
                {
                    direction = "Tapped";
                }
                else if (Mathf.Abs(x) > Mathf.Abs(y))
                {
                    direction = x > 0 ? "Right" : "Left";
                }
                else
                {
                    direction = y > 0 ? "Up" : "Down";
                }
            } else if(theTouch.phase == TouchPhase.Stationary)
            {
                direction = "Stationary";
            }
        }
        return direction;

    }
}

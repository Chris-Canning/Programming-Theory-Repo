using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }
        if (transform.position.x < -20 || transform.position.x > 20 || transform.position.z < -20 || transform.position.z > 20)
        {
            //Destroy(gameObject);
        }
    }
}

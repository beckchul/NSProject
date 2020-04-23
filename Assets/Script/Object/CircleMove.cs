using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMove : MonoBehaviour
{

    // Use this for initialization
    int a = 5;
    void Start()
    {


    }
    void Update()
    {
        if (transform.position.x < -10.0f)
        {
            a = 5;
        }
        else if(transform.position.x>10.0f)
        {
            a = -5;
        }
        transform.Translate(Vector3.forward * 1.0f * Time.deltaTime * a);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testcamara : MonoBehaviour
{
    [SerializeField] Camera cam;
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (cam.WorldToViewportPoint(transform.position).x > 0 && cam.WorldToViewportPoint(transform.position).x < 1 &&
            cam.WorldToViewportPoint(transform.position).y > 0 && cam.WorldToViewportPoint(transform.position).y < 1 &&
            cam.WorldToViewportPoint(transform.position).z > 0)
        {
            print("estoy en camara");
        }
        else
        {
            print("no estoy en camara :(");
        }
    }
}

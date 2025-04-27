using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI y;

    Gyroscope _gyro;

    void Start()
    {
        _gyro = Input.gyro;
    }

    
    void Update()
    {
        //y.text = (_gyro.gravity.y.ToString());
        y.text = (Input.acceleration.y.ToString());

        if (Input.GetTouch(0).phase == 0)
        {
            SceneManager.LoadScene("Saltarin");
        }
    }
}

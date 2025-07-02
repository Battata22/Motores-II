using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaltarinManager : MonoBehaviour
{
    [SerializeField] int lockFrames;

    public event Action TriggerStep;

    public PlataformaScript LastStep
    {
        get
        {
            return LastStep;
        }
        set
        {
            TriggerStep();
            LastStep = value;
        }
        
    }

    //Gyroscope _gyro;

    #region Instance
    public static SaltarinManager instance;

    private void Awake()
    {
        instance = this;
    } 
    #endregion

    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        Screen.autorotateToPortrait = false;
        Screen.autorotateToLandscapeLeft = false;
        Screen.autorotateToLandscapeRight = false;
        Screen.autorotateToPortraitUpsideDown = false;

        //_gyro = Input.gyro;

        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = lockFrames;
    }
    
    void Update()
    {
        //if (Input.GetMouseButtonDown(0) /*Input.GetTouch(0).phase == 0*/)
        //{
        //    SceneManager.LoadScene("Saltarin");
        //}

        if (Input.GetKeyDown(KeyCode.F1))
        {
            SceneManager.LoadScene("Saltarin");
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
        }

       

    }
}

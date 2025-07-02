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

    private PlataformaScript _lastStep;

    public PlataformaScript LastStep
    {
        get
        {
            return _lastStep;
        }
        set
        {
            TriggerStep();
            _lastStep = value;
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
        TriggerStep += TestEvento;

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
        if (Input.GetKeyDown(KeyCode.F1))
        {
            SceneManager.LoadScene("Saltarin");
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
        }
    }

    void TestEvento()
    {
        print("TestEvento");
    }
}

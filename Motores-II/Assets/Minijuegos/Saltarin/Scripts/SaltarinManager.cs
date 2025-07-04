using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaltarinManager : Rewind
{
    [SerializeField] int lockFrames;

    public PlayerBehaivour Player;

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

    [SerializeField] Image _relojFoto;
    [SerializeField] Animator _relojAnim;

    //Gyroscope _gyro;

    #region Instance
    public static SaltarinManager instance;

    protected override void Awake()
    {
        base.Awake();

        instance = this;
    }
    #endregion

    #region Memento
    public override void Save()
    {
        mementoState.Rec(_lastStep);
    }

    public override void Load()
    {
        if (!mementoState.IsRemember()) return;

        var remember = mementoState.Remember();

        _lastStep = (PlataformaScript)remember.parameters[0];
    }

    public override void RemoveMe()
    {
        //MementoManager.instance.QuitMeRewind(this);
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

        if (MementoManager.instance.finishLoad)
        {
            _relojFoto.enabled = false;
            _relojAnim.SetBool("Active", false);            
        }
        else
        {
            _relojFoto.enabled = true;
            _relojAnim.SetBool("Active", true);
        }

    }

    public void ActivarTrigger()
    {
        TriggerStep();
    }

    void TestEvento()
    {
        print("TestEvento");
    }
}

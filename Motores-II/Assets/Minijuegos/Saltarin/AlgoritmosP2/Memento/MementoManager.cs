using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MementoManager : MonoBehaviour
{
    [SerializeField] List<Rewind> _rewinds = new();
    //Rewind[] _rewinds;
    Coroutine _CoroutineSaved;
    public bool finishLoad = true;

    public static MementoManager instance;
    void Awake()
    {
        instance = this;

        finishLoad = true;

        //_rewinds = FindObjectsOfType<Rewind>();    
    }

    public void AddMeRewind(Rewind rew)
    {
        _rewinds.Add(rew);
    }

    public void QuitMeRewind(Rewind rew)
    {
        _rewinds.Remove(rew);
    }

    private void Start()
    {
        //-----------------Original-----------------------
        _CoroutineSaved = StartCoroutine(CoroutineSave());
    }

    void Update()
    {


        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (_CoroutineSaved != null)
                StopCoroutine(_CoroutineSaved);

            _CoroutineSaved = StartCoroutine(CoroutineLoad());
        }
    }


    IEnumerator CoroutineSave()
    {
        var WaitForSeconds = new WaitForSeconds(0.01f);
        while (true) 
        {
            foreach (var item in _rewinds)
                item.Save();

            yield return WaitForSeconds;
        }
    }

    IEnumerator CoroutineLoad()
    {
        var WaitForSeconds = new WaitForSeconds(0.01f);

        finishLoad = false;
        while (finishLoad == false)
        {
            finishLoad = true;
            foreach (var item in _rewinds)
            {
                if (item.mementoState.IsRemember())
                    finishLoad = false;                
                item.Load();
            }

            yield return WaitForSeconds;
        }

        _CoroutineSaved = StartCoroutine(CoroutineSave());
    }
}

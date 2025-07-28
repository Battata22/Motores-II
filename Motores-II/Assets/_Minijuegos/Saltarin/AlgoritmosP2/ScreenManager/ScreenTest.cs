using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenTest : MonoBehaviour, IScreen
{
    [SerializeField] ScreenTest _nextScreen;

    [SerializeField] Canvas _canvas;

    private void Awake()
    {
        _canvas = GetComponent<Canvas>();
    }
    public void BTN_Load()
    {
        ScreenManager.instance.ActiveScreen(_nextScreen);
    }

    public void Activate()
    {
        _canvas.enabled = true;
    }

    public void Deactivate()
    {
        _canvas.enabled = false;
    }
}
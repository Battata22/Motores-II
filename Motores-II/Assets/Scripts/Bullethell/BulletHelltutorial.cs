using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHelltutorial : MonoBehaviour
{
    bool _needTutorial = true;
    [SerializeField] Canvas _canvas;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("BulletHell_NeedTutorial"))
            _needTutorial = PlayerPrefs.GetInt("BulletHell_NeedTutorial") == 1 ? true : false;
        if (_needTutorial)
        {
            Time.timeScale = 0f;
            _canvas.enabled = true;
        }
    }

    private void Update()
    {
        if (!_needTutorial)
        {
            this.enabled = false;
            return;
        }

        if (Input.touchCount != 0) 
            TurnOff();

    }

    void TurnOff()
    {
        Time.timeScale = 1;
        _canvas.enabled = false;
        this.enabled = false;
        _needTutorial = false;
        PlayerPrefs.SetInt("BulletHell_NeedTutorial", 0);
    }


}

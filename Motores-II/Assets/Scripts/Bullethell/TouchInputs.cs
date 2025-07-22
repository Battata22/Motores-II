using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInputs : MonoBehaviour
{
    [SerializeField] BasePlayer _player;
    [SerializeField] bool _stickMode = false;

    [SerializeField] Joystick _stick;


    private void Start()
    {
        //conseguir config de usario aca
        _stickMode = PlayerPrefs.GetInt("StickMode") == 1 ? true : false;
        UseStick(_stickMode);

        EventManager.Subscribe("ChangeToStick", UseStick);
    }

    private void OnDestroy()
    {
        EventManager.Unsubscribe("ChangeToStick", UseStick);
        
    }

    private void Update()
    {
        if (Input.touchCount < 1) return;

        if (!_stickMode)
        {
            MoveToTouch(Input.GetTouch(0));

        }
        else
        {
            //if (!_stick.dragged)
            //    _stick.ChangeInitialPos(Input.GetTouch(0).position);

            StickMovement();
        }
    }

    public void UseStick(params object[] noUse)
    {
        var use = PlayerPrefs.GetInt("StickMode") == 1 ? true : false;
        _stickMode = use;

        if (_stickMode)
        {
            _stick.gameObject.SetActive(true);
        }
        else
        {
            _stick.gameObject.SetActive(false);
        }

    }

    void MoveToTouch(Touch touch)
    {
        var point = Camera.main.ScreenToWorldPoint(touch.position);
        point.z = 0;
        //Debug.Log(point);


        _player.SetMovementPoint(point);
    }


    void StickMovement()
    {
        var dir = _stick.Direction;
        _player.SetMovementDirection(dir);
    }

}

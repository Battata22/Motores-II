using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roca : MonoBehaviour
{
    [SerializeField] int _currentStep = 0;
    [SerializeField] int _maxStep;

    [SerializeField] float _stepDistance;

    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] Sprite[] _posibleSprites;

    public Action<Roca> TurnOff = delegate { };
    public EscaladaManager manager;

    public bool canInteract = false;
    public KeyCode interactKey = KeyCode.None;

    public SwipeDirection inputSwipe;

    public static event Action<float> Oninteract = delegate { };

    private void Update()
    {
        //if (!canInteract && _currentStep == 3)
        //    canInteract = true;

        if(canInteract && Input.GetKeyDown(interactKey))
        {
            Debug.Log($"<color=green>No me toques mi chilito</color>");
            Interact();
        }

    }

    public void Interact()
    {
        if (!canInteract) return;

        manager.StepRock(transform.position.x);
    }

    public void SetSprite()
    {
        _spriteRenderer.sprite = _posibleSprites[UnityEngine.Random.Range(0, _posibleSprites.Length)];
        transform.rotation = Quaternion.Euler(0, 0, UnityEngine.Random.Range(0, 360));
    }

    public void StepRock()
    {
        if (!gameObject.activeInHierarchy) return;

        canInteract = false;
        _currentStep++;

        if (_currentStep == 3)
            StartCoroutine(SetActive());
        transform.position += new Vector3(0, _stepDistance, 0);

        if(_currentStep > _maxStep)
        {
            _currentStep = 0;
            canInteract = false ;
            TurnOff(this);
        }
    }

    IEnumerator SetActive()
    {
        yield return new WaitForEndOfFrame();
        canInteract = true;

    }

    void CheckSwipe(SwipeData data)
    {
        var swipeDir = data.points[data.points.Count - 1] - data.points[0];

        var swipeAngle = Vector2.Angle(Vector2.up, swipeDir);

        switch (swipeAngle)
        {
            case < 45:
                Debug.Log("Swipe para arriba");
                if (inputSwipe == SwipeDirection.Up)
                    Interact();
                break;
            case > 135:
                Debug.Log("Swipe para abajo");
                if (inputSwipe == SwipeDirection.Down)
                    Interact();
                break;
            case >= 45:
                if(swipeDir.x > 0)
                {
                    Debug.Log("Swipe para Derecha");
                    if (inputSwipe == SwipeDirection.Right)
                        Interact();
                }
                else
                {
                    Debug.Log("Swipe para Izquierda");
                    if (inputSwipe == SwipeDirection.Left)
                        Interact();
                }
                break;
        }

        //Debug.Log($"SwipeAngle = {swipeAngle}");

    }

    public void GameOver()
    {
        TurnOff(this);
    }

    public void SubscribeToGameOver()
    {
        EscaladaManager.Instance.OnGameOver += GameOver;
    }

    public void SubscribeToSwipe()
    {
        Debug.Log(SwipeManager.instance);
        SwipeManager.instance.OnSwipeEnd += CheckSwipe;
    }
}



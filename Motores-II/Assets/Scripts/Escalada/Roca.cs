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
    

    private void Update()
    {
        if (!canInteract && _currentStep == 3)
            canInteract = true;

        if(canInteract && Input.GetKeyDown(interactKey))
        {
            Debug.Log($"<color=green>No me toques mi chilito</color>");
            Interact();
        }
    }

    public void Interact()
    {
        if (!canInteract) return;

        manager.StepRock();
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
            canInteract = true;
        transform.position += new Vector3(0, _stepDistance, 0);

        if(_currentStep > _maxStep)
        {
            _currentStep = 0;
            canInteract = false ;
            TurnOff(this);
        }
    }
}

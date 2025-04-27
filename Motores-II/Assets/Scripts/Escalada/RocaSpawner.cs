using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocaSpawner : MonoBehaviour
{
    [SerializeField] EscaladaManager _manager;
    [SerializeField] Roca _rocaPrefab;
    [SerializeField] Transform _spawnPos;

    ObjectPool<Roca> _pool;

    public bool canSpawn;
    [SerializeField] KeyCode _interactKey = KeyCode.None;
    [SerializeField] SwipeDirection _inputSwipe;


    private void Awake()
    {
        _pool = new ObjectPool<Roca>(CreateRock, TurnOnRock, TurnOffRock);
    }

    public void StepRock()
    {
        if (canSpawn)
            _pool.Get();
    }

    void DeactivateRock(Roca rock)
    {
        _pool.Return(rock);
    }

    #region Factory
    Roca CreateRock()
    {
        var roca = Instantiate(_rocaPrefab, _spawnPos.position, Quaternion.identity);

        TurnOffRock(roca);
        _manager.StepTrigger += roca.StepRock;
        roca.TurnOff += DeactivateRock;
        roca.interactKey = _interactKey;
        roca.inputSwipe = _inputSwipe;
        roca.SubscribeToSwipe();
        roca.manager = _manager;

        return roca;
    }

    void TurnOnRock(Roca rock)
    {
        rock.gameObject.SetActive(true);
        rock.SetSprite();

        rock.transform.position = _spawnPos.position;
    }

    void TurnOffRock(Roca rock)
    {
        rock.gameObject.SetActive(false);
    } 
    #endregion
}

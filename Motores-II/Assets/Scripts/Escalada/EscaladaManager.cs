using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscaladaManager : MonoBehaviour
{
    [SerializeField] RocaSpawner[] _spawners;

    public event Action StepTrigger;

    bool _gameStarted = false;

    private void Update()
    {




        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!_gameStarted)
            {
                StartGame();
                return;
            }

            StepRock();
        }
    }

    void StartGame()
    {
        for (int i = 0; i < 5; i++)
        {
            StepRock();
        }
    }

    public void StepRock()
    {
        var num = UnityEngine.Random.Range(0, _spawners.Length+1);
        var num2 = UnityEngine.Random.Range(0, _spawners.Length+1);

        for (int i = 0; i < _spawners.Length; i++)
        {
            if (i == num || i == num2)
                _spawners[i].canSpawn = false;
            else
                _spawners[i].canSpawn = true;

            _spawners[i].StepRock();
        }

        StepTrigger();

    }
}

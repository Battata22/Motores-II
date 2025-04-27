using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscaladaManager : MonoBehaviour
{
    [SerializeField] RocaSpawner[] _spawners;
    [SerializeField] PlayerEscalada _player;

    public event Action StepTrigger;

    bool _gameStarted = false;

    private IEnumerator Start()
    {

        yield return new WaitForEndOfFrame();
        StartGame();
    }

    private void Update()
    {




        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!_gameStarted)
            {
                StartGame();
                return;
            }

            StepRock(0);
        }
    }

    void StartGame()
    {
        for (int i = 0; i < 3; i++)
        {
            StepRock(0);
        }
    }

    public void StepRock(float x)
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
        _player.SetPos(x);
    }
}

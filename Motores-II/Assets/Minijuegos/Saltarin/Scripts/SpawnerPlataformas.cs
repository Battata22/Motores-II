using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerPlataformas : MonoBehaviour
{
    [SerializeField] float _xIzqSpawn, _xMedioSpawn, _xDerSpawn;
    [SerializeField] float _offset, _offsetDist;
    [SerializeField] float _spawnCooldown;
    [SerializeField] GameObject _platPrefab;
    float waitSpawn = 0.5f;

    void Start()
    {

    }


    void Update()
    {
        waitSpawn += Time.deltaTime;

        if (waitSpawn >= _spawnCooldown)
        {
            int choose = Random.Range(1, 3 + 1);
            switch (choose)
            {
                case 1:
                    //spawn corto
                    var izq = Instantiate(_platPrefab, new Vector3(_xIzqSpawn, 0, _offset), Quaternion.identity);
                    break;

                case 2:
                    //spawn medio
                    var medio = Instantiate(_platPrefab, new Vector3(_xMedioSpawn, 0, _offset), Quaternion.identity);
                    break;

                case 3:
                    //spawn lejos
                    var der = Instantiate(_platPrefab, new Vector3(_xDerSpawn, 0, _offset), Quaternion.identity);
                    break;

            }
            _offset += _offsetDist;
            waitSpawn = 0;
        }
    }
}


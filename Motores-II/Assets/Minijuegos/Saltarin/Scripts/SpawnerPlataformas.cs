using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnerPlataformas : MonoBehaviour
{
    [SerializeField] float _xIzqSpawn, _xMedioSpawn, _xDerSpawn;
    [SerializeField] float _offset, _offsetDist;
    [SerializeField] float _spawnCooldown;
    [SerializeField] GameObject _platPrefab;
    [SerializeField] Canvas _canvas;
    [SerializeField] Image _avisoPrefab;
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
                    var aloha = izq.GetComponent<PlataformaScript>();
                    aloha._canvas = _canvas;
                    aloha._avisoPrefab = _avisoPrefab;
                    izq.SetActive(true);
                    break;

                case 2:
                    //spawn medio
                    var medio = Instantiate(_platPrefab, new Vector3(_xMedioSpawn, 0, _offset), Quaternion.identity);
                    var aloha2 = medio.GetComponent<PlataformaScript>();
                    aloha2._canvas = _canvas;
                    aloha2._avisoPrefab = _avisoPrefab;
                    medio.SetActive(true);
                    break;

                case 3:
                    //spawn lejos
                    var der = Instantiate(_platPrefab, new Vector3(_xDerSpawn, 0, _offset), Quaternion.identity);
                    var aloha3 = der.GetComponent<PlataformaScript>();
                    aloha3._canvas = _canvas;
                    aloha3._avisoPrefab = _avisoPrefab;
                    der.SetActive(true);
                    break;

            }
            _offset += _offsetDist;
            waitSpawn = 0;
        }
    }
}


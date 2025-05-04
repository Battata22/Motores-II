using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.Services.RemoteConfig;

public class AttackSpawner : MonoBehaviour
{
    [SerializeField] Attack attackPrefab;
    [SerializeField] Transform[] _spawnPos;
    [SerializeField] GameObject _alerta;
    [SerializeField] float[] randomTimeLimits = new float[2];
    [SerializeField] float alertTime;

    float randomTime;

    bool _canSpawn = true;

    private void Awake()
    {
        randomTime = 1;
        StartCoroutine(AttackCounter(randomTime));
        RemoteConfigManager.Instance.OnConfigFetched += SetData;
        
    }

    private void Start()
    {
        EscaladaManager.Instance.OnGameOver += GameOver;

        SetData();
        //randomTimeLimits[0] = RemoteConfigManager.Instance.escaladaAtkTimeMin;
        //randomTimeLimits[1] = RemoteConfigManager.Instance.escaladaAtkTimeMax;
    }

    //private void Awake()
    //{
    //    RemoteConfigManager.Instance.OnConfigFetched += SetData;
    //}

    void SetData()
    {
        randomTimeLimits[0] = RemoteConfigManager.Instance.Escalada_randomTimeRange[0];
        randomTimeLimits[1] = RemoteConfigManager.Instance.Escalada_randomTimeRange[1];
    }


    IEnumerator AttackCounter(float wait)
    {
        yield return new WaitForSeconds(wait);

        if (_canSpawn)
            StartCoroutine(StartAttack());
    }

    IEnumerator StartAttack()
    {
        //elegir spawn
        var index = ChoseSpawner();
        //activar alerta
        _alerta.transform.position = _spawnPos[index].position;
        _alerta.SetActive(true);
        // esperar
        yield return new WaitForSeconds(alertTime);
        //desactivar alerta
        _alerta.SetActive(false);
        //atacar
        var attack = Instantiate(attackPrefab, _spawnPos[index].position, Quaternion.identity);
        attack.gameObject.SetActive(true);
        //loop
        StartCoroutine(AttackCounter(SetRandomTime()));
    }

    public void GameOver() 
    {
        _canSpawn = false;
    }

    int ChoseSpawner()
    {      
        int index = Random.Range(0, _spawnPos.Length);
        return index;
    }

    float SetRandomTime()
    {
        float time = Random.Range(randomTimeLimits[0], randomTimeLimits[1]);
        return time;
    }

}

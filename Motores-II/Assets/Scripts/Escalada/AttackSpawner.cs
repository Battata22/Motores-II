using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpawner : MonoBehaviour
{
    [SerializeField] Attack attackPrefab;
    [SerializeField] Transform[] _spawnPos;
    [SerializeField] GameObject _alerta;
    [SerializeField] float[] randomTimeLimits = new float[2];
    [SerializeField] float alertTime;

    float randomTime;

    private void Awake()
    {
        randomTime = 0;
        StartCoroutine(AttackCounter(randomTime));
    }

    IEnumerator AttackCounter(float wait)
    {
        yield return new WaitForSeconds(wait);

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

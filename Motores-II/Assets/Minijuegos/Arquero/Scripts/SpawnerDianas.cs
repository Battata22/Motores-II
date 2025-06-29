using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerDianas : MonoBehaviour
{
    [SerializeField] float _zCerca, _zMedio, _zLejos;
    [SerializeField] float _xLimite, _yLimite;
    [SerializeField] float _sizecerca, _sizeMedio, _sizeLejos;
    [SerializeField] float _spawnCooldown;
    [SerializeField] GameObject _dianasPrefab;

    [SerializeField] float _destroyTime;

    float waitSpawn;
    
    void Update()
    {
        if (!PausaInGame.Instance.isPaused)
        {
            waitSpawn += Time.deltaTime;

            if (waitSpawn >= _spawnCooldown)
            {
                int choose = Random.Range(1, 3 + 1);
                switch (choose)
                {
                    case 1:
                        //spawn corto
                        var c = Instantiate(_dianasPrefab, new Vector3(Random.Range(-_xLimite, _xLimite), Random.Range(-_yLimite, _yLimite), _zCerca), Quaternion.identity);
                        c.transform.localScale = Vector3.one * _sizecerca;
                        Destroy(c, _destroyTime);
                        break;

                    case 2:
                        //spawn medio
                        var m = Instantiate(_dianasPrefab, new Vector3(Random.Range(-_xLimite, _xLimite), Random.Range(-_yLimite, _yLimite), _zMedio), Quaternion.identity);
                        m.transform.localScale = Vector3.one * _sizeMedio;
                        Destroy(m, _destroyTime);
                        break;

                    case 3:
                        //spawn lejos
                        var l = Instantiate(_dianasPrefab, new Vector3(Random.Range(-_xLimite, _xLimite), Random.Range(-_yLimite, _yLimite), _zLejos), Quaternion.identity);
                        l.transform.localScale = Vector3.one * _sizeLejos;
                        Destroy(l, _destroyTime);
                        break;

                }
                waitSpawn = 0;
            }
        }
    }
}

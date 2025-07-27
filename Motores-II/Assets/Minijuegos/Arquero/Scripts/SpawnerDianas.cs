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
    [SerializeField] Material _default, dianaPapel, dianaDiana, selected;

    float waitSpawn;

    private void Start()
    {
        if (PlayerPrefs.GetInt("SelectedDiana") == 5)
        {
            selected = dianaPapel;
        }
        else if (PlayerPrefs.GetInt("SelectedDiana") == 6)
        {
            selected = dianaDiana;
        }
        else if (PlayerPrefs.GetInt("SelectedBall") == 0)
        {
            selected = _default;
        }
    }

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
                        //var c = Instantiate(_dianasPrefab, new Vector3(Random.Range(-_xLimite, _xLimite), Random.Range(-_yLimite, _yLimite), _zCerca), Quaternion.identity);
                        var pos = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(Screen.safeArea.xMin, Screen.safeArea.xMax), Random.Range(Screen.safeArea.yMin, Screen.safeArea.yMax), 0));
                        pos.z = _zCerca;

                        var c = Instantiate(_dianasPrefab, pos, Quaternion.identity);
                        //var c = Instantiate(_dianasPrefab, new Vector3(Random.Range(Screen.safeArea.xMin, Screen.safeArea.xMax), Random.Range(Screen.safeArea.yMin, Screen.safeArea.yMax), _zCerca), Quaternion.identity);
                        
                        //var c = Instantiate(_dianasPrefab, new Vector3(Random.Range(-Screen.safeArea.x, Screen.safeArea.x), Random.Range(-Screen.safeArea.y, Screen.safeArea.y), _zCerca), Quaternion.identity);

                        c.transform.localScale = Vector3.one * _sizecerca;
                        //Destroy(c, _destroyTime);
                        c.GetComponent<Renderer>().material = selected;
                        break;

                    case 2:
                        #region Old
                        ////spawn medio
                        //var m = Instantiate(_dianasPrefab, new Vector3(Random.Range(-_xLimite, _xLimite), Random.Range(-_yLimite, _yLimite), _zMedio), Quaternion.identity);
                        //m.transform.localScale = Vector3.one * _sizeMedio;
                        ////Destroy(m, _destroyTime);
                        //m.GetComponent<Renderer>().material = selected; 
                        #endregion
                        //spawn medio
                        var pos2 = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(Screen.safeArea.xMin, Screen.safeArea.xMax), Random.Range(Screen.safeArea.yMin, Screen.safeArea.yMax), 0));
                        pos2.z = _zMedio;

                        var m = Instantiate(_dianasPrefab, pos2, Quaternion.identity);

                        m.transform.localScale = Vector3.one * _sizeMedio;
                        m.GetComponent<Renderer>().material = selected;
                        break;

                    case 3:
                        #region Old
                        ////spawn lejos
                        //var l = Instantiate(_dianasPrefab, new Vector3(Random.Range(-_xLimite, _xLimite), Random.Range(-_yLimite, _yLimite), _zLejos), Quaternion.identity);
                        //l.transform.localScale = Vector3.one * _sizeLejos;
                        ////Destroy(l, _destroyTime);
                        //l.GetComponent<Renderer>().material = selected; 
                        #endregion
                        //spawn medio
                        var pos3 = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(Screen.safeArea.xMin, Screen.safeArea.xMax), Random.Range(Screen.safeArea.yMin, Screen.safeArea.yMax), 0));
                        pos3.z = _zLejos;

                        var l = Instantiate(_dianasPrefab, pos3, Quaternion.identity);

                        l.transform.localScale = Vector3.one * _sizeLejos;
                        l.GetComponent<Renderer>().material = selected;
                        break;

                }
                waitSpawn = 0;
            }
        }
    }

}

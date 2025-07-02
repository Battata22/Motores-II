using UnityEngine;

public class SpawnerPlataformasPool : MonoBehaviour
{
    [SerializeField] float _spawnTime;
    [SerializeField] PlataformaScript _plataforma;
    ObjectPool<PlataformaScript> _poolPlataformas;
    float waitSpawn;

    [SerializeField] float _xIzqSpawn, _xMedioSpawn, _xDerSpawn;
    [SerializeField] float _offset, _offsetDist;

    private void Awake()
    {
        _poolPlataformas = new ObjectPool<PlataformaScript>(SpawnPlataforma, PlatformOn, PlatformOff, 5);
    }

    void Start()
    {
        SaltarinManager.instance.TriggerStep += Spawn;
    }


    void Update()
    {
        if (!PausaInGame.Instance.isPaused)
        {
            int choose = Random.Range(1, 3 + 1);
            switch (choose)
            {
                case 1:
                    //spawn corto
                    var izq = Instantiate(_platPrefab, new Vector3(_xIzqSpawn, 0, _offset), Quaternion.identity);
                    izq.SetActive(true);
                    break;

                case 2:
                    //spawn medio
                    var medio = Instantiate(_platPrefab, new Vector3(_xMedioSpawn, 0, _offset), Quaternion.identity);
                    medio.SetActive(true);
                    break;

                case 3:
                    //spawn lejos
                    var der = Instantiate(_platPrefab, new Vector3(_xDerSpawn, 0, _offset), Quaternion.identity);
                    der.SetActive(true);
                    break;

            }
            _offset += _offsetDist;
        }
    }

    void Spawn()
    {
        var enemy = _poolPlataformas.Get();
    }

    PlataformaScript SpawnPlataforma()
    {
        //---------------------------------DEFINIR EL PUNTO DE SPAWN----------------------------------
        //float x = transform.position.x;
        //float y = transform.position.y;
        //float z = transform.position.z;

        return Instantiate(_plataforma, new Vector3(), Quaternion.identity);
    }

    void PlatformOn(PlataformaScript _plataforma)
    {
        _plataforma.gameObject.SetActive(true);
        _plataforma.Recicle(_poolPlataformas);
    }

    void PlatformOff(PlataformaScript _plataforma)
    {
        _plataforma.gameObject.SetActive(false);
        //por las dudas
        _plataforma.Recicle(_poolPlataformas);
    }
}

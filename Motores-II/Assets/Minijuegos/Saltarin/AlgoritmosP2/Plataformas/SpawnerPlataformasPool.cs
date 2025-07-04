using UnityEngine;

public class SpawnerPlataformasPool : MonoBehaviour
{
    [SerializeField] PlataformaScript _plataforma;
    ObjectPool<PlataformaScript> _poolPlataformas;

    [SerializeField] float _xIzqSpawn, _xMedioSpawn, _xDerSpawn;
    [SerializeField] float _offsetDist;
    //float _offset;

    private void Awake()
    {
        //_offset = _offsetDist;
        _poolPlataformas = new ObjectPool<PlataformaScript>(SpawnPlataforma, PlatformOn, PlatformOff, 10);
    }

    void Start()
    {
        SaltarinManager.instance.TriggerStep += Spawn;
    }

    float wait;
    void Update()
    {
        #region Spawn Por Tiempo
        //wait += Time.deltaTime;
        //if (wait >= 2)
        //{
        //    if (!PausaInGame.Instance.isPaused)
        //    {
        //        int choose = Random.Range(1, 3 + 1);
        //        switch (choose)
        //        {
        //            case 1:
        //                //-------------------------------------------Spawn Izquierda---------------------------------------------

        //                //var izq = Instantiate(_plataforma, new Vector3(_xIzqSpawn, 0, _offset), Quaternion.identity);
        //                //izq.SetActive(true);
        //                //
        //                var plataformaIzq = _poolPlataformas.Get();
        //                plataformaIzq.transform.position = new Vector3(_xIzqSpawn, 0, _offsetDist);
        //                break;

        //            case 2:
        //                //-------------------------------------------Spawn Medio---------------------------------------------

        //                //var medio = Instantiate(_plataforma, new Vector3(_xMedioSpawn, 0, _offset), Quaternion.identity);
        //                //medio.SetActive(true);

        //                var plataformaMid = _poolPlataformas.Get();
        //                plataformaMid.transform.position = new Vector3(_xMedioSpawn, 0, _offsetDist);
        //                break;

        //            case 3:
        //                //-------------------------------------------Spawn Derecha---------------------------------------------

        //                //var der = Instantiate(_plataforma, new Vector3(_xDerSpawn, 0, _offset), Quaternion.identity);
        //                //der.SetActive(true);

        //                var plataformaDer = _poolPlataformas.Get();
        //                plataformaDer.transform.position = new Vector3(_xDerSpawn, 0, _offsetDist);
        //                break;

        //        }
        //        //_offset += _offsetDist;
        //    }
        //    wait = 0;
        //}         
        #endregion
    }

    void Spawn()
    {
        if (!PausaInGame.Instance.isPaused)
        {
            int choose = Random.Range(1, 3 + 1);
            switch (choose)
            {
                case 1:
                    //-------------------------------------------Spawn Izquierda---------------------------------------------

                    //var izq = Instantiate(_plataforma, new Vector3(_xIzqSpawn, 0, _offset), Quaternion.identity);
                    //izq.SetActive(true);
                    //
                    var plataformaIzq = _poolPlataformas.Get();
                    plataformaIzq.transform.position = new Vector3(_xIzqSpawn, 0, _offsetDist);
                    break;

                case 2:
                    //-------------------------------------------Spawn Medio---------------------------------------------

                    //var medio = Instantiate(_plataforma, new Vector3(_xMedioSpawn, 0, _offset), Quaternion.identity);
                    //medio.SetActive(true);

                    var plataformaMid = _poolPlataformas.Get();
                    plataformaMid.transform.position = new Vector3(_xMedioSpawn, 0, _offsetDist);
                    break;

                case 3:
                    //-------------------------------------------Spawn Derecha---------------------------------------------

                    //var der = Instantiate(_plataforma, new Vector3(_xDerSpawn, 0, _offset), Quaternion.identity);
                    //der.SetActive(true);

                    var plataformaDer = _poolPlataformas.Get();
                    plataformaDer.transform.position = new Vector3(_xDerSpawn, 0, _offsetDist);
                    break;

            }
            //_offset += _offsetDist;
        }
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

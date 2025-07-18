using UnityEngine;

public class SpawnerPlataformasPool : MonoBehaviour
{
    [SerializeField] PlataformaScript _plataform;
    ObjectPool<PlataformaScript> _poolPlataformas;

    [SerializeField] float _xIzqSpawn, _xMedioSpawn, _xDerSpawn;
    [SerializeField] float _offsetDist;

    [SerializeField] int obstOffset, obstOffsetDesired;
    //float _offset;

    private void Awake()
    {
        //_offset = _offsetDist;
        _poolPlataformas = new ObjectPool<PlataformaScript>(SpawnPlataforma, PlatformOn, PlatformOff, 10);
    }

    void Start()
    {
        SaltarinManager.instance.TriggerStep += Spawn;
        obstOffset = 0;
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
            if (obstOffset < obstOffsetDesired)
            {
                obstOffset++;
                SpawnNormal();
            }
            else
            {
                obstOffset = 0;
                //spawn doble con obstaculo
                SpawnObstacle();
            }
        }
    }

    void SpawnNormal()
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

    void SpawnObstacle()
    {

        int choose = Random.Range(1, 3 + 1);
        int lastChoice = choose;
        switch (choose)
        {
            case 1:
                //-------------------------------------------Spawn Izquierda---------------------------------------------

                var plataformaIzq = _poolPlataformas.Get();
                plataformaIzq.transform.position = new Vector3(_xIzqSpawn, -0.01f, _offsetDist);
                break;

            case 2:
                //-------------------------------------------Spawn Medio---------------------------------------------

                var plataformaMid = _poolPlataformas.Get();
                plataformaMid.transform.position = new Vector3(_xMedioSpawn, 0, _offsetDist);
                break;

            case 3:
                //-------------------------------------------Spawn Derecha---------------------------------------------

                var plataformaDer = _poolPlataformas.Get();
                plataformaDer.transform.position = new Vector3(_xDerSpawn, -0.02f, _offsetDist);
                break;

        }

        if (choose == 1)
        {
            choose = Random.Range(2, 3 + 1);

            switch (choose)
            {
                case 2:
                    //-------------------------------------------Spawn Medio---------------------------------------------

                    var plataformaMid = _poolPlataformas.Get();
                    plataformaMid.transform.position = new Vector3(_xMedioSpawn, 0, _offsetDist);
                    PutObstacle(plataformaMid.transform);
                    break;

                case 3:
                    //-------------------------------------------Spawn Derecha---------------------------------------------

                    var plataformaDer = _poolPlataformas.Get();
                    plataformaDer.transform.position = new Vector3(_xDerSpawn, -0.02f, _offsetDist);
                    PutObstacle(plataformaDer.transform);
                    break;

            }
        }
        else if (choose == 2)
        {
            choose = Random.Range(1, 2 + 1);

            switch (choose)
            {
                case 1:
                    //-------------------------------------------Spawn Izquierda---------------------------------------------

                    var plataformaIzq = _poolPlataformas.Get();
                    plataformaIzq.transform.position = new Vector3(_xIzqSpawn, -0.01f, _offsetDist);
                    PutObstacle(plataformaIzq.transform);
                    break;


                case 2:
                    //-------------------------------------------Spawn Derecha---------------------------------------------

                    var plataformaDer = _poolPlataformas.Get();
                    plataformaDer.transform.position = new Vector3(_xDerSpawn, -0.02f, _offsetDist);
                    PutObstacle(plataformaDer.transform);
                    break;

            }
        }
        else if (choose == 3)
        {
            choose = Random.Range(1, 2 + 1);

            switch (choose)
            {
                case 1:
                    //-------------------------------------------Spawn Izquierda---------------------------------------------

                    var plataformaIzq = _poolPlataformas.Get();
                    plataformaIzq.transform.position = new Vector3(_xIzqSpawn, -0.01f, _offsetDist);
                    PutObstacle(plataformaIzq.transform);
                    break;

                case 2:
                    //-------------------------------------------Spawn Medio---------------------------------------------

                    var plataformaMid = _poolPlataformas.Get();
                    plataformaMid.transform.position = new Vector3(_xMedioSpawn, 0, _offsetDist);
                    PutObstacle(plataformaMid.transform);
                    break;

            }
        }

    }


    [SerializeField] Obstacle[] _obstacles;

    void PutObstacle(Transform platTransform)
    {
        int random = Random.Range(0, _obstacles.Length);

        _obstacles[random].TurnOn();
        _obstacles[random].Follow = platTransform;

        //_obstacles[7].TurnOn();
        //_obstacles[7].Follow = platTransform;

        #region Comment
        //if (random == 1)
        //{
        //    PutWood(platTransform);
        //}
        //else if (random == 2)
        //{
        //    PutBroken(platTransform);
        //}
        //else if (random == 3)
        //{
        //    PutSquid(platTransform);
        //}
        //else if (random == 4)
        //{
        //    PutNiebla(platTransform);
        //} 
        #endregion
    }

    #region Comment
    //[SerializeField] Obstacle _wood;
    //void PutWood(Transform platTransform)
    //{
    //    _wood.TurnOn();
    //    _wood.Follow = platTransform;
    //}

    //[SerializeField] Obstacle _broken;
    //void PutBroken(Transform platTransform)
    //{
    //    _broken.TurnOn();
    //    _broken.Follow = platTransform;
    //}

    //[SerializeField] Obstacle _squid;
    //void PutSquid(Transform platTransform)
    //{
    //    _squid.TurnOn();
    //    _squid.Follow = platTransform;
    //}

    //[SerializeField] Obstacle _nieblaWarning;
    //void PutNiebla(Transform platTransform)
    //{
    //    _nieblaWarning.TurnOn();
    //    _nieblaWarning.Follow = platTransform;
    //} 
    #endregion

    PlataformaScript SpawnPlataforma()
    {
        //---------------------------------DEFINIR EL PUNTO DE SPAWN----------------------------------
        //float x = transform.position.x;
        //float y = transform.position.y;
        //float z = transform.position.z;

        return Instantiate(_plataform, new Vector3(), Quaternion.identity);
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

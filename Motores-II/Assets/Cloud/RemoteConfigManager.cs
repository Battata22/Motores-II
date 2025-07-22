using System.Threading.Tasks;
using Unity.Services.RemoteConfig;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;
using System;

public class RemoteConfigManager : MonoBehaviour
{
    public static RemoteConfigManager Instance { get; private set; }

    public struct userAttributes { }
    public struct appAttributes { }

    public event Action OnConfigFetched;

    async Task InitializeRemoteConfigAsync()
    {
        // initialize handlers for unity game services
        await UnityServices.InitializeAsync();

        // remote config requires authentication for managing environment information
        if (!AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(gameObject);

        //No sacar
        DontDestroyOnLoad(gameObject);

        LoadLastInfo();
    }

    async Task Start()
    {
        // initialize Unity's authentication and core services, however check for internet connection
        // in order to fail gracefully without throwing exception if connection does not exist
        if (Utilities.CheckForInternetConnection())
        {
            await InitializeRemoteConfigAsync();
        }

        RemoteConfigService.Instance.FetchCompleted += ApplyRemoteSettings;
        RemoteConfigService.Instance.FetchConfigs(new userAttributes(), new appAttributes());
    }

    public float runner_speed;
    public float _puntMulti;
    public string _titulo;
    public bool _tiendaState = true;

    [Header("CARLOS")]
    [SerializeField] int _maxRound;
    [SerializeField] int _initialButtonsCount;
    [SerializeField] bool _endless;

    public int Carlos_MaxRound { get { return _maxRound; } }
    public int Carlos_InitialButtonsCount{ get { return _initialButtonsCount; } }
    public bool Carlos_Endless { get { return _endless; } }

    [Header("Escalada")]
    [SerializeField] int _maxHeight;
    [SerializeField] public float[] Escalada_randomTimeRange = new float[2];
    public int Escalada_MaxHeight { get { return _maxHeight; } }
    
    [SerializeField] LoadMenu loadScript;


    [Header("BulletHell")]
    [SerializeField] int _bossDmg;
    [SerializeField] float _bossMaxHP;
    [SerializeField] int _enemyDmg;
    [SerializeField] int _playerDmg;
    [SerializeField] float _playerSpeed;
    [SerializeField] float _powerUpDuration;
    [SerializeField] float _powerUpSpawnCD;

    public int BossDmg {  get { return _bossDmg; } }
    public float BossMaxHP { get { return _bossMaxHP; } }
    public int EnemyDmg {  get { return _enemyDmg; } }
    public int PlayerDmg {  get { return _playerDmg; } }
    public float PlayerSpeed { get { return _playerSpeed; } }
    public float PowerUpDuration { get { return _powerUpDuration; } }
    public float PowerUpSpawnCD { get { return _powerUpSpawnCD; } }





    void ApplyRemoteSettings(ConfigResponse configResponse)
    {
        Debug.Log("RemoteConfigService.Instance.appConfig fetched: " + RemoteConfigService.Instance.appConfig.config.ToString());

        //Yo voy a setear las variables aca y voy a acceder a este script para agarrarlas en vez de usar el evento por que por como funca lo mio no lo agarra
        runner_speed = RemoteConfigService.Instance.appConfig.GetFloat("Running_Speed");
        _puntMulti = RemoteConfigService.Instance.appConfig.GetFloat("Archer_PuntMultiply");
        _titulo = RemoteConfigService.Instance.appConfig.GetString("TituloText");
        _tiendaState = RemoteConfigService.Instance.appConfig.GetBool("ShopActive");

        //carlos
        _maxRound = RemoteConfigService.Instance.appConfig.GetInt("Carlos_MaxRounds");
        _initialButtonsCount = RemoteConfigService.Instance.appConfig.GetInt("Carlos_InitialRoundButtons");
        _endless = RemoteConfigService.Instance.appConfig.GetBool("Carlos_Endless");

        //Escalada

        _maxHeight = RemoteConfigService.Instance.appConfig.GetInt("Escalada_MaxHeight");
        Escalada_randomTimeRange[0] = RemoteConfigService.Instance.appConfig.GetFloat("Escalada_MinTimeAtk");
        Escalada_randomTimeRange[1] = RemoteConfigService.Instance.appConfig.GetFloat("Escalada_MaxTimeAtk");

        //Bullethell
        _bossDmg = RemoteConfigService.Instance.appConfig.GetInt("BulletHell_BossDamage");
        _enemyDmg = RemoteConfigService.Instance.appConfig.GetInt("BulletHell_EnemyDamage");
        _playerDmg = RemoteConfigService.Instance.appConfig.GetInt("BulletHell_PlayerDamage");
        _bossMaxHP = RemoteConfigService.Instance.appConfig.GetFloat("BulletHell_BossMaxHP");
        _playerSpeed = RemoteConfigService.Instance.appConfig.GetFloat("BulletHell_PlayerSpeed");
        _powerUpDuration = RemoteConfigService.Instance.appConfig.GetFloat("BulletHell_PowerUpDuration");
        _powerUpSpawnCD = RemoteConfigService.Instance.appConfig.GetFloat("BulletHell_PowerUpSpawnCD");

        //CONSULTAR AL PROFE POR COMO HACER QUE SE ACTUALICE CUANDO CAMBIE, CON EL EVENTO?
        //Aun asi el evento queda configurado y funcional supuestamente
        OnConfigFetched?.Invoke();
        loadScript.chargeDone = true;

        //Te dejo aca lo que tendrias que poner en tus scripts para adaptarlos con el evento

        //private void Awake()
        //{
        //    RemoteConfigManager.Instance.OnConfigFetched += SetData;
        //}

        //void SetData()
        //{
        //    [Tu Variable] = RemoteConfigService.Instance.appConfig.GetFloat("[Nombre de tu variable en la nube]");
        //}

        SaveLastInfo();
    }

    public void SaveLastInfo()
    {
        //LastCloudInfo data = new();
        ////data.runner_speed = RemoteConfigService.Instance.appConfig.GetFloat("Running_Speed");
        ////data._puntMulti = RemoteConfigService.Instance.appConfig.GetFloat("Archer_PuntMultiply");
        ////data._titulo = RemoteConfigService.Instance.appConfig.GetString("TituloText");
        ////data._tiendaState = RemoteConfigService.Instance.appConfig.GetBool("ShopActive");
        //data.runner_speed = runner_speed;
        //data._puntMulti = _puntMulti;
        //data._titulo = _titulo;
        //data._tiendaState = _tiendaState;

        //string json = JsonUtility.ToJson(data, true);
        //File.WriteAllText(Application.dataPath + "/Cloud/LastCloudInfo.json", json);

        PlayerPrefs.SetFloat("runner_speed", runner_speed);
        PlayerPrefs.SetFloat("puntMulti", _puntMulti);
        PlayerPrefs.SetString("titulo", _titulo);
        PlayerPrefs.SetInt("tiendaStateBool", BoolToInt(_tiendaState));
        PlayerPrefs.Save();
    }

    bool IntToBool(int i)
    {
        if (i == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    int BoolToInt(bool i)
    {
        if (i == false)
        {
            return 0;
        }
        else
        {
            return 1;
        }
    }

    public void LoadLastInfo()
    {
        //if (File.Exists(Application.dataPath + "/Cloud/LastCloudInfo.json") == true)
        //{
        //    string json = File.ReadAllText(Application.dataPath + "/Cloud/LastCloudInfo.json");
        //    LastCloudInfo data = JsonUtility.FromJson<LastCloudInfo>(json);

        //    runner_speed = data.runner_speed;
        //    _puntMulti = data._puntMulti;
        //    _titulo = data._titulo;
        //    _tiendaState = data._tiendaState;
        //}

        if (PlayerPrefs.HasKey("runner_speed"))
        {
            runner_speed = PlayerPrefs.GetFloat("runner_speed");
            _puntMulti = PlayerPrefs.GetFloat("puntMulti");
            _titulo = PlayerPrefs.GetString("titulo");
            _tiendaState = IntToBool(PlayerPrefs.GetInt("tiendaStateBool"));
        }
    }


}
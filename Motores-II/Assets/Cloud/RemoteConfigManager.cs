using System.Threading.Tasks;
using Unity.Services.RemoteConfig;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;
using System;
using System.IO;

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

    void ApplyRemoteSettings(ConfigResponse configResponse)
    {
        Debug.Log("RemoteConfigService.Instance.appConfig fetched: " + RemoteConfigService.Instance.appConfig.config.ToString());

        //Yo voy a setear las variables aca y voy a acceder a este script para agarrarlas en vez de usar el evento por que por como funca lo mio no lo agarra
        runner_speed = RemoteConfigService.Instance.appConfig.GetFloat("Running_Speed");
        _puntMulti = RemoteConfigService.Instance.appConfig.GetFloat("Archer_PuntMultiply");
        _titulo = RemoteConfigService.Instance.appConfig.GetString("TituloText");
        _tiendaState = RemoteConfigService.Instance.appConfig.GetBool("ShopActive");

        //CONSULTAR AL PROFE POR COMO HACER QUE SE ACTUALICE CUANDO CAMBIE, CON EL EVENTO?
        //Aun asi el evento queda configurado y funcional supuestamente
        OnConfigFetched?.Invoke();

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
        LastCloudInfo data = new();
        //data.runner_speed = RemoteConfigService.Instance.appConfig.GetFloat("Running_Speed");
        //data._puntMulti = RemoteConfigService.Instance.appConfig.GetFloat("Archer_PuntMultiply");
        //data._titulo = RemoteConfigService.Instance.appConfig.GetString("TituloText");
        //data._tiendaState = RemoteConfigService.Instance.appConfig.GetBool("ShopActive");
        data.runner_speed = runner_speed;
        data._puntMulti = _puntMulti;
        data._titulo = _titulo;
        data._tiendaState = _tiendaState;

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(Application.dataPath + "/Cloud/LastCloudInfo.json", json);
    }

    public void LoadLastInfo()
    {
        if (File.Exists(Application.dataPath + "/Cloud/LastCloudInfo.json") == true)
        {
            string json = File.ReadAllText(Application.dataPath + "/Cloud/LastCloudInfo.json");
            LastCloudInfo data = JsonUtility.FromJson<LastCloudInfo>(json);

            runner_speed = data.runner_speed;
            _puntMulti = data._puntMulti;
            _titulo = data._titulo;
            _tiendaState = data._tiendaState;
        }
    }


}
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

    void ApplyRemoteSettings(ConfigResponse configResponse)
    {
        Debug.Log("RemoteConfigService.Instance.appConfig fetched: " + RemoteConfigService.Instance.appConfig.config.ToString());

        //Yo voy a setear las variables aca y voy a acceder a este script para agarrarlas en vez de usar el evento por que por como funca lo mio no lo agarra
        runner_speed = RemoteConfigService.Instance.appConfig.GetFloat("Running_Speed");
        _puntMulti = RemoteConfigService.Instance.appConfig.GetFloat("Archer_PuntMultiply");

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
    }


}
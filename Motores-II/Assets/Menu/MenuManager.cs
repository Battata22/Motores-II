using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _titulo;
    bool titulocambiado = false;

    [SerializeField] GameObject _xTienda;
    
    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        Screen.autorotateToPortrait = false;
        Screen.autorotateToLandscapeLeft = false;
        Screen.autorotateToLandscapeRight = false;
        Screen.autorotateToPortraitUpsideDown = false;

        QualitySettings.vSyncCount = 1;

        if (RemoteConfigManager.Instance._tiendaState == true)
        {
            _xTienda.SetActive(false);
        }
        else
        {
            _xTienda.SetActive(true);
        }
    }

    
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        if(titulocambiado == false)
        {
            _titulo.text = RemoteConfigManager.Instance._titulo;
            titulocambiado = true;
        }

    }
}

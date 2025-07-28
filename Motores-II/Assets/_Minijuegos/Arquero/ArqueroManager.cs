using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ArqueroManager : MonoBehaviour
{
    [SerializeField] Sprite _default, fall, snow, selected;
    [SerializeField] Image fondo;
    [SerializeField] AudioSource _audioSourceHit;
    [SerializeField] AudioClip _audioClipHit;

    public static ArqueroManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        Screen.autorotateToPortrait = false;
        Screen.autorotateToLandscapeLeft = false;
        Screen.autorotateToLandscapeRight = false;
        Screen.autorotateToPortraitUpsideDown = false;
        QualitySettings.vSyncCount = 1;

        if (PlayerPrefs.GetInt("SelectedFondo") == 1)
        {
            selected = fall;
        }
        else if (PlayerPrefs.GetInt("SelectedFondo") == 2)
        {
            selected = snow;
        }
        else if (PlayerPrefs.GetInt("SelectedFondo") == 0)
        {
            selected = _default;
        }

        fondo.sprite = selected;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            //SceneManager.LoadScene("Menu");
            SceneLoaderManager.instance.SceneToLoad = 2;
        }
    }

    public void Hit()
    {
        _audioSourceHit.clip = _audioClipHit;
        _audioSourceHit.Play();
    }

}

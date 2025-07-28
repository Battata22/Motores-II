using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using BulletHell;
using TMPro;

public class PauseScreen : BaseScreen
{
    [SerializeField] BaseScreen _optionScreen;


    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _clickClip;

    [SerializeField] Canvas _confirmCanvas;
    

    public override void Deactivate()
    {
        Time.timeScale = 1.0f;
        base.Deactivate();
    }

    public void Resume()
    {
        BulletHell.ScreenManager.Instance.DeactivateScreen();
        PlaySound(_clickClip);
    }

    public void Restart()
    {
        PlaySound(_clickClip);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1.0f;
    }

    public void BackToMenu()
    {
        PlaySound(_clickClip);

        BulletHell.ScreenManager.Instance.DeactivateScreen();

        _confirmCanvas.enabled = true;
        //SceneManager.LoadScene(0);
        //SceneLoaderManager.instance.SceneToLoad = 2;
    }

    public void CallCheckpoint()
    {
        PlaySound(_clickClip);


        EventManager.Trigger("MementoLoad");
        BulletHell.ScreenManager.Instance.DeactivateScreen();
    }

    public void OpenOptions()
    {
        //Deactivate();
        PlaySound(_clickClip);


        _optionScreen.Activate();
    }

    void PlaySound(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using BulletHell;

public class PauseScreen : BaseScreen
{
    [SerializeField] BaseScreen _optionScreen;


    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _clickClip;

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

        SceneManager.LoadScene(0);
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

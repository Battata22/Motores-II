using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionScreen : BaseScreen
{
    [SerializeField] bool _stickMode;
    [SerializeField] Canvas _volumeScreen;

    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _clickClip;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
            Deactivate();
    }

    public override void Activate()
    {
        base.Activate();
        _stickMode = PlayerPrefs.GetInt("StickMode") == 1 ? true : false;
    }

    public void SwitchStickMode()
    {
        _stickMode = !_stickMode;

        PlayerPrefs.SetInt("StickMode", _stickMode ? 1 : 0);
        PlayerPrefs.Save();

        EventManager.Trigger("ChangeToStick", _stickMode);

        PlaySound(_clickClip);

    }

    public void OpenVolumeScreen()
    {
        
        _volumeScreen.enabled = true;

        PlaySound(_clickClip);
    }

    void PlaySound(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable] public class PlayerView
{
    SpriteRenderer _shieldSprite;
    AudioSource _audioSource;
    AudioClip _shieldUpClip;
    AudioClip _shieldBreakClip;
    AudioClip _powerUpClip;
    AudioClip _healthUpClip;
    AudioClip _playerHurtClip;
    AudioClip _playerShootClip;


    public PlayerView()
    {
        EventManager.Subscribe("OnShieldDestroy", DestroyShield);
    }

    public PlayerView SetAudioData
        (AudioSource audioSource,
         AudioClip shieldUpClip,
         AudioClip shieldBreakClip,
         AudioClip powerUpClip,
         AudioClip healthUpClip,
         AudioClip playerHurtClip,
         AudioClip playerShootClip)
    {
        _audioSource = audioSource;
        _shieldUpClip = shieldUpClip;
        _shieldBreakClip = shieldBreakClip;
        _powerUpClip = powerUpClip;
        _healthUpClip = healthUpClip;
        _playerHurtClip = playerHurtClip;
        _playerShootClip = playerShootClip;

        return this;
    }

    public void FakeUpdate()
    {

    }

    public void FakeOnDestroy()
    {
        EventManager.Unsubscribe("OnShieldDestroy", DestroyShield);

    }

    public void Shoot()
    {
        if (_audioSource.isPlaying) return;
        PlayAudio(_playerShootClip);
    }

    public void GetDamage()
    {
        PlayAudio(_playerHurtClip);
    }

    public void GetHeal()
    {
        PlayAudio(_healthUpClip);
    }

    public void PowerUp()
    {
        PlayAudio(_powerUpClip);
    }

    public void SetShild(SpriteRenderer sprite, bool shilded)
    {
        _shieldSprite = sprite;

        if(shilded)
            ShieldUp();
        else
            DestroyShield();
    }

    void ShieldUp()
    {
        if(_shieldSprite != null) 
        _shieldSprite.enabled = true;
        PlayAudio(_shieldUpClip);
    }

    void DestroyShield(params object[] noUse)
    {
        if(_shieldSprite != null)
        {
        _shieldSprite.enabled = false;
        PlayAudio(_shieldBreakClip);

        }
    }
    

    void PlayAudio(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip);
    }
}

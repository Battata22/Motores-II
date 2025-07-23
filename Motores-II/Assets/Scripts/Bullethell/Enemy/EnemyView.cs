using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyView
{
    [SerializeField] ParticleSystem _deathParticles;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _shootClip;


    public EnemyView()
    {

    }


    public EnemyView SetDeath(BaseEnemy myBase, ParticleSystem particle)
    {
        _deathParticles = particle;
        myBase.OnDeath += Death;
        return this;
    }
    
    public EnemyView SetAudioData(AudioSource source, AudioClip shootClip)
    {
        _audioSource = source;
        _shootClip = shootClip;

        return this;
    }

    public void Shoot()
    {
        PlaySound(_shootClip);
    }
    void PlaySound(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip);
    }

    public void Death()
    {
        _deathParticles.Play();
    }
}

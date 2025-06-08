using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEscalada : MonoBehaviour
{
    //a
    [SerializeField] AudioSource _source;
    [SerializeField] AudioClip[] _moveClip;
    //[SerializeField] AudioClip _hitClip;
    public bool canSound;

    public void SetPos(float x)
    {
        transform.position = new Vector3(x, transform.position.y, transform.position.z);

        PlaySound(_moveClip[Random.Range(0, _moveClip.Length)]);
    }

    void PlaySound(AudioClip clip)
    {
        if (!canSound) return;
        if(_source == null || clip == null) return;

        _source.PlayOneShot(clip);
    }

    //public void PlayHitSound() => PlaySound(_hitClip);

}

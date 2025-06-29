using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class VolumeManager : MonoBehaviour
{
    [SerializeField] AudioMixer _audioMixer;
    [SerializeField] AudioClip _setVolumeSound, _clickedButton;
    [SerializeField] Slider _masterSlider;
    [SerializeField] TextMeshProUGUI _textMaster;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioMixerGroup groupMaster;
    float waitSonido, menuTimer, wait1;
    bool sono = true;

    void OnEnable()
    {
        VolCargarJSON();
    }

    private void Start()
    {
        VolCargarJSON();
    }


    void Update()
    {
        waitSonido += Time.deltaTime;
        menuTimer += Time.deltaTime;

        if (waitSonido >= 0.1f && menuTimer >= 0.2f && sono == false)
        {
            sono = true;
            PlaySound();
        }

        if (menuTimer <= 0.3)
        {
            _audioSource.volume = 0;
        }
        else
        {
            _audioSource.volume = 1;
        }
    }

    public void PlaySoundClickButton()
    {
        _audioSource.clip = _clickedButton;
        _audioSource.Play();
        //_audioSource.clip = null;
    }

    public void PlaySound()
    {
        _audioSource.Play();
    }

    public void MasterOutput()
    {
        _audioSource.outputAudioMixerGroup = groupMaster;
        _audioSource.clip = _setVolumeSound;
    }


    public void SetVolume()
    {
        float volumeMaster = _masterSlider.value;

        _textMaster.text = ((volumeMaster * 100).ToString("0") + "%");
        if (_audioMixer.SetFloat("Master", Mathf.Log10(volumeMaster) * 20) == false)
        {
            print("nmo");
        }
        _audioMixer.SetFloat("Master", Mathf.Log10(volumeMaster) * 20);

        waitSonido = 0;
        sono = false;

        VolGuardarJSON();
    }

    public void VolGuardarJSON()
    {
        VolumeData data = new VolumeData();
        data.Master = _masterSlider.value;

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(Application.dataPath + "/VolumenDataFile.json", json);
    }

    public void VolCargarJSON()
    {
        string json = File.ReadAllText(Application.dataPath + "/VolumenDataFile.json");
        VolumeData data = JsonUtility.FromJson<VolumeData>(json);

        _masterSlider.value = data.Master;
    }

    //public void MasterOutput()
    //{
    //    StartCoroutine(MasterOutputCoroutine());
    //}


    //public void SetVolume()
    //{
    //    StartCoroutine(SetVolumeCoroutine());
    //}

    //public void VolGuardarJSON()
    //{
    //    StartCoroutine(VolGuardarJSONCoroutine());
    //}

    //public void VolCargarJSON()
    //{
    //    StartCoroutine(VolCargarJSONCoroutine());
    //}

}

[System.Serializable]
public class VolumeData
{
    public float Master;
}

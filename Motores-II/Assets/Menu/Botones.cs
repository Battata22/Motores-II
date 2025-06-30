using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Botones : MonoBehaviour
{
    [SerializeField] StaminaSystem stamina;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip noStamina;
    [SerializeField] TextMeshProUGUI textStamina;
    [SerializeField] int rewardStamina;

    private void Start()
    {
        stamina = StaminaSystem.Instance;
        AdsManager.Instance.rewardedAds.OnRewardAddComplete += Rewarded;
    }

    private void Update()
    {
        textStamina.text = ("Stamina: " + stamina.CurrentStamina + "/" + stamina.maxStamina);
    }

    public void CargarArquero()
    {
        if (stamina.CurrentStamina >= stamina.gameStaminaCost)
        {
            SceneManager.LoadScene("Arquero");
        }
        else
        {
            audioSource.clip = noStamina;
            audioSource.Play();
        }
    }

    public void CargarCarlos()
    {
        if (stamina.CurrentStamina >= stamina.gameStaminaCost)
        {
            SceneManager.LoadScene("CarlosDice");
        }
        else
        {
            audioSource.clip = noStamina;
            audioSource.Play();
        }
    }

    public void CargarEscalar()
    {
        if (stamina.CurrentStamina >= stamina.gameStaminaCost)
        {
            SceneManager.LoadScene("Escalada");
        }
        else
        {
            audioSource.clip = noStamina;
            audioSource.Play();
        }
    }

    public void CargarSaltarin()
    {
        if (stamina.CurrentStamina >= stamina.gameStaminaCost)
        {
            SceneManager.LoadScene("Saltarin");
        }
        else
        {
            audioSource.clip = noStamina;
            audioSource.Play();
        }
    }

    public void RewardAd()
    {
        AdsManager.Instance.adButtonScript.ExecuteButton();
    }

    public void Rewarded()
    {
        StaminaSystem.Instance.AdStamina(rewardStamina);
    }

    private void OnDestroy()
    {
        AdsManager.Instance.rewardedAds.OnRewardAddComplete -= Rewarded;
    }
}

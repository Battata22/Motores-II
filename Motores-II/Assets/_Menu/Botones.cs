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
            //SceneManager.LoadScene("Arquero");
            SceneLoaderManager.instance.SceneToLoad = 4;
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
            //SceneManager.LoadScene("CarlosDice");
            SceneLoaderManager.instance.SceneToLoad = 6;
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
            //SceneManager.LoadScene("Escalada");
            SceneLoaderManager.instance.SceneToLoad = 5;
        }
        else
        {
            audioSource.clip = noStamina;
            audioSource.Play();
        }
    }
    
    public void CargarBulletHell()
    {
        if (stamina.CurrentStamina >= stamina.gameStaminaCost)
        {
            //SceneManager.LoadScene("BulletHell");
            SceneLoaderManager.instance.SceneToLoad = 7;
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
            //SceneManager.LoadScene("Saltarin");

            //Cualquiera de las dos funciona, una es sabiendo el numero de la escena (se caga si cambia el orden) y el otro es con el nombre (Mentira, el del nombre no funciona, da -1)
            //SceneLoaderManager.instance.SceneToLoad = SceneManager.GetSceneByName("Saltarin").buildIndex;
            SceneLoaderManager.instance.SceneToLoad = 3;
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

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : BaseScreen
{
    [SerializeField] TMP_Text _text;
    [SerializeField] GameObject _retryButton;

    [SerializeField] AudioClip _errorSound;
    [SerializeField] AudioClip _winClip;
    [SerializeField] AudioClip _loseClip;
    //[SerializeField] AudioClip _clickSound;
    [SerializeField] AudioSource _audioSource;

    private void Awake()
    {
        EventManager.Subscribe("PlayerDeath", Lose);
        EventManager.Subscribe("BossDeath", Win);
        gameObject.SetActive(false);

    }

    private void Start()
    {
        AdsManager.Instance.rewardedAds.OnRewardAddComplete += CallCheckpoint;
        
    }


    private void OnDestroy()
    {
        AdsManager.Instance.rewardedAds.OnRewardAddComplete -= CallCheckpoint;
        EventManager.Unsubscribe("PlayerDeath", Lose);
        EventManager.Unsubscribe("BossDeath", Win);
    }

    void Win(params object[] noUse)
    {
        _text.text = "YOU WIN";
        _text.color = Color.green;
        _retryButton.SetActive(false);

        _audioSource.PlayOneShot(_winClip);

        BulletHell.ScreenManager.Instance.ActivateScreen(this);
    }

    void Lose(params object[] noUse)
    {
        _text.text = "YOU LOSE";
        _text.color = Color.red;
        _retryButton.SetActive(true);

        _audioSource.PlayOneShot(_loseClip);

        BulletHell.ScreenManager.Instance.ActivateScreen(this);
    }

    public override void Activate()
    {
        base.Activate();
        Time.timeScale = 0f;
    }

    public override void Deactivate()
    {
        Time.timeScale = 1f;
        base.Deactivate();
    }

    public void Restart()
    {
        if (!StaminaSystem.Instance.HasEnoughStamina(StaminaSystem.Instance.gameStaminaCost))
        {
            _audioSource.PlayOneShot(_errorSound);

            Debug.Log($"ESTAMINA INSUFICIENTE {StaminaSystem.Instance.CurrentStamina} \n" +
                $"Estamina Necesaria {StaminaSystem.Instance.gameStaminaCost}");
            return;
        }

        BulletHell.ScreenManager.Instance.DeactivateScreen();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackToMenu()
    {
        BulletHell.ScreenManager.Instance.DeactivateScreen();

        SceneManager.LoadScene(1);
    }

    void CallCheckpoint()
    {
        EventManager.Trigger("MementoLoad");
        BulletHell.ScreenManager.Instance.DeactivateScreen();

        //_audioSource.PlayOneShot(_clickSound);

        //Deactivate();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimerInterstitial : MonoBehaviour
{
    public float timeToAd;
    private float timer;

    private void Start()
    {
        timer = Time.time + timeToAd;
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            if (Time.time > timer)
            {
                AdsManager.Instance.ShowInterstitialAd();
                timer = Time.time + timeToAd;
            }
        }
    }
}

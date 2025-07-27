using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PuntajeManager : MonoBehaviour
{
    public static PuntajeManager instance;

    [SerializeField] TextMeshProUGUI textPuntaje;
    [SerializeField] float totalPoints;

    [SerializeField] GameObject victoriaPNG;
    [SerializeField] Toggle checkVictoriaBox;
    [SerializeField] float puntosNecesariosVictoria;
    [SerializeField] float _tiempoDeVictoria;
    float waitvictoria;
    [SerializeField] bool checkVictoria = true;
    [SerializeField] SpawnerDianas spawnerScript;

    [SerializeField] AudioSource _audioSourceVictory;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        textPuntaje.text = ("Puntaje: 0");
        totalPoints = 0;

        spawnerScript = GetComponent<SpawnerDianas>();
    }


    void Update()
    {
        checkVictoria = checkVictoriaBox.isOn;

        if (totalPoints >= puntosNecesariosVictoria && checkVictoria == true)
        {
            Victory();
        }
    }

    public void AddPoints(float points)
    {
        totalPoints += points;

        SetScore(totalPoints);

        print("sume " + points + " puntos");
    }

    void SetScore(float points)
    {
        textPuntaje.text = ("Puntaje: " + (int)points);
    }

    void Victory()
    {
        Sonido();
        spawnerScript.enabled = false;
        checkVictoriaBox.gameObject.SetActive(false);
        victoriaPNG.SetActive(true);
        waitvictoria += Time.deltaTime;
        StaminaSystem.Instance.UseStamina(StaminaSystem.Instance.gameStaminaCost);
        if (waitvictoria >= _tiempoDeVictoria)
        {
            PointsManager.Instance.AddPoints((totalPoints / 1000));
            //SceneManager.LoadScene("Menu");
            SceneLoaderManager.instance.SceneToLoad = 2;
        }
    }

    public void MeVoyPajarito()
    {
        StaminaSystem.Instance.UseStamina(StaminaSystem.Instance.gameStaminaCost);
        PointsManager.Instance.AddPoints((totalPoints / 1000));
    }

    bool _primerSonido = true;
    public void Sonido()
    {
        if (_primerSonido)
        {
            _audioSourceVictory.Play();
            _primerSonido = false;
        }
    }

}

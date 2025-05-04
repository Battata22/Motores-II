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
            spawnerScript.enabled = false;
            checkVictoriaBox.gameObject.SetActive(false);
            victoriaPNG.SetActive(true);
            waitvictoria += Time.deltaTime;
            if (waitvictoria >= _tiempoDeVictoria)
            {
                SceneManager.LoadScene("Menu");
            }
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

}

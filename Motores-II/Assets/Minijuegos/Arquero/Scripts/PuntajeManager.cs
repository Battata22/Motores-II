using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PuntajeManager : MonoBehaviour
{
    public static PuntajeManager instance;

    [SerializeField] TextMeshProUGUI textPuntaje;
    [SerializeField] float totalPoints;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        textPuntaje.text = ("Puntaje: 0");
        totalPoints = 0;
    }


    void Update()
    {
        
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class StaminaSystem : MonoBehaviour
{
    public int maxStamina = 10;
    [SerializeField] float timeToRecharge = 10f;
    [SerializeField] public int gameStaminaCost;
    [SerializeField] int currentStamina;
    public int CurrentStamina {  get { return currentStamina; } }


    DateTime nextStaminaTime;
    DateTime lastStaminaTime;

    bool recharging;


    //notification
    [SerializeField] string _titleNotif = "Energia al maximo";
    [SerializeField] string _textNotif = "Estamina llena, no desperdicies energia";
    [SerializeField] IconSelecter _smallIcon = IconSelecter.icon_reminder;
    [SerializeField] IconSelecter _largeIcon = IconSelecter.icon_reminderbig;
    TimeSpan timer;
    int id;

    //[SerializeField] TextMeshProUGUI staminaText = null;
    //[SerializeField] TextMeshProUGUI timerText = null;

    #region Singleton
    public static StaminaSystem Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    } 
    #endregion

    private void Start()
    {
        if (!PlayerPrefs.HasKey("currentStamina"))
        {
            PlayerPrefs.SetInt("currentStamina", maxStamina);
        }

        //Esto se hace para que se seteen de todas maneras los valores default en caso que sea primera vez que se cargan datos
        //DateTime StringToDateTime(string date)
        Load();
        StartCoroutine(RechargeStamina());
    }

    //Uso de lamda y goes to
    public bool HasEnoughStamina(int stamina) => currentStamina - stamina >= 0;

    IEnumerator RechargeStamina()
    {
        UpdateTimer();
        recharging = true;

        //Quizas queres recargar varias staminas a la vez
        while (currentStamina < maxStamina)
        {
            //Checkeos de tiempos
            DateTime currentTime = DateTime.Now;
            DateTime nextTime = nextStaminaTime;

            //Booleano temporal para checkear si se recargo stamina.
            bool staminaAdd = false;

            while (currentTime > nextTime)
            {
                //No quiero superar mi maximo de stamina
                if (currentStamina >= maxStamina) break;

                currentStamina += 1;
                staminaAdd = true;
                UpdateStamina();

                //Predecir cual va a ser el momento en el que se va a cargar mi proxima stamina.

                DateTime timeToAdd = nextTime;

                //Mas que nada para checkear si cerre la aplicacion o no
                if (lastStaminaTime > nextTime)
                    timeToAdd = lastStaminaTime;

                //Creo una funcion para agregar el tiempo a nextTime
                nextTime = AddDuration(timeToAdd, timeToRecharge);
            }

            //Si se recargo stamina...
            if (staminaAdd)
            {
                nextStaminaTime = nextTime;
                lastStaminaTime = DateTime.Now;
            }

            //Updateos en UI y guardar
            UpdateTimer();
            UpdateStamina();
            Save();

            yield return new WaitForEndOfFrame();
        }

        //Notificacion
        NotificationManager.Instance.CancelNotification(id);

        recharging = false;
    }

    DateTime AddDuration(DateTime date, float duration)
    {
        //En nuestro caso, como es un testeo, lo hacemos con un AddSeconds
        return date.AddSeconds(duration);
    }

    public void UseStamina(int staminaToUse)
    {
        if(currentStamina - staminaToUse >= 0)
        {
            //Resto y pregunto la cantiad de stamina usada en x nivel.
            currentStamina -= staminaToUse;
            UpdateStamina();

            NotificationManager.Instance.CancelNotification(id);
            DisplayNotif();

            //Si no estoy recargando stamina
            if (!recharging)
            {

                //Seteo el next stamina time y comienzo mi recarga.
                nextStaminaTime = AddDuration(DateTime.Now, timeToRecharge);
                StartCoroutine(RechargeStamina());
            }
        }
        else
        {
            Debug.Log("No tengo Stamina!");
        }
    }

    void UpdateTimer()
    {
        if(currentStamina >= maxStamina)
        {
            //timerText.text = "Full Stamina!";

            return;
        }

        //Estructura que nos da un intervalo de tiempo
        TimeSpan timer = nextStaminaTime - DateTime.Now;

        //Formato "00" para representar el horario con 2 numeros.
        //timerText.text = timer.Minutes.ToString("00") + ":" + timer.Seconds.ToString("00");
    }

    void UpdateStamina()
    {
        //staminaText.text = currentStamina.ToString() + " / " + maxStamina.ToString();
        EventManager.Trigger("OnStaminaUpdate");
    }

    public void AdStamina(int amount)
    {
        currentStamina += amount;

        NotificationManager.Instance.CancelNotification(id);
        DisplayNotif();

        UpdateStamina();
    }

    public void ResetStamina()
    {
        currentStamina = maxStamina;
    }


    void Save()
    {
        PlayerPrefs.SetInt("currentStamina", currentStamina);
        PlayerPrefs.SetString("nextStaminaTime", nextStaminaTime.ToString());
        PlayerPrefs.SetString("lastStaminaTime", lastStaminaTime.ToString());
    }

    void Load()
    {
        currentStamina = PlayerPrefs.GetInt("currentStamina");
        //Mas lindo
        nextStaminaTime = StringToDateTime(PlayerPrefs.GetString("nextStaminaTime"));
        lastStaminaTime = StringToDateTime(PlayerPrefs.GetString("lastStaminaTime"));

        //nextStaminaTime = DateTime.Parse(PlayerPrefs.GetString("nextStaminaTime"));
        //lastStaminaTime = DateTime.Parse(PlayerPrefs.GetString("lastStaminaTime"));
    }

    //Mas lindo
    DateTime StringToDateTime(string date)
    {
        if (string.IsNullOrEmpty(date))
            return DateTime.Now; //Este mismo momento > Today es hoy a las 000 > UtcNow, Tiempo universal coordinado (Argentina UTC-3)
        else
            return DateTime.Parse(date);
    }

    void DisplayNotif()
    {
        id = NotificationManager.Instance.DisplayNotification(_titleNotif, _textNotif, _smallIcon, _largeIcon,
            AddDuration(DateTime.Now, ((maxStamina - (currentStamina) + 1) * timeToRecharge) + 1 + (float)timer.TotalSeconds));
    }

    void OnApplicationQuit()
    {
        Save();
    }

    void OnApplicationPause(bool isPaused)
    {
        if (isPaused) Save();
    }

    private void OnDisable()
    {
        Save();
    }
}

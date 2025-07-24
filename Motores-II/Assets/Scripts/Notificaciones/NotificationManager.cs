using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Notifications.Android;
using System;


public class NotificationManager : MonoBehaviour
{
    public static NotificationManager Instance { get; private set; }

    AndroidNotificationChannel ReminderChannel;
    AndroidNotificationChannel EnergyChannel;

    private void Awake()
    {
        if(Instance != this && Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        //Opcional a usar
        AndroidNotificationCenter.CancelAllDisplayedNotifications();
        AndroidNotificationCenter.CancelAllScheduledNotifications();
        if (PlayerPrefs.HasKey("Display_ComeBack")) CancelNotification(PlayerPrefs.GetInt("Display_ComeBack"));

        ReminderChannel = new AndroidNotificationChannel()
        {
            Id = "reminder_notif_ch",
            Name = "Reminder Notification",
            Description = "Reminder to login",
            Importance = Importance.High,
            CanBypassDnd = true
        };
        EnergyChannel = new AndroidNotificationChannel()
        {
            Id = "energy_notif_ch",
            Name = "Energy Notification",
            Description = "Energy charged",
            Importance = Importance.High,
            CanBypassDnd = true
        };

        AndroidNotificationCenter.RegisterNotificationChannel(ReminderChannel);
        AndroidNotificationCenter.RegisterNotificationChannel(EnergyChannel);

        PlayerPrefs.SetInt("Display_ComeBack", DisplayNotification("OLVIDONAAA", "¿Hace cuanto que no jugamos?",
            IconSelecter.icon_reminder, IconSelecter.icon_reminderbig, DateTime.Now.AddMinutes(1), NotiChannel.reminder));
    }

    public int DisplayNotification(string title, string text, IconSelecter iconSmall, IconSelecter iconLarge, DateTime fireTime, NotiChannel channel)
    {
        var notification = new AndroidNotification()
        {
            Title = title,
            Text = text,
            SmallIcon = iconSmall.ToString(),
            LargeIcon = iconLarge.ToString(),
            FireTime = fireTime,
        };
        //notification.Title = title;
        //notification.Text = text;
        //notification.SmallIcon = iconSmall.ToString();
        //notification.LargeIcon = iconLarge.ToString();
        //notification.FireTime = fireTime;

        Debug.Log($"Notificacion Seteada: \n triger time{fireTime} \n {title} \n {text}\n {iconSmall}\n {iconLarge}");//a
        Debug.Log(notification);

        var finalChannel = ReminderChannel;
        switch (channel)
        {
            case NotiChannel.reminder:

                break;
            case NotiChannel.energy:

                finalChannel = EnergyChannel;
                break;
            default:
                break;
        }

        var id = AndroidNotificationCenter.SendNotification(notification, finalChannel.Id);

        Debug.Log($"Notification id: {id}");

        return id;
    }

    public void CancelNotification(int id)
    {
        AndroidNotificationCenter.CancelScheduledNotification(id);
        Debug.Log($"Notificacion cancelada {id}");
    }
}

public enum NotiChannel
{
    reminder,
    energy
}

public enum IconSelecter
{
    icon_reminder,
    icon_reminderbig,
    icon_energy,
    icon_energybig
}

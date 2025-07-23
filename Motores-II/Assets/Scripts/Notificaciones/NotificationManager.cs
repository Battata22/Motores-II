using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Notifications.Android;
using System;


public class NotificationManager : MonoBehaviour
{
    public static NotificationManager Instance { get; private set; }

    AndroidNotificationChannel notifChannel;

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
        //AndroidNotificationCenter.CancelAllDisplayedNotifications();
        //AndroidNotificationCenter.CancelAllScheduledNotifications();
        if (PlayerPrefs.HasKey("Display_ComeBack")) CancelNotification(PlayerPrefs.GetInt("Display_ComeBack"));

        notifChannel = new AndroidNotificationChannel()
        {
            Id = "reminder_notif_ch",
            Name = "Reminder Notification",
            Description = "Reminder to login",
            Importance = Importance.High
        };

        AndroidNotificationCenter.RegisterNotificationChannel(notifChannel);

        PlayerPrefs.SetInt("Display_ComeBack", DisplayNotification("ALVIDONAAA", "¿Hace cuanto que no jugamos?",
            IconSelecter.icon_reminder, IconSelecter.icon_reminderbig, DateTime.Now.AddMinutes(1)));
    }

    public int DisplayNotification(string title, string text, IconSelecter iconSmall, IconSelecter iconLarge, DateTime fireTime)
    {
        var notification = new AndroidNotification();
        notification.Title = title;
        notification.Text = text;
        notification.SmallIcon = iconSmall.ToString();
        notification.LargeIcon = iconLarge.ToString();
        notification.FireTime = fireTime;

        return AndroidNotificationCenter.SendNotification(notification, notifChannel.Id);
    }

    public void CancelNotification(int id)
    {
        AndroidNotificationCenter.CancelScheduledNotification(id);
    }
}

public enum IconSelecter
{
    icon_reminder,
    icon_reminderbig
}

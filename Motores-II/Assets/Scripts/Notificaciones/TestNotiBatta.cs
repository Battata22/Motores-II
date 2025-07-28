using System.Collections;
using System.Collections.Generic;
using Unity.Notifications.Android;
using UnityEngine;

public class TestNotiBatta : MonoBehaviour
{

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        var channel = new AndroidNotificationChannel("my_channel_id", "My Channel Name", "My channel description", Importance.Default);
        AndroidNotificationCenter.RegisterNotificationChannel(channel);

        CrearNoti();
    }

    void CrearNoti()
    {
        var notification = new AndroidNotification();
        notification.Title = "Game Alert!";
        notification.Text = "Your energy is full!";
        notification.FireTime = System.DateTime.Now.AddSeconds(60); // Fire in 10 seconds

        AndroidNotificationCenter.SendNotification(notification, "my_channel_id");
    }

}

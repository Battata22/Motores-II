using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_ANDROID
using Unity.Notifications.Android;
#endif

public class MobileNotificationsViaLogin : MonoBehaviour
{
    public string tittleString;
    public string textString;
    public float hoursToDisplayThis;
    public int id;

    public void MobileNotificationViaLogin()
    {
        AndroidNotificationCenter.CancelDisplayedNotification(id);

        var channel = new AndroidNotificationChannel()
        {
            Id = "channel_id",
            Name = "Notifications Channel",
            Importance = Importance.Default,
            Description = "Reminder notifications",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channel);


        var notification = new AndroidNotification();

        notification.Title = tittleString;
        notification.Text = textString;

        notification.FireTime = System.DateTime.Now.AddHours(hoursToDisplayThis);

        id = AndroidNotificationCenter.SendNotification(notification, "channel_id");

        if (AndroidNotificationCenter.CheckScheduledNotificationStatus(id) == NotificationStatus.Scheduled)
        {
            AndroidNotificationCenter.CancelNotification(id);
            AndroidNotificationCenter.SendNotification(notification, "channel_id");
        }
    }
}

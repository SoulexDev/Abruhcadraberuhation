using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationAccessor : MonoBehaviour
{
    public void SendNotification(string message)
    {
        Player.Instance.notificationManager.QueueNotification(message);
    }
}
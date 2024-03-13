using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NotificationManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI notificationText;
    private string ogMessage;

    private int multiplier = 1;

    private void Awake()
    {
        notificationText.alpha = 0;
    }
    public void QueueNotification(string message)
    {
        if (ogMessage == message)
        {
            StopAllCoroutines();
            multiplier++;

            StartCoroutine(DisplayNotification(message + $" x{multiplier}"));
            return;
        }
        else
        {
            multiplier = 1;
            ogMessage = message;
            StartCoroutine(DisplayNotification(message));
        }
    }
    IEnumerator DisplayNotification(string message)
    {
        notificationText.text = message;
        notificationText.alpha = 1;

        yield return new WaitForSeconds(2);

        while (notificationText.alpha > 0)
        {
            notificationText.alpha = Mathf.MoveTowards(notificationText.alpha, 0, Time.deltaTime);
            yield return null;
        }
        notificationText.alpha = 0;
        ogMessage = "";
    }
}
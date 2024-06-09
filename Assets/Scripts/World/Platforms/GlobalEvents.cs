using System;
using UnityEngine;

public class GlobalEvents : MonoBehaviour
{
    public static event Action<string> OnEvent;

    public static void FireEvent(string eventName)
    {
        OnEvent?.Invoke(eventName);
    }
}

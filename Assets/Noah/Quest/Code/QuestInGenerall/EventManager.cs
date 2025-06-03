using System;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    // Singleton-Instanz
    public static EventManager Instance { get; private set; }

    private Dictionary<Type, Action<object>> eventTable = new();

    private void Awake()
    {
        // Singleton-Sicherung
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional, falls du es szenenübergreifend brauchst
        }
    }

    // Listener hinzufügen
    public void AddListener<T>(Action<T> listener)
    {
        Type type = typeof(T);

        if (eventTable.TryGetValue(type, out var existing))
        {
            eventTable[type] = existing + (Action<object>)(e => listener((T)e));
        }
        else
        {
            eventTable[type] = (Action<object>)(e => listener((T)e));
        }
    }

    // Event auslösen
    public void Raise<T>(T eventInfo)
    {
        Type type = typeof(T);

        if (eventTable.TryGetValue(type, out var action))
        {
            action?.Invoke(eventInfo);
        }
    }

    // Listener entfernen (optional für Cleanup)
    public void RemoveListener<T>(Action<T> listener)
    {
        Type type = typeof(T);

        if (eventTable.TryGetValue(type, out var existing))
        {
            existing -= (Action<object>)(e => listener((T)e));
            if (existing == null) eventTable.Remove(type);
            else eventTable[type] = existing;
        }
    }
}

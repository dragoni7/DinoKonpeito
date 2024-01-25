using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public sealed class EventBus
{
    private static EventBus _instance = null;
    private static readonly object _lock = new();

    private Dictionary<Type, SortedList<EventPriority, List<Delegate>>> _registeredEventHandlers;

    public static EventBus Instance
    {
        get
        {
            lock (_lock)
            {
                _instance ??= new EventBus();

                return _instance;
            }
        }
    }

    private EventBus()
    {
        _registeredEventHandlers = new();
    }

    public void Raise<T>(T e) where T : IEvent
    {
        Type eventType = typeof(T);

        //GD.Print("trying to raise event " + eventType.ToString());

        if (_registeredEventHandlers.TryGetValue(eventType, out SortedList<EventPriority, List<Delegate>> listeners))
        {
            foreach (var entry in listeners)
            {
                //GD.Print(entry.Key);

                foreach(Delegate listener in entry.Value)
                {
                    if (listener is not Action<T>)
                        continue;

                    //GD.Print(eventType.ToString() + " raised");
                    ((Action<T>)listener).Invoke(e);
                }
            }
        }
    }

    public void Subscribe<T>(Action<T> listener) where T : IEvent
    {
        Subscribe<T>(listener, EventPriority.Normal);
    }


    public void Subscribe<T>(Action<T> listener, EventPriority priority) where T : IEvent
    {
        Type eventType = typeof(T);
        //GD.Print("subscribed to " + eventType.ToString());

        if (_registeredEventHandlers.ContainsKey(eventType))
        {
            if (_registeredEventHandlers[eventType].ContainsKey(priority))
            {
                _registeredEventHandlers[eventType][priority].Add(listener);
            }
            else
            {
                _registeredEventHandlers[eventType].Add(priority, new() { listener });
            }
        }
        else
        {
            SortedList<EventPriority, List<Delegate>> newList = new()
            {
                { priority, new() { listener } }
            };

            _registeredEventHandlers.Add(eventType, newList);
        }
    }

    public void Unsubscribe<T>(Action<T> listener) where T : IEvent
    {
        Type eventType = typeof(T);

        if (_registeredEventHandlers.ContainsKey(eventType))
        {
            foreach (var item in _registeredEventHandlers[eventType].Where(kvp => kvp.Value.Contains(listener)))
            {
                _registeredEventHandlers[eventType][item.Key].Remove(listener);
            }
        }
    }

    public void UnsubscribeAll()
    {
        _registeredEventHandlers = new();
    }
}

public enum EventPriority
{
    Low = 2,
    Normal = 1,
    High = 0,
    Highest = -1
}

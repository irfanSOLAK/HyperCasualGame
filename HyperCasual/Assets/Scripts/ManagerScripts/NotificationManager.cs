using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NotificationManager : MonoBehaviour
{
    public enum EVENT_TYPE { Countdown, StartRunning, FinishRunning, PaintWall, };


    private Dictionary<EVENT_TYPE, List<IListener>> _listeners = new Dictionary<EVENT_TYPE, List<IListener>>();

    void OnEnable()
    {
        SceneManager.sceneLoaded += RemoveRedundancies;
    }

    public void AddListener(EVENT_TYPE eventName, IListener lObject)
    {
        if (!_listeners.ContainsKey(eventName))
            _listeners.Add(eventName, new List<IListener>());

        _listeners[eventName].Add(lObject);
    }

    public void RemoveListener(EVENT_TYPE eventName, IListener lObject)
    {
        if (!_listeners.ContainsKey(eventName))
            return;

        for (int i = _listeners[eventName].Count - 1; i >= 0; i--)
        {
            if ((_listeners[eventName][i] as Component).GetInstanceID() == (lObject as Component).GetInstanceID())
                _listeners[eventName].RemoveAt(i);
        }
    }

    public void PostNotification(EVENT_TYPE eventName, float parameter = 0)
    {
        if (!_listeners.ContainsKey(eventName))
        {
            Debug.Log(eventName.ToString() + " does not exist in the NotificationManager dictionary.");
            return;
        }

        foreach (Component Listener in _listeners[eventName])
            Listener.GetComponent<IListener>().OnEventOccured(eventName, parameter);
    }

    public void RemoveRedundancies(Scene scene, LoadSceneMode mode)
    {
        Dictionary<EVENT_TYPE, List<IListener>> TmpListeners = new Dictionary<EVENT_TYPE, List<IListener>>();

        foreach (KeyValuePair<EVENT_TYPE, List<IListener>> Item in _listeners)
        {
            for (int i = Item.Value.Count - 1; i >= 0; i--)
            {
                if (Item.Value[i] == null)
                    Item.Value.RemoveAt(i);
            }

            if (Item.Value.Count > 0)
                TmpListeners.Add(Item.Key, Item.Value);
        }

        _listeners = TmpListeners;
    }

}
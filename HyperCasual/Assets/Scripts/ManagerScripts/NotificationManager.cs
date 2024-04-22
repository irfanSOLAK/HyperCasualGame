using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NotificationManager : MonoBehaviour
{

    private Dictionary<Game_Events, List<IListener>> _listeners = new Dictionary<Game_Events, List<IListener>>();

    void OnEnable()
    {
        SceneManager.sceneLoaded += RemoveRedundancies;
    }

    public void AddListener(Game_Events eventName, IListener lObject)
    {
        if (!_listeners.ContainsKey(eventName))
            _listeners.Add(eventName, new List<IListener>());

        _listeners[eventName].Add(lObject);
    }

    public void RemoveListener(Game_Events eventName, IListener lObject)
    {
        if (!_listeners.ContainsKey(eventName))
            return;

        for (int i = _listeners[eventName].Count - 1; i >= 0; i--)
        {
            if ((_listeners[eventName][i] as Component).GetInstanceID() == (lObject as Component).GetInstanceID())
                _listeners[eventName].RemoveAt(i);
        }
    }

    public void PostNotification(Game_Events eventName, float parameter = 0)
    {
        if (!_listeners.ContainsKey(eventName))
        {
            Debug.Log(eventName.ToString() + " does not exist in the NotificationManager dictionary.");
            return;
        }

        foreach (IListener Listener in _listeners[eventName])
            Listener.OnEventOccured(eventName, parameter);
    }

    public void RemoveRedundancies(Scene scene, LoadSceneMode mode)
    {
        Dictionary<Game_Events, List<IListener>> TmpListeners = new Dictionary<Game_Events, List<IListener>>();

        foreach (KeyValuePair<Game_Events, List<IListener>> Item in _listeners)
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
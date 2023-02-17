using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(NotificationManager))]
public class GameBehaviour : Singleton<GameBehaviour>
{

    private GameParametersScriptableObject _gameParameters = null;
    public GameParametersScriptableObject GameParameters
    {
        get
        {
            if (_gameParameters == null) _gameParameters = Resources.Load<GameParametersScriptableObject>("GameParameters/GameParameters");
            return _gameParameters;
        }
    }


    private NotificationManager _notifications = null;
    public NotificationManager Notifications
    {
        get
        {
            if (_notifications == null) _notifications = Instance.GetComponent<NotificationManager>();
            return _notifications;
        }
    }


    // OnDisable method in Listener class causes an error when exiting game on editor. This method is to avoid the error.
    private void OnApplicationQuit()
    {
        MonoBehaviour[] scripts = FindObjectsOfType<MonoBehaviour>();
        foreach (MonoBehaviour script in scripts)
            script.enabled = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStarter : MonoBehaviour
{
    [SerializeField] Text _runnningCountdownText;
    [SerializeField] Text _runningGameplayInfo;
    [SerializeField] Text _paintingGameplayInfo;

    int _countdownInSeconds;

    private void Awake()
    {
        SetGameStarterParameters();
    }

    private void SetGameStarterParameters()
    {
        _countdownInSeconds = GameBehaviour.Instance.GameParameters.countdownInSeconds;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Game2")
        {
            StartCoroutine(StartPaintingGame());
        }
        else
        {
            StartCoroutine(StartRunningGame());
        }
    }


    #region Painting Game

    IEnumerator StartPaintingGame()
    {
        PostPaintWall();
        yield return StartCoroutine(GivePaintingGameInfoForSeconds(_countdownInSeconds));
        ClearPaintingGameTexts();
    }

    private void PostPaintWall()
    {
        GameBehaviour.Instance.Notifications.PostNotification(NotificationManager.EVENT_TYPE.PaintWall, 0);
    }

    IEnumerator GivePaintingGameInfoForSeconds(int seconds)
    {
        _paintingGameplayInfo.text = GameBehaviour.Instance.GameParameters.paintingGameplayInfo;
        yield return new WaitForSeconds(seconds);
    }

    void ClearPaintingGameTexts()
    {
        _paintingGameplayInfo.text = "";
    }

    #endregion


    #region Running Game

    IEnumerator StartRunningGame()
    {
        GiveRunningGameInfo();
        PostCountdown();
        yield return StartCoroutine(CountdownCoroutine(_countdownInSeconds));
        ClearRunningGameTexts();
        PostStartRace();
    }

    void GiveRunningGameInfo()
    {
        _runningGameplayInfo.text = GameBehaviour.Instance.GameParameters.runningGameplayInfo;
    }

    private void PostCountdown()
    {
        GameBehaviour.Instance.Notifications.PostNotification(NotificationManager.EVENT_TYPE.Countdown);
    }

    IEnumerator CountdownCoroutine(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            _runnningCountdownText.text = i.ToString();
            yield return new WaitForSeconds(1.0f);
        }

        _runnningCountdownText.text = "Go!";
        yield return new WaitForSeconds(1.0f);
    }

    private void ClearRunningGameTexts()
    {
        _runnningCountdownText.text = " ";
        _runningGameplayInfo.text = " ";
    }

    private void PostStartRace()
    {
        GameBehaviour.Instance.Notifications.PostNotification(NotificationManager.EVENT_TYPE.StartRunning);
    }

    #endregion

}

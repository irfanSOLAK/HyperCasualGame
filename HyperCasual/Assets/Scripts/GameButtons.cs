using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameButtons : MonoBehaviour
{
    [SerializeField] Text _mainMenuButtonsInfoText;

    public void PlayGame1Button()
    {
        SceneManager.LoadScene("Game1");
    }

    public void GiveGame1Info()
    {
        _mainMenuButtonsInfoText.text = GameBehaviour.Instance.GameParameters.game1Info;
    }

    public void PlayGame2Button()
    {
        SceneManager.LoadScene("Game2");
    }

    public void GiveGame2Info()
    {
        _mainMenuButtonsInfoText.text = GameBehaviour.Instance.GameParameters.game2Info;
    }

    public void PlayGame3Button()
    {
        SceneManager.LoadScene("Game3");
    }

    public void GiveGame3Info()
    {
        _mainMenuButtonsInfoText.text = GameBehaviour.Instance.GameParameters.game3Info;
    }

    public void QuitGameButton()
    {
        Application.Quit();
    }

    public void GiveDefaultInfo()
    {
        _mainMenuButtonsInfoText.text = GameBehaviour.Instance.GameParameters.defaultText;
    }

    public void HomeButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
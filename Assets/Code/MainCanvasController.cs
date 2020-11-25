using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MainCanvasController : MonoBehaviour
{
    public GameObject MainMenuRoot;
    public GameObject InGameUIRoot;
    public GameObject PauseMenuRoot;
    public GameObject GameOverMenuRoot;
    public TextMeshProUGUI GameOverYourScoreText;
    public TextMeshProUGUI GameOverHighScoreText;
    public GameObject GameOverNewHighScore;

    public TextMeshProUGUI MainMenuHighScoreText;

    public TextMeshProUGUI PauseMenuYourScoreText;
    public TextMeshProUGUI PauseMenuHighScoreText;

    public TextMeshProUGUI LanesText;
    public Slider LanesSlider;

    void Start()
    {
        GameController.Instance.MainCanvasController = this;
        MainMenuHighScoreText.text = "High score: " + GameController.Instance.CurrentHighScore;
        LanesSlider.minValue = GameController.Instance.LaneAmountMin;
        LanesSlider.maxValue = GameController.Instance.LaneAmountMax;
    }

    public void ToggleMainMenu(bool enable)
    {
        MainMenuHighScoreText.text = "High score: " + GameController.Instance.CurrentHighScore;
        MainMenuRoot.SetActive(enable);
    }

    public void ToggleInGameUI(bool enable)
    {
        InGameUIRoot.SetActive(enable);
    }

    public void TogglePauseMenu(bool enable)
    {
        if (enable)
        {
            PauseMenuYourScoreText.text = "Your score: " + GameController.Instance.CurrentScore;
            PauseMenuHighScoreText.text = "High score: " + GameController.Instance.CurrentHighScore;
        }
        PauseMenuRoot.SetActive(enable);
    }

    public void ToggleGameOverMenu(bool enable, bool newHighScore = false)
    {
        if (enable)
        {
            GameOverYourScoreText.text = "Your score: " + GameController.Instance.CurrentScore;
            GameOverHighScoreText.text = "High score: " + GameController.Instance.CurrentHighScore;
            InGameUIRoot.SetActive(false);
        }
        GameOverMenuRoot.SetActive(enable);
        GameOverNewHighScore.SetActive(newHighScore);
    }


    public void StartGameButton()
    {
        ToggleMainMenu(false);
        ToggleInGameUI(true);
        GameController.Instance.StartGame((int)LanesSlider.value);
    }

    public void PauseGameButton()
    {
        ToggleInGameUI(false);
        TogglePauseMenu(true);
        GameController.Instance.TogglePause(true);
    }

    public void ContinueGameButton()
    {
        ToggleInGameUI(true);
        TogglePauseMenu(false);
        GameController.Instance.TogglePause(false);
    }

    public void RestartGameButton()
    {
        GameController.Instance.DeSpawnEverything();
        GameController.Instance.StartGame((int)LanesSlider.value);
        ToggleGameOverMenu(false);
        TogglePauseMenu(false);
        ToggleInGameUI(true);
    }

    public void MainMenuButton()
    {
        GameController.Instance.DeSpawnEverything();
        GameController.Instance.GameStarted = false;
        GameController.Instance.GameEnded = true;
        GameController.Instance.GameRunning = false;
        ToggleInGameUI(false);
        ToggleGameOverMenu(false);
        TogglePauseMenu(false);
        ToggleMainMenu(true);
    }

    public void UpdateLanesText()
    {
        LanesText.text = "Number of lanes: " + LanesSlider.value;
    }
}

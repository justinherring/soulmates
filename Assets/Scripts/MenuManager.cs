using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private int gameMenuScene = 0;

    [SerializeField]
    private int gameStartScene = 1;

    [SerializeField]
    private int gameOverScene = 2;

    [SerializeField]
    private int victoryScene = 3;
    public void GotoMainMenu()
    {
        SceneManager.LoadScene(gameMenuScene);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(gameStartScene);
    }

    public void GotoGameOver()
    {
        SceneManager.LoadScene(gameOverScene);
    }

    public void GotoVictory()
    {
        SceneManager.LoadScene(victoryScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayGameButton : MonoBehaviour
{
    [SerializeField]
    private int gameStartScene = 1;

    public void StartGame()
    {
        SceneManager.LoadScene(gameStartScene);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    public GameObject gameOverUI;
    public GameObject youWinUI;

    private void Awake()
    {
        instance = this;
    }

    public void GameOver()
    {
        gameOverUI.SetActive(true);
    }
    public void PlayAgain() 
    {         
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void YouWin()
    {
        youWinUI.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    public GameObject playButton;
    public GameObject quitButton;
    public GameObject turtorialButton;
    public GameObject turtorialPanel;
    public void Play()
    {
        SceneManager.LoadScene(1);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Turtorial()
    {
        playButton.SetActive(false);
        turtorialButton.SetActive(false);
        quitButton.SetActive(false);
        turtorialPanel.SetActive(true);
    }
    public void Back()
    {
        playButton.SetActive(true);
        turtorialButton.SetActive(true);
        quitButton.SetActive(true);
        turtorialPanel.SetActive(false);
    }
}

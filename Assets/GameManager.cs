using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject startGame;
    public GameObject gameinterface;
    public GameObject LostPanel;
    public CameraPosition cameraPosition;
    public GameObject helpPanel;

    public PlayerStats playerStats;

    bool endGame = false;
    private void Update()
    {
        if (playerStats.playerHP < 1 && !endGame)
        {
            LostPanel.SetActive(true);
            Time.timeScale = 0f;
            cameraPosition.enabled = false;
            endGame = true;
        }
    }

    public void restartScene()
    {
        Time.timeScale = 1f;
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    public void healpButton()
    {
        helpPanel.SetActive(!helpPanel.activeSelf);
    }
    private void Start()
    {
        Time.timeScale = 0f;
        cameraPosition.enabled = false;
        LostPanel.SetActive(false);
        helpPanel.SetActive(false);
    }

    public void buttonStart()
    {
        Time.timeScale = 1f;
        startGame.SetActive(false);
        gameinterface.SetActive(true);
        cameraPosition.enabled = true;
        LostPanel.SetActive(false);
    }

    public void buttonExit()
    {
        Application.Quit();
    }
}

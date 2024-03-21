using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject WinPanel;
    public void Win()
    {
        Debug.Log("YOU WON");
        PauseGame();
        WinPanel.SetActive(true);
        //pausa
        //abrir canvas de final de juego
    }
    void PauseGame()
    {
        Time.timeScale = 0;
    }

    void ResumeGame()
    {
        Time.timeScale = 1;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GetValueDropdown getValueDropdown;

    private void Awake()
    {
        //getValueDropdown = GetComponent<GetValueDropdown>();
    }

    private void Update()
    {
        Settings();
    }

    public void PlayGame()
    {
        //Settings();
        SceneManager.LoadScene(1);
    }
    public void Quit()
    {
        Debug.Log("Quitting...");
       Application.Quit();
    }
    void Settings()
    {
        string optionPlayers = getValueDropdown.GetSelectedOption();
        switch (optionPlayers)
        {
            case "1":
                //añadir un jugador
                Debug.Log("Ha selecionado modo juego "+optionPlayers+" jugador");
                break;
            case "2":
                //añadir dos jugadores
                Debug.Log("Ha selecionado modo juego " + optionPlayers + " jugador");
                break;
            case "3":
                //añadir 3 jugadores
                Debug.Log("Ha selecionado modo juego " + optionPlayers + " jugador");
                break;
            case "4":
                //añadir cuatro jugadores
                Debug.Log("Ha selecionado modo juego " + optionPlayers + " jugador");
                break;
        }
    }
}

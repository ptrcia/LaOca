using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GetValueDropdown getValueDropdownPlayers;
    [SerializeField] GetValueDropdown getValueDropdownBoards;
    [SerializeField] GetValueDropdown getValueDropdownLanguage;

    [SerializeField] private GameObject startButton;

    private void Update()
    {
        SettingsPlayers();
        SettingsBoard();
        SettingsLanguage();
    }

    public void PlayGame()
    {
        FadeOutButton();
        float delay = 1f;
        Invoke("LoadScene", delay);
    }
    public void Quit()
    {
        Debug.Log("Quitting...");
       Application.Quit();
    }
    void SettingsPlayers()
    {
        string optionPlayers = getValueDropdownPlayers.GetSelectedOption();
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

    void SettingsBoard()
    {
        string optionBoard = getValueDropdownBoards.GetSelectedOption();
        switch (optionBoard)
        {
            case "Classic":
                //cambiar el tablero y las reglas y supongo que ya la escena, no?
                Debug.Log("Has selecionado la tabla " + optionBoard);
                break;

        }
    }
    void SettingsLanguage()
    {
        string optionLanguage = getValueDropdownLanguage.GetSelectedOption();
        switch (optionLanguage)
        {
            case "English":
                //cosas
                Debug.Log("Has selecionado el idioma " + optionLanguage);
                break;
            case "Spanish":
                //cosas en español
                Debug.Log("Has selecionado el idioma " + optionLanguage);
                break;

        }
    }

    void LoadScene()
    {
        SceneManager.LoadScene(1);
    }
    private void FadeOutButton()
    {
        RectTransform rectTransform = startButton.GetComponent<RectTransform>();
        Vector2 screenSize = new Vector2(Screen.width*5, Screen.height*5);
        rectTransform.DOSizeDelta(screenSize, 2f);
    }
}

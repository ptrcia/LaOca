using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    public bool nextTurnPlayer;
    public int currentPlayerIndex = 0;
    [Header("PlayerPrefab")]
    [SerializeField] GameObject playerPrefab;
    [SerializeField] Transform[] playerStartPosition;
    [SerializeField] Color[] playerColor;
    [Header("PlayerButtonPrefab")]
    public GameObject cloneButtonPrefab;
    [SerializeField] GameObject playerButtonPrefab;
    [SerializeField] Transform[] playerButtonStartPosition;
    [Header("PlayerList")]
    public GameObject currentPlayer;
    public List<GameObject> players = new List<GameObject>();
    [Header("ButtonList")]
    public GameObject currentButton;
    RectTransform currentButtonRectTransform;
    RectTransform originalButtonRectTransform;
    List<GameObject> buttonPlayerList = new List<GameObject>();
    int rename = 1;

    PlayerMovement playerMovement;
    GameManagerUI gameManagerUI;
    Dice dice;
    GameRules gameRules;

    private void Awake()
    {
        Debug.Log("Player count: " + PlayerPrefs.GetInt("NumberPlayers"));
        for(int i = 0; i< PlayerPrefs.GetInt("NumberPlayers"); i++)
        {
            #region Player
            GameObject clonePrefab = Instantiate(playerPrefab, playerStartPosition[i]);
            clonePrefab.GetComponent<Renderer>().material.color = playerColor[i];
            clonePrefab.transform.localScale = new Vector3(5f, 0.1f, 5f);
            clonePrefab.transform.localPosition = new Vector3(0, 0.1f, 0);
            string newID = "Player " + (i + 1).ToString();
            clonePrefab.GetComponent<PlayerMovement>().playerID = newID;
            //Debug.Log("Prefab ID: " + clonePrefab.GetComponent<PlayerMovement>().playerID);
            clonePrefab.GetComponentInChildren<TextMeshProUGUI>().text = newID;
            #endregion

            #region Player Button
            cloneButtonPrefab = Instantiate(playerButtonPrefab, playerButtonStartPosition[i]);
            cloneButtonPrefab.GetComponent<Image>().color = new Color(playerColor[i].r, playerColor[i].g, playerColor[i].b, 1f);
            string newIDButton = "Player " + (i + 1).ToString();
            cloneButtonPrefab.GetComponentInChildren<TextMeshProUGUI>().text = newIDButton;
            cloneButtonPrefab.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
            #endregion
        }
        currentPlayer = GameObject.FindGameObjectWithTag("Player");
        dice = GameObject.FindGameObjectWithTag("Dice").
            GetComponent<Dice>();
        gameManagerUI = GameObject.FindGameObjectWithTag("GameManagerUI")
            .GetComponent<GameManagerUI>();
        gameRules = GameObject.FindGameObjectWithTag("GameRules").
            GetComponent<GameRules>();
    }
    void Start()
    {
        //playerList
        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player"); 
        foreach (GameObject player in playerObjects)
        {
            players.Add(player);
            //Debug.Log("Lista de Jugadores:" + player.GetComponent<PlayerMovement>().playerID);
        }
        //buttonPlayerList
        GameObject[] playerButtons = GameObject.FindGameObjectsWithTag("PlayerButton");
        foreach(GameObject button in playerButtons)
        {
            //rename = 1;
            button.name = "PlayerButton" + rename;
            buttonPlayerList.Add(button);
            Debug.Log(button.ToString());
            rename++;
        }
        StartGame();
    }

    void StartGame()
    {
        Debug.Log("Roll to decide order");
        PlayerOrderAutomatic2();
        //StartCoroutine(PlayerOrder());
        //PlayerOrder();
        StartCoroutine(PlayerTurn());
    }

    #region Turns
    IEnumerator PlayerTurn()
    {
        //Debug.Log("PLAYERTURN TE TOCA A TI: " + currentPlayer.GetComponent<PlayerMovement>().playerID);
        while (currentPlayerIndex < players.Count)
        {
            nextTurnPlayer = true;
            currentPlayer = players[currentPlayerIndex];
            //give data
            currentButton = getButtonById(currentPlayer.GetComponent<PlayerMovement>().playerID); 
            currentButtonRectTransform = currentButton.GetComponent<RectTransform>();

            //Debug.Log("Current Player Index: " + currentPlayerIndex);
            //Debug.Log("Current Player: " + currentPlayer.GetComponent<PlayerMovement>().playerID);
            //Debug.Log("Button Index: " + players.IndexOf(currentPlayer));
            //Debug.Log("Current Button: " + currentButton.name);
            Debug.Log("-------------------------- NEW TURN");
            Debug.Log("Turno del -> " + currentPlayer.GetComponent<PlayerMovement>().playerID);
            //Debug.Log("Con la etiqueta ->" + currentButton.name);

            //Animate button
            gameManagerUI.CurrentTurnAnimation(currentButtonRectTransform);

            Debug.Log("no playable turns : " + currentPlayer.GetComponent<PlayerMovement>().noPlayableTurns);

            //make the Gane Rules know whos turn is
            gameRules.CheckWhosTurn(currentPlayer.GetComponent<PlayerMovement>());

            //player can NOT play his turn
            if (currentPlayer.GetComponent<PlayerMovement>().noPlayableTurns != 0)
            {
                Debug.Log("Skip Turn");
                gameManagerUI.CurrentTurnAnimationClose(currentButtonRectTransform);
                currentPlayer.GetComponent<PlayerMovement>().noPlayableTurns --;
                currentPlayerIndex++;

                if (currentPlayerIndex >= players.Count)
                {
                    currentPlayerIndex = 0;
                    gameManagerUI.StartAnimatingRound();
                    Debug.Log("-----New Round-----");
                }
            }
            //player can play his turn
            else
            {
                Debug.Log("NO Skip Turn");
                currentPlayer.GetComponent<PlayerMovement>().MoveIfDiceRolled();                             
                while (!currentPlayer.GetComponent<PlayerMovement>().HasCompletedMovement())
                {
                    yield return null; 
                }
                if (nextTurnPlayer) {
                    gameManagerUI.CurrentTurnAnimationClose(currentButtonRectTransform);
                    currentPlayerIndex++;

                    if (currentPlayerIndex >= players.Count)
                    {
                        currentPlayerIndex = 0;
                        gameManagerUI.StartAnimatingRound();
                        Debug.Log("-----New Round-----");
                    }
                }
            }
        }
    }
    #endregion

    #region Order Automatic
    void PlayerOrderAutomatic()
    {
        players.Sort((a, b) =>
        {
            int diceA, diceB;
            do
            {
                diceA = dice.RollDice();
                diceB = dice.RollDice();
                //diceA = Random.Range(1, 6);
                //diceB = Random.Range(1, 6);
            } while (diceA == diceB);
            Debug.Log("Player 1 roll ->" + diceA);
            Debug.Log("Player 2 roll ->" + diceB);

            return diceB.CompareTo(diceA);
        });
        
        Debug.Log("Orden de los jugadores por turnos:");
        for (int i = 0; i < players.Count; i++)
        {
            Debug.Log("Turno " + (i + 1) + ": " + players[i].GetComponent<PlayerMovement>().playerID );
        }
    }
    #endregion

    void PlayerOrderAutomatic2()
    {
        Dictionary<GameObject, int> diceResults = new Dictionary<GameObject, int>();

        foreach (GameObject player in players)
        {
            int diceResult;
            do
            {
                diceResult = dice.RollDice();
            } while (diceResults.ContainsValue(diceResult));
            diceResults.Add(player, diceResult);

            Debug.Log(player.GetComponent<PlayerMovement>().playerID + " roll -> " + diceResult);
        }

        players.Sort((a, b) =>
        {
            int indexA = diceResults[a];
            int indexB = diceResults[b];
            return indexB.CompareTo(indexA);
        });

        Debug.Log("Order: ");
        for (int i = 0; i < players.Count; i++)
        {
            Debug.Log("Turn " + (i + 1) + ": " + players[i].GetComponent<PlayerMovement>().playerID);
        }
    }
   
    
    #region Order by Rolling
    void PlayerOrder()
    {
        List<int> diceValues = new List<int>();

        // Cada jugador realiza su lanzamiento de dado y almacena su valor
        foreach (GameObject player in players)
        {
            StartCoroutine(WaitForPlayerOrder(player, diceValues));
        }
    }

    IEnumerator WaitForPlayerOrder(GameObject player, List<int> diceValues)
    {
        // wait till player has rolled the dice
        while (!player.GetComponent<PlayerMovement>().HasCompletedMovement())
        {
            yield return null;
        }

        // add the value to the list
        diceValues.Add(player.GetComponent<PlayerMovement>().diceValue);

        // It everyone thowed, sort the numbers
        if (diceValues.Count == players.Count)
        {
            SortPlayersByDiceValues(diceValues);
        }
    }

    void SortPlayersByDiceValues(List<int> diceValues)
    {
        Debug.Log("Orden de los jugadores por turnos:");
        for (int i = 0; i < players.Count; i++)
        {
            Debug.Log("Turno " + (i + 1) + ": " + players[i].GetComponent<PlayerMovement>().playerID);
        }
    }
    #endregion


    GameObject getButtonById(String playerId)
    {
        //Debug.Log("ID DEL JUGADOR EN LA NUEVA FUNCION JEJEJEJEJEJ: " + playerId);
        foreach (GameObject button in buttonPlayerList) {
            //Debug.Log("TEXT DEL BOTON QUE TOCA COMPARAR: " + button.GetComponentInChildren<TextMeshProUGUI>().text);
            if (button.GetComponentInChildren<TextMeshProUGUI>().text == playerId)
            {
                //Debug.Log("ID DEL BOTON QUE LE TOCA " + (button.GetComponentInChildren<TextMeshProUGUI>().text).ToString());
                return button;
            }
        }
        Debug.Log("It havent been found none buttoin with that id. returning 1 by default");
        return buttonPlayerList[0];
    } 
}

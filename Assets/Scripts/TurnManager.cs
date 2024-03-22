using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public bool nextTurnPlayer;
    public int currentPlayerIndex = 0;
    [SerializeField] GameObject playerPrefab;
    [SerializeField] Transform[] playerStartPosition;
    [SerializeField] Color[] playerColor;

    Dice dice;
    GameObject currentPlayer;
    List<GameObject> players = new List<GameObject>();
    List<int> diceValues = new List<int>();

    private void Awake()
    {
        Debug.Log("Numero de jugadores: " + PlayerPrefs.GetInt("NumberPlayers"));
        for(int i = 0; i< PlayerPrefs.GetInt("NumberPlayers"); i++)
        {
            GameObject clonePrefab = Instantiate(playerPrefab, playerStartPosition[i]);
            clonePrefab.GetComponent<Renderer>().material.color = playerColor[i];
            clonePrefab.transform.localScale = new Vector3(5f, 0.1f, 5f);

        }
        currentPlayer = GameObject.FindGameObjectWithTag("Player");
        dice = GameObject.FindGameObjectWithTag("Dice").
            GetComponent<Dice>();
    }
    void Start()
    {
        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in playerObjects)
        {
            players.Add(player);
            Debug.Log("Lista de Jugadores:" + player.ToString());
        }
        StartGame();
    }

    void StartGame()
    {
        Debug.Log("TIRAR PARA DECIDIR ORDEN");
        PlayerOrderAutomatic();
        //StartCoroutine(PlayerOrder());
        //PlayerOrder();
        StartCoroutine(PlayerTurn());
    }

    IEnumerator PlayerTurn()
    {
        while (currentPlayerIndex < players.Count)
        {
            nextTurnPlayer = true;

            currentPlayer = players[currentPlayerIndex];
            Debug.Log("Turno del -> " + currentPlayer.name);

            Debug.Log("no playable turns : " + currentPlayer.GetComponent<PlayerMovement>().noPlayableTurns);

            if (currentPlayer.GetComponent<PlayerMovement>().noPlayableTurns != 0)
            {
                Debug.Log("saltamos turno");
                currentPlayer.GetComponent<PlayerMovement>().noPlayableTurns --;
                currentPlayerIndex++;

                if (currentPlayerIndex >= players.Count)
                {
                    currentPlayerIndex = 0;
                    Debug.Log("-----Nueva Ronda-----");
                }
            }
            else
            {
                Debug.Log("NO saltamos turno");
                // Llamar a MoveIfDiceRolled una vez para cada jugador
                currentPlayer.GetComponent<PlayerMovement>().MoveIfDiceRolled();
                             
                // Esperar hasta que el jugador haya completado su movimiento
                while (!currentPlayer.GetComponent<PlayerMovement>().HasCompletedMovement())
                {
                    yield return null; 
                }
                if (nextTurnPlayer) { 
                    currentPlayerIndex++;

                    if (currentPlayerIndex >= players.Count)
                    {
                        currentPlayerIndex = 0;
                        Debug.Log("-----Nueva Ronda-----");
                    }
                }
            }

        }
    }

    void PlayerOrderAutomatic()
    {
        //players.Sort((a, b) => dice.RollDice().CompareTo(dice.RollDice()));
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
            Debug.Log("Player 1 roll ->"+diceA);
            Debug.Log("Player 2 roll ->" + diceB);
            return diceB.CompareTo(diceA);
        });
        
        // Mostrar el orden de los jugadores en la consola
        Debug.Log("Orden de los jugadores por turnos:");
        for (int i = 0; i < players.Count; i++)
        {
            Debug.Log("Turno " + (i + 1) + ": " + players[i].name);
        }
    }

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
        // Esperar hasta que el jugador haya completado su lanzamiento de dado
        while (!player.GetComponent<PlayerMovement>().HasCompletedMovement())
        {
            yield return null;
        }

        // Agregar el valor del dado del jugador a la lista
        diceValues.Add(player.GetComponent<PlayerMovement>().diceValue);

        // Si todos los jugadores han lanzado los dados, ordenarlos y mostrar el resultado
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
            Debug.Log("Turno " + (i + 1) + ": " + players[i].name);
        }
    }
}

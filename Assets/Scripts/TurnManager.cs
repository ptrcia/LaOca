using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class TurnManager : MonoBehaviour
{

    Dice dice;

    private List<GameObject> players = new List<GameObject>();
    List<int> diceValues = new List<int>();

    private int currentPlayerIndex = 0;
    GameObject currentPlayer;

    public bool nextTurnPlayer;
    //private int whosTurn = 1;

    //Decidir cuantos jugadores y hacer un Invoke??????

    private void Awake()
    {
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
            GameObject currentPlayer = players[currentPlayerIndex];

            Debug.Log("Turno del -> " + currentPlayer.name);

            // Llamar a MoveIfDiceRolled una vez para cada jugador
            currentPlayer.GetComponent<PlayerMovement>().MoveIfDiceRolled();

            // Esperar hasta que el jugador haya completado su movimiento
            while (!currentPlayer.GetComponent<PlayerMovement>().HasCompletedMovement())
            {
                yield return null; // Esperar un frame
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

    public void SkipTurn()
    {
    }
    public void ReRoll()
    {
    }

    void PlayerOrderAutomatic()
    {
        //players.Sort((a, b) => dice.RollDice().CompareTo(dice.RollDice()));
        players.Sort((a, b) =>
        {
            int diceA, diceB;
            do
            {
                //diceA = dice.RollDice();
                //diceB = dice.RollDice();
                diceA = Random.Range(1, 6);
                diceB = Random.Range(1, 6);
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
        // Tu lógica de ordenamiento aquí...

        // Mostrar el orden de los jugadores en la consola
        Debug.Log("Orden de los jugadores por turnos:");
        for (int i = 0; i < players.Count; i++)
        {
            Debug.Log("Turno " + (i + 1) + ": " + players[i].name);
        }
    }
}

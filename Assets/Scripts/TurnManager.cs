using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class TurnManager : MonoBehaviour
{

    Dice dice;

    private List<GameObject> players = new List<GameObject>();
    private int currentPlayerIndex = 0;
    GameObject currentPlayer;


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

        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        PlayerOrder();
        yield return StartCoroutine(PlayerTurn());
    }

    IEnumerator PlayerTurn()
    {
        while (currentPlayerIndex < players.Count)
        {
            GameObject currentPlayer = players[currentPlayerIndex];

            // Hacer algo con el jugador actual (por ejemplo, mover si se lanzaron los dados)
            currentPlayer.GetComponent<PlayerMovement>().MoveIfDiceRolled();
            Debug.Log("Jugador actual: " + currentPlayer.name);


            // Esperar hasta que el jugador haya completado su movimiento
            while (!currentPlayer.GetComponent<PlayerMovement>().HasCompletedMovement())
            {
                //Debug.Log("CompletedMovement=?" + GetComponent<PlayerMovement>().HasCompletedMovement());
                yield return null; // Esperar un frame
            }

            // Incrementar el índice del jugador actual para pasar al siguiente jugador
            currentPlayerIndex++;
            Debug.Log("Índice del jugador actual: " + currentPlayerIndex);


            // Si llegamos al final de la lista, volver al principio (ciclo)
            if (currentPlayerIndex >= players.Count)
            {
                currentPlayerIndex = 0;
                Debug.Log("Se reinicia el índice del jugador.");

            }
        }
    }

    public void SkipTurn()
    {

    }
    public void NextTurn()
    {

    }

    void PlayerOrder()
    {
        players.Sort((a, b) => dice.RollDice().CompareTo(dice.RollDice()));

        // Mostrar el orden de los jugadores en la consola
        Debug.Log("Orden de los jugadores por turnos:");
        for (int i = 0; i < players.Count; i++)
        {
            Debug.Log("Turno " + (i + 1) + ": " + players[i].name);
        }
    }
}

using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Dice dice;
    GameManager gameManager;
    GameRules gameRules;

    public Transform[] cells;
    public int currentCell;


    public bool movementCompleted = true;
    public int diceValue = 0;

    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").
            GetComponent<GameManager>();
        gameRules = GameObject.FindGameObjectWithTag("GameRules").
            GetComponent<GameRules>();
        dice = GameObject.FindGameObjectWithTag("Dice").
            GetComponent<Dice>();
        transform.position = cells[0].position;

    }
    void Start()
    {
        Debug.Log("Casilla Actual: " + currentCell);
    }
  
    void Move()
        {
            int diceResult = dice.RollDice();
            currentCell = currentCell + diceResult;
            transform.position = cells[currentCell].position;
            Debug.Log("CurrentCell-> " + currentCell);
            gameRules.CheckSpecialCell();
            movementCompleted = true;
            Debug.Log("movementCompleted set to true.");
        }

    public void MoveIfDiceRolled()
    {
        movementCompleted = false;
        StartCoroutine(WaitForDiceRolled());
    }

    IEnumerator WaitForDiceRolled()
    {
        while (!dice.diceRolled)
        {
            yield return null;
        }
        Debug.Log("CurrentCell: " + currentCell);
        int diceResult = dice.RollDice();
        currentCell = currentCell + diceResult;   //////////////
        Debug.Log("Dado:" + diceResult);
        transform.position = cells[currentCell].position;
        Debug.Log("CurrentCell tras movimiento-> " + currentCell);
        //numberCell = currentCell + 1;
        gameRules.CheckSpecialCell();
        movementCompleted = true;
        dice.diceRolled = false; 
    }
    public bool HasCompletedMovement()
    {
        return movementCompleted;
    }

    public void OrderIfDiceRolled()
    {
        movementCompleted = false;
        StartCoroutine (WaitOrderIfDiceRolled());
    }
    IEnumerator WaitOrderIfDiceRolled()
    {
        while (!dice.diceRolled)
        {
            yield return null;
        }

        diceValue = dice.RollDice();
        Debug.Log("Dice Value -> " + diceValue);
        movementCompleted = true;
    }
}
    

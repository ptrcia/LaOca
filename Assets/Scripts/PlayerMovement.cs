using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Dice dice;
    GameManager gameManager;

    [SerializeField]
    Transform[] cells;
    [SerializeField]
    public int currentCell = 0;

    private bool movementCompleted = true;

    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").
            GetComponent<GameManager>();
        dice = GameObject.FindGameObjectWithTag("Dice").
            GetComponent<Dice>();
    }
    void Start()
    {
        transform.position = cells[currentCell].position;
        Debug.Log("cURRENTinitial cELL: " + currentCell);

    }

    void FixedUpdate()
    {
        if (dice.diceRolled == true)
        {
            Move();
            dice.diceRolled = false;
        }
        MoveIfDiceRolled();

    }   
    void Move()
        {
            currentCell = currentCell + dice.RollDice();
            transform.position = cells[currentCell].position;
            Debug.Log("CurrentCell-> " + currentCell);
            //gameManager.CheckSpecialCell();
        }

    public void MoveIfDiceRolled()
    {
        if (dice.diceRolled == true)
        {
            currentCell = currentCell + dice.RollDice();
            transform.position = cells[currentCell].position;
            Debug.Log("CurrentCell-> " + currentCell);
            //gameManager.CheckSpecialCell();
            movementCompleted = true;
        }
        dice.diceRolled = false;
    }
    public bool HasCompletedMovement()
    {
        return movementCompleted;
    }
}
    

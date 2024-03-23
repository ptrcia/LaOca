using DG.Tweening;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Linq;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Dice dice;
    GameManager gameManager;
    GameRules gameRules;
    PauseMenu pauseMenu;

    int playersInCell;

    //public Transform[] cells;
    public int currentCell;
    public int noPlayableTurns = 0 ;
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
        pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu")
            .GetComponent<PauseMenu>();

        //transform.position = cells[0].position;
        //transform.position = CellManager.instance.cells[0].position;
    }
    void Start()
    {
        Debug.Log("Casilla Actual: " + currentCell);
        pauseMenu.diceImage.localScale = Vector3.zero;
    }
  
    void Move()
        {
            diceValue = dice.RollDice();
            currentCell = currentCell + diceValue;
            transform.position = CellManager.instance.cells[currentCell].position;
            Debug.Log("CurrentCell-> " + currentCell);
            //gameRules.CheckSpecialCell(this);
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
        pauseMenu.AnimatingDiceIimage();
        transform.position = CellManager.instance.cells[currentCell].position; //ATENCION
        //CellArragement();
        Debug.Log("CurrentCell tras movimiento-> " + currentCell);
        Debug.Log("not playable turns befor checking "+noPlayableTurns);
        //gameRules.CheckSpecialCell(this, this.gameObject);
        Debug.Log("not playable turns after checking " + noPlayableTurns);
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
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<CellContainer>())
        {
            playersInCell = other.gameObject.GetComponent<CellContainer>().currentPlayersInCell;
        }
    }
    void CellArragement()
    {
        if (playersInCell == 2)
        {
            Debug.Log("Hay 2 players aqui");//arriba
            transform.position = new Vector3
                (CellManager.instance.cells[currentCell].position.x - 0.5f, CellManager.instance.cells[currentCell].position.y, CellManager.instance.cells[currentCell].position.z);
        }
        else if(playersInCell > 2)
        {
            Debug.Log("Hay + de 2 players aqui");
            //ni idea
            //arroba a la derecha
            transform.position = new Vector3
                (CellManager.instance.cells[currentCell].position.x - 0.5f, CellManager.instance.cells[currentCell].position.y, CellManager.instance.cells[currentCell].position.z + 0.5f);
        }else
        {
            transform.position = CellManager.instance.cells[currentCell].position;
        }

    }


}
    

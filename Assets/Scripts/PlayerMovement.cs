using DG.Tweening;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Dice dice;
    //GameManager gameManager;
    GameRules gameRules;
    PauseMenu pauseMenu;

    //int playersInCell;

    public int currentCell;
    public int noPlayableTurns = 0 ;
    public bool movementCompleted = true;
    public int diceValue = 0;
    public string playerID = "sinID";


    private void Awake()
    {
        //gameManager = GameObject.FindGameObjectWithTag("GameManager").
            //GetComponent<GameManager>();
        gameRules = GameObject.FindGameObjectWithTag("GameRules").
            GetComponent<GameRules>();
        dice = GameObject.FindGameObjectWithTag("Dice").
            GetComponent<Dice>();
        pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu")
            .GetComponent<PauseMenu>();
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

    #region Move
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
        pauseMenu.AnimatingDiceImage();
        transform.position = CellManager.instance.cells[currentCell].position; //ATENCION
        //CellArragement();
        Debug.Log("CurrentCell tras movimiento-> " + currentCell);
        Debug.Log("not playable turns befor checking "+noPlayableTurns);
        gameRules.CheckSpecialCell(this, this.gameObject);
        Debug.Log("not playable turns after checking " + noPlayableTurns);
        movementCompleted = true;
        dice.diceRolled = false; 
    }
    public bool HasCompletedMovement()
    {
        return movementCompleted;
    }
    #endregion

    #region Order By Rolling
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
    #endregion

    #region Cell Arragement

   /* void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<CellContainer>())
        {           
            playersInCell = other.gameObject.GetComponent<CellContainer>().currentPlayersInCell;
            Debug.Log(playersInCell + "<------ colision del player");
        }
    }*/

    public void CellArragement(int playersCounter, List<GameObject>players)
    {
        if (playersCounter == 2)
        {
            Debug.Log("Hay 2 players aqui");

            //abajo
            players[0].transform.position = new Vector3(
                CellManager.instance.cells[currentCell].position.x,
                CellManager.instance.cells[currentCell].position.y,
                CellManager.instance.cells[currentCell].position.z + 0.2f);
            //arriba
            players[1].transform.position = new Vector3(
                CellManager.instance.cells[currentCell].position.x,
                CellManager.instance.cells[currentCell].position.y,
                CellManager.instance.cells[currentCell].position.z - 0.2f);
            
        }
        else if(playersCounter == 3 )
        {
            Debug.Log("Hay 3 players aqui");
            //abajo
            players[0].transform.position = new Vector3(
                CellManager.instance.cells[currentCell].position.x, 
                CellManager.instance.cells[currentCell].position.y, 
                CellManager.instance.cells[currentCell].position.z - 0.2f);
            //arriba derehca
            players[1].transform.position = new Vector3(
                CellManager.instance.cells[currentCell].position.x - 0.17f,
                CellManager.instance.cells[currentCell].position.y,
                CellManager.instance.cells[currentCell].position.z + 0.2f);
            //arriba izquierda
            players[2].transform.position = new Vector3(              
                CellManager.instance.cells[currentCell].position.x + 0.17f,
                CellManager.instance.cells[currentCell].position.y,
                CellManager.instance.cells[currentCell].position.z + 0.2f);
        }
        else if(playersCounter == 4 )
        {
            Debug.Log("Hay 4 players aqui");
            //abajo derecha
            players[0].transform.position = new Vector3(
                CellManager.instance.cells[currentCell].position.x + 0.17f, 
                CellManager.instance.cells[currentCell].position.y,
                CellManager.instance.cells[currentCell].position.z - 0.2f);
            //arriba derecha
            players[1].transform.position = new Vector3(
                CellManager.instance.cells[currentCell].position.x - 0.17f,
                CellManager.instance.cells[currentCell].position.y,
                CellManager.instance.cells[currentCell].position.z + 0.2f);
            //arriba izquierda
            players[2].transform.position = new Vector3(
                CellManager.instance.cells[currentCell].position.x + 0.17f,
                CellManager.instance.cells[currentCell].position.y,
                CellManager.instance.cells[currentCell].position.z + 0.2f);
            //abajo izquierda
            players[3].transform.position = new Vector3(
                CellManager.instance.cells[currentCell].position.x - 0.17f,
                CellManager.instance.cells[currentCell].position.y,
                CellManager.instance.cells[currentCell].position.z - 0.2f);
        }
        else
        {
            Debug.Log("Al centro");
            players[0].transform.position = new Vector3(
                CellManager.instance.cells[currentCell].position.x,
                CellManager.instance.cells[currentCell].position.y,
                CellManager.instance.cells[currentCell].position.z);
        }
    }





    #endregion
}


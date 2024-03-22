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

    int playersInCell;

    public Transform[] cells;
    public int currentCell;
    public int noPlayableTurns = 0 ;
    public bool movementCompleted = true;
    public int diceValue = 0;

    [SerializeField] RectTransform diceImage;

    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").
            GetComponent<GameManager>();
        gameRules = GameObject.FindGameObjectWithTag("GameRules").
            GetComponent<GameRules>();
        dice = GameObject.FindGameObjectWithTag("Dice").
            GetComponent<Dice>();
        transform.position = cells[0].position;

        cells[1] = GameObject.Find("Cell (1)").transform;
        cells[2] = GameObject.Find("Cell (2)").transform;
        cells[3] = GameObject.Find("Cell (3)").transform;
        cells[4] = GameObject.Find("Cell (4)").transform;
        cells[5] = GameObject.Find("Cell (5)").transform;
        cells[6] = GameObject.Find("Cell (6)").transform;
        cells[7] = GameObject.Find("Cell (7)").transform;
        cells[8] = GameObject.Find("Cell (8)").transform;
        cells[9] = GameObject.Find("Cell (9)").transform;
        cells[10] = GameObject.Find("Cell (10)").transform;
        cells[11] = GameObject.Find("Cell (11)").transform;
        cells[12] = GameObject.Find("Cell (12)").transform;
        cells[13] = GameObject.Find("Cell (13)").transform;
        cells[14] = GameObject.Find("Cell (14)").transform;
        cells[15] = GameObject.Find("Cell (15)").transform;
        cells[16] = GameObject.Find("Cell (16)").transform;
        cells[17] = GameObject.Find("Cell (17)").transform;
        cells[18] = GameObject.Find("Cell (18)").transform;
        cells[19] = GameObject.Find("Cell (19)").transform;
        cells[20] = GameObject.Find("Cell (20)").transform;

    }
    void Start()
    {
        Debug.Log("Casilla Actual: " + currentCell);
        diceImage.localScale = Vector3.zero;
    }
  
    void Move()
        {
            diceValue = dice.RollDice();
            currentCell = currentCell + diceValue;
            transform.position = cells[currentCell].position;
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
        AnimatingDiceIimage();
        transform.position = cells[currentCell].position;
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
                (cells[currentCell].position.x - 0.5f, cells[currentCell].position.y, cells[currentCell].position.z);
        }
        else if(playersInCell > 2)
        {
            Debug.Log("Hay + de 2 players aqui");
            //ni idea
            //arroba a la derecha
            transform.position = new Vector3
                (cells[currentCell].position.x - 0.5f, cells[currentCell].position.y, cells[currentCell].position.z + 0.5f);
        }else
        {
            transform.position = cells[currentCell].position;
        }

    }

    void AnimatingDiceIimage()
    {
        diceImage.DOScale(new Vector3(0.5f, 0.5f, diceImage.localScale.z), 1f).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            diceImage.DOScale(Vector3.zero, 0.5f).SetEase(Ease.OutQuad);
        });
    }
}
    

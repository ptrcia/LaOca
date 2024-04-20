using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Dice dice;
    GameRules gameRules;
    GameManagerUI gameManagerUI;
    CellContainer cellContainer;

    public int currentCell;
    public int noPlayableTurns = 0 ;
    public bool movementCompleted = true;
    public int diceValue = 0;
    public string playerID = "sinID";

    private void Awake()
    {
        gameRules = GameObject.FindGameObjectWithTag("GameRules").
            GetComponent<GameRules>();
        dice = GameObject.FindGameObjectWithTag("Dice").
            GetComponent<Dice>();
        gameManagerUI = GameObject.FindGameObjectWithTag("GameManagerUI")
            .GetComponent<GameManagerUI>();
    }
    void Start()
    {
        Debug.Log("Casilla Actual: " + currentCell);
        gameManagerUI.diceImage.localScale = Vector3.zero;
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        //esto lo quiero hacer en el pozo, no aquí
        cellContainer = collision.gameObject.GetComponent<CellContainer>();
        int lastCountWell = 0;
        //esto tampoco debería valer porque al llegar a la casilla no tiene porque estar en 0
        //lo actualizo en el update??
        if (collision.gameObject.CompareTag("Cell") && currentCell == 30)
        {
            while(cellContainer.playersRegistry.Count != lastCountWell)
            {
                //NO TENGO NI IDEA AYUDANME
                noPlayableTurns++;
            }
            Debug.Log("La lista ha aumentado");
            lastCountWell = cellContainer.playersRegistry.Count;
            //esto no deberia funcionar, porque cada celda tiene una contabilidad distinta
        }
    }*/

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
        //Debug.Log("CurrentCell: " + currentCell);
        int diceResult = dice.RollDice();

        Debug.Log("Dice Result:" + diceResult);
        for(int i = 0; i < diceResult; i++)
        {
            currentCell++;
            //corrutina
            //StartMovementAnimation(); 
            Debug.Log("From cell tot cell: "+currentCell);
        }
          
        gameManagerUI.AnimatingDiceImage();
        transform.position = CellManager.instance.cells[currentCell].position; //ATENCION
        //Debug.Log("CurrentCell AFTER chhecking-> " + currentCell);
        Debug.Log("not playable turns BEFORE checking "+noPlayableTurns);
        gameRules.CheckSpecialCell(this, this.gameObject);
        Debug.Log("not playable turns AFTER checking " + noPlayableTurns);
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

    public void CellArragement(int playersCounter, List<GameObject>players)
    {
        //Debug.Log("VARIABLES CellArrangement \n - playersCounter: " + playersCounter.ToString() + " |- players: " + listaStringPorfi(players));
        if (playersCounter == 2)
        {
            Debug.Log("2 players here");

            //down
            players[0].transform.position = new Vector3(
                CellManager.instance.cells[currentCell].position.x,
                CellManager.instance.cells[currentCell].position.y,
                CellManager.instance.cells[currentCell].position.z + 0.2f);
            //up
            players[1].transform.position = new Vector3(
                CellManager.instance.cells[currentCell].position.x,
                CellManager.instance.cells[currentCell].position.y,
                CellManager.instance.cells[currentCell].position.z - 0.2f);
            
        }
        else if(playersCounter == 3 )
        {
            Debug.Log("3 players here");
            //down
            players[0].transform.position = new Vector3(
                CellManager.instance.cells[currentCell].position.x, 
                CellManager.instance.cells[currentCell].position.y, 
                CellManager.instance.cells[currentCell].position.z - 0.2f);
            //up right
            players[1].transform.position = new Vector3(
                CellManager.instance.cells[currentCell].position.x - 0.17f,
                CellManager.instance.cells[currentCell].position.y,
                CellManager.instance.cells[currentCell].position.z + 0.2f);
            //up left
            players[2].transform.position = new Vector3(              
                CellManager.instance.cells[currentCell].position.x + 0.17f,
                CellManager.instance.cells[currentCell].position.y,
                CellManager.instance.cells[currentCell].position.z + 0.2f);
        }
        else if(playersCounter == 4 )
        {
            Debug.Log("4 players here");
            //down right
            players[0].transform.position = new Vector3(
                CellManager.instance.cells[currentCell].position.x + 0.17f, 
                CellManager.instance.cells[currentCell].position.y,
                CellManager.instance.cells[currentCell].position.z - 0.2f);
            //up right
            players[1].transform.position = new Vector3(
                CellManager.instance.cells[currentCell].position.x - 0.17f,
                CellManager.instance.cells[currentCell].position.y,
                CellManager.instance.cells[currentCell].position.z + 0.2f);
            //up left
            players[2].transform.position = new Vector3(
                CellManager.instance.cells[currentCell].position.x + 0.17f,
                CellManager.instance.cells[currentCell].position.y,
                CellManager.instance.cells[currentCell].position.z + 0.2f);
            //down left
            players[3].transform.position = new Vector3(
                CellManager.instance.cells[currentCell].position.x - 0.17f,
                CellManager.instance.cells[currentCell].position.y,
                CellManager.instance.cells[currentCell].position.z - 0.2f);
        }
        else
        {
            Debug.Log("Center");
            players[0].transform.position = new Vector3(
                CellManager.instance.cells[currentCell].position.x,
                CellManager.instance.cells[currentCell].position.y,
                CellManager.instance.cells[currentCell].position.z);
        }
    }

    #endregion

    #region Movement Animation
    public void StartMovementAnimation() 
    {
        StartCoroutine(nameof(MovementAnimation));
    }
    IEnumerator MovementAnimation() //EN PROCWASO
    {
        float duration = 0.5f; 
        AnimationCurve curve = new AnimationCurve(); 

        Vector3 midPoint = transform.position / 2f;

        Sequence movementSequence = DOTween.Sequence();
        movementSequence.Append(transform.DOMove(midPoint, duration / 2f)
            .SetEase(Ease.OutQuad));
        movementSequence.Append(transform.DOMove(transform.position, duration / 2f)
            .SetEase(Ease.InQuad));
        movementSequence.Play();

        yield return
        transform.DOMove(transform.position, duration).SetEase(curve)
                .WaitForCompletion();
    }
    #endregion



    string PlayerStringList(List<GameObject> players)  // Method to print the list of players id
    {
        string StringsIdsPlayers = "";
        foreach (GameObject player in players)
        {
            StringsIdsPlayers += player.GetComponent<PlayerMovement>().playerID;
        }

        return StringsIdsPlayers;
    }
}


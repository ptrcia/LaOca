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

    //[SerializeField] GameObject playerButton;

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
        //playerButton = GameObject.FindGameObjectWithTag("PlayerButton");
        Debug.Log("Casilla Actual: " + currentCell);
        gameManagerUI.diceImage.localScale = Vector3.zero;
    }

    private void OnCollisionEnter(Collision collision)
    {
        cellContainer = collision.gameObject.GetComponent<CellContainer>();
        if (collision.gameObject.CompareTag("Cell"))
        {
            if(cellContainer.playersRegistry.Count != cellContainer.playersRegistry.Count)
            {
                //NO TENGO NI IDEA AYUDANME
            }
        }
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
        
        Debug.Log("Dado:" + diceResult);
        for(int i = 0; i < diceResult; i++)
        {
            currentCell++;
            //corrutina
            //StartMovementAnimation(); NE PROCESOOOO
            Debug.Log(currentCell);
        }
          
        gameManagerUI.AnimatingDiceImage();
        transform.position = CellManager.instance.cells[currentCell].position; //ATENCION
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

    #region Movement Animation
    public void StartMovementAnimation() //Cell to cell
    {
        StartCoroutine(nameof(MovementAnimation));
    }
    IEnumerator MovementAnimation() //EN PROCWASO
    {
        float duration = 0.5f; // Duración de la animación de salto
        AnimationCurve curve = new AnimationCurve(); // Curva de animación de salto
        //curve = new AnimationCurve();
        // Punto medio entre A y B
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
}


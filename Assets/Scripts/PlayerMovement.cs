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
        Debug.Log("CurrentCell: " + currentCell);
        int diceResult = dice.RollDice();
        
        Debug.Log("Dado:" + diceResult);
        for(int i = 0; i < diceResult; i++)
        {
            currentCell++;
            //corrutina
            //StartMovementAnimation(); 
            Debug.Log("Cual es la curretn cell dentro del FOR: "+currentCell);
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

        /*
         ArgumentOutOfRangeException: Index was out of range. Must be non-negative and less than the size of the collection.
Parameter name: index
System.Collections.Generic.List`1[T].get_Item (System.Int32 index) (at <787acc3c9a4c471ba7d971300105af24>:0)
PlayerMovement.CellArragement (System.Int32 playersCounter, System.Collections.Generic.List`1[T] players) (at Assets/Scripts/PlayerMovement.cs:186)
CellContainer.OnCollisionExit (UnityEngine.Collision collision) (at Assets/Scripts/CellContainer.cs:38)
UnityEngine.Physics:OnSceneContact(PhysicsScene, IntPtr, Int32)
*/
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
}


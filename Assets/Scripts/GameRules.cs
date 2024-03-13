using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRules : MonoBehaviour
{
    [SerializeField] GameObject player;

    PlayerMovement playerMovement;
    GameManager gameManager;
    TurnManager turnManager;


    int firstBridge = 5;
    int secondBridge = 11;

    int firstDice = 15;
    int secondDice = 12;

    int finalCell = 62;

    private void Awake()
    {
        playerMovement = GameObject.
            FindGameObjectWithTag("Player").
            GetComponent<PlayerMovement>();
        gameManager = GameObject.
            FindGameObjectWithTag("GameManager").
            GetComponent<GameManager>();
        turnManager = GameObject.FindGameObjectWithTag("TurnManager").
            GetComponent<TurnManager>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    public void CheckSpecialCell()
    {
        Awake();

        switch (playerMovement.currentCell)
        {
            case 4 or 8 or 13 or 17 or 22 or 26 or 31 or 35 or
               40 or 44 or 49 or 53 or 58:
                Debug.Log("Oca");
                Oca();
                break;
            case 5 or 11:
                Debug.Log("Puente");
                Puente();
                break;
            case 18:
                Debug.Log("Posada");
                Posada();
                break;
        }
        void Oca()
        {
            if (playerMovement.currentCell != 59)
            {
                Debug.Log("curretnBoardCell" + playerMovement.currentCell);
                //currentBoardCell = currentBoardCell + 3;
                //player.transform.position = playerMovement.cells[currentBoardCell+3].position;
                Debug.Log("+3  ->" + playerMovement.currentCell);
                turnManager.ReRoll();
            }
            else if (playerMovement.currentCell == 59)
            {
                gameManager.Win();
            }


        }
        void Puente()
        {
            Debug.Log("CHECKSPECIALCELL PUENTEEEEE");
            if (playerMovement.currentCell == firstBridge)
            {
                playerMovement.currentCell = secondBridge;
                player.transform.position = playerMovement.cells[playerMovement.currentCell].position;
                turnManager.nextTurnPlayer = false;

            }
            else if (playerMovement.currentCell == secondBridge)
            {
                playerMovement.currentCell = firstBridge;
                player.transform.position = playerMovement.cells[playerMovement.currentCell].position;
                turnManager.nextTurnPlayer = false;

            }
        }
        void Posada()
        {
            //turnManager.SkipTurn();
            //a la sioguiente ronda no juega
        }
        void Pozo()
        {
            /*
            while (player2.currentCell!=31 ||player3.currentCell ...)
            {
                //turno.SaltarTturno;
            }
            */
        }
        void Laberinto()
        {
            playerMovement.currentCell = 30;
        }
        void Cárcel()
        {
            /*
             while(!player2.layerMovement.currentCell != 56 || ...)
            {
            turno.SaltarTurno();
            }
             * */
        }
        void Dados()
        {
            if (playerMovement.currentCell == firstDice)
            {
                playerMovement.currentCell = secondDice;
                player.transform.position = playerMovement.cells[playerMovement.currentCell].position;

            }
            else if (playerMovement.currentCell == secondDice)
            {
                playerMovement.currentCell = firstDice;
                player.transform.position = playerMovement.cells[playerMovement.currentCell].position;

            }
        }
        void Calavera()
        {
            playerMovement.currentCell = 1;
        }
        void Final()
        {
            //gameManager.Win();
        }
        void Jardin()
        {
            playerMovement.currentCell = playerMovement.currentCell -
                            (playerMovement.currentCell - finalCell);
            player.transform.position = playerMovement.cells[playerMovement.currentCell].position;

            CheckSpecialCell();
        }


        /*
                 switch (playerMovement.currentCell)
            {
     

                case 31:
                    Debug.Log("Pozo");
                    Pozo();
                    break;
                case 42:
                    Debug.Log("Laberinto");
                    Laberinto();
                    break;
                case 56:
                    Debug.Log("Carcel");
                    Cárcel();
                    break;
                case 26 or 53:
                    Debug.Log("Dados");
                    Dados();
                    break;
                case 58:
                    Debug.Log("Calavera");
                    Calavera();
                    break;
                case 63:
                    Debug.Log("Final");
                    Final();
                    break;
                case > 63:
                    Debug.Log("Jardín");
                    Jardin();
                    break;
                default: break;
            }
         */
    }
}

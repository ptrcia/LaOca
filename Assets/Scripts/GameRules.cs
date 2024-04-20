using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameRules : MonoBehaviour
{
    GameManager gameManager;
    TurnManager turnManager;

    int firstBridge = 5;
    int secondBridge = 11;

    int firstDice = 25;
    int secondDice = 51;
    int finalCell = 62;

    [Header("Well Rule")]
    PlayerMovement playerInWell = null;
    int playerInWellRemainingTurns = 0;

    private void CheckGameObjects()
    {
        gameManager = GameObject.
            FindGameObjectWithTag("GameManager").
            GetComponent<GameManager>();
        turnManager = GameObject.FindGameObjectWithTag("TurnManager").
            GetComponent<TurnManager>();
    }

    public void CheckSpecialCell(PlayerMovement _playerMovement, GameObject player)
    {
        Debug.Log("CURRENT CELL PLAYER   " + _playerMovement.currentCell);

        CheckGameObjects();

        switch (_playerMovement.currentCell)

        {
            case 4 or 8 or 13 or 17 or 22 or 26 or 31 or 35 or
               40 or 44 or 49 or 53 or 58:
                Debug.Log("Oca");
                Goose();
                break;
            case 5 or 11:
                Debug.Log("Puente");
                Bridge();
                break;
            case 18:
                Debug.Log("Posada");
                Inn();
                break;
            case 30:
                Debug.Log("Pozo");
                Well();
                break;
            case 41:
                Debug.Log("Laberinto");
                Labyrinth();
                break;
            case 25 or 52:
                Debug.Log("Dados");
                Dices();
                break;
            case 55:
                Debug.Log("Carcel");
                Jail();
                break;
            case 57:
                Debug.Log("Calavera");
                Death();
                break;
            case 62:
                Debug.Log("Final");
                Final();
                break;
            case > 62:
                Debug.Log("Jardín");
                Garden();
                break;
            default: break;
        }
        void Goose()
        {
            Dictionary<int, int> cellOcaTransitions = new Dictionary<int, int>()
            {
                { 4, 8 },
                { 8, 13 },
                { 13, 17 },
                { 17, 22 },
                { 22, 26 },
                { 26, 31 },
                { 31, 35 },
                { 35, 40 },
                { 40, 44 },
                { 48, 57 }
            };

            if (cellOcaTransitions.ContainsKey(_playerMovement.currentCell))
                {
                    int destinationCell = cellOcaTransitions[_playerMovement.currentCell];

                    _playerMovement.currentCell = destinationCell;
                    player.transform.position = CellManager.instance.cells[destinationCell].position;
                    turnManager.nextTurnPlayer = false;
            }
            else if (_playerMovement.currentCell == 57)
            {
                gameManager.Win();
            }
        }
        void Bridge()
        {
            if (_playerMovement.currentCell == firstBridge)
            {
                _playerMovement.currentCell = secondBridge;
                player.transform.position = CellManager.instance.cells[_playerMovement.currentCell].position;
                turnManager.nextTurnPlayer = false;

            }
            else if (_playerMovement.currentCell == secondBridge)
            {
                _playerMovement.currentCell = firstBridge;
                player.transform.position = CellManager.instance.cells[_playerMovement.currentCell].position;
                turnManager.nextTurnPlayer = false;
            }
        }
        void Inn()
        {
            _playerMovement.noPlayableTurns++;
        }
        void Well()
        {
            // Sum up 3 no playable turns
            _playerMovement.noPlayableTurns = _playerMovement.noPlayableTurns + 3;
            playerInWellRemainingTurns = 3;

            // Check if there is anyone in the Well
            if (playerInWell != null)
            {
                // Set free whoever was in the well
                playerInWell.noPlayableTurns = 0;
                Debug.Log("EL PLAYER " + playerInWell.playerID + " ESTA LIBRE DEL POZO GRACIAS A " + _playerMovement.playerID);
            }

            playerInWell = _playerMovement;
            Debug.Log("EL PLAYER " + playerInWell.playerID + " QUEDA ATASCADO EN EL POZO DURANTE " + _playerMovement.noPlayableTurns + " TURNOS");
            
        }
        void Labyrinth()
        {
            _playerMovement.currentCell = 30;
            CellManager.instance.transform.position = CellManager.instance.cells[_playerMovement.currentCell].position;

        }
        void Jail()
        {
            _playerMovement.noPlayableTurns = _playerMovement.noPlayableTurns + 2;
        }
        void Dices()
        {
            if (_playerMovement.currentCell == firstDice)
            {
                _playerMovement.currentCell = secondDice;
                player.transform.position = CellManager.instance.cells[_playerMovement.currentCell].position;

            }
            else if (_playerMovement.currentCell == secondDice)
            {
                _playerMovement.currentCell = firstDice;
                player.transform.position = CellManager.instance.cells[_playerMovement.currentCell].position;

            }
            turnManager.nextTurnPlayer = false;
        }
        void Death()
        {
            _playerMovement.currentCell = 1;
        }
        void Final()
        {
            gameManager.Win();
        }
        void Garden()
        {
            //it does not work
            int difference = 0;
            Debug.Log("El jugador se mueve a la casilla " + _playerMovement.currentCell);

            difference = (_playerMovement.currentCell - finalCell);

            Debug.Log("El valor necesario para entrar en el Jardín desde la casilla " + _playerMovement.currentCell + " es: " + difference);

            player.transform.position = CellManager.instance.cells[_playerMovement.currentCell - difference].position;

            Debug.Log("El jugador se mueve a la casilla " + (_playerMovement.currentCell - difference));

            _playerMovement.currentCell -= difference;

            CheckSpecialCell(_playerMovement, CellManager.instance.gameObject);
        }
    }
    public void CheckWhosTurn(PlayerMovement player)
    {
        // Check if the actual turn is from the player in the well
        //Debug.Log(">>>>>>> TURN OF  " + player.playerID);
        if(playerInWell != null)
        {
            //Debug.Log("player.playerID: " + player.playerID + "playerInWell.playerID: " + playerInWell.playerID);
            if (player.playerID == playerInWell.playerID)
            {
                //Debug.Log("PLAYER IN WELL " + player.playerID + " HAS " + playerInWellRemainingTurns + "TURNS LEFT IN THE WELL");
                if (playerInWellRemainingTurns > 0)
                {
                    playerInWellRemainingTurns--;
                }
                else
                {
                    //Debug.Log("PLAYER IN WELL " + player.playerID + " DOES NOT HAVE TURNLS LEFT IN THE WELL, TAKE IT OUT");
                    playerInWell = null;
                }
            }
        }
        
    }
}

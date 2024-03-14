using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRules : MonoBehaviour
{   
    GameManager gameManager;
    TurnManager turnManager;

    int firstBridge = 5;
    int secondBridge = 11;

    int firstDice = 25;
    int secondDice = 51;

    int finalCell = 62;

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
        Debug.Log("CuRRENT CELL PLAYER   " + _playerMovement.currentCell);

        CheckGameObjects();

        switch(_playerMovement.currentCell)

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
            case 41:
                Debug.Log("Laberinto");
                Laberinto();
                break;
            case 25 or 52:
                Debug.Log("Dados");
                Dados();
                break;
            case 57:
                Debug.Log("Calavera");
                Calavera();
                break;
            case 62:
                Debug.Log("Final");
                Final();
                break;
            case > 62:
                Debug.Log("Jardín");
                Jardin();
                break;
            default: break;
        }
        void Oca()
        {            
            if (_playerMovement.currentCell != 57)
            {
                //Debug.Log("curretnBoardCell" + _playerMovement.currentCell);

                if (_playerMovement.currentCell == 4)
                {
                    player.transform.position = _playerMovement.cells[8].position;
                }
                else if (_playerMovement.currentCell == 8)
                {
                    player.transform.position = _playerMovement.cells[13].position;
                }
                else if (_playerMovement.currentCell == 13)
                {
                    player.transform.position = _playerMovement.cells[17].position;
                }
                else if (_playerMovement.currentCell == 17)
                {
                    player.transform.position = _playerMovement.cells[22].position;
                }
                else if (_playerMovement.currentCell == 22)
                {
                    player.transform.position = _playerMovement.cells[26].position;
                }
                else if (_playerMovement.currentCell == 26)
                {
                    player.transform.position = _playerMovement.cells[31].position;
                }
                else if (_playerMovement.currentCell == 31)
                {
                    player.transform.position = _playerMovement.cells[35].position;
                }
                else if (_playerMovement.currentCell == 35)
                {
                    player.transform.position = _playerMovement.cells[40].position;
                }
                else if (_playerMovement.currentCell == 40)
                {
                    player.transform.position = _playerMovement.cells[44].position;
                }
                else if (_playerMovement.currentCell == 48)
                {
                    player.transform.position = _playerMovement.cells[57].position;
                }

                turnManager.nextTurnPlayer = false;
            }
            else if (_playerMovement.currentCell == 57)
            {
                gameManager.Win();
            }
        }
        void Puente()
        {
            if (_playerMovement.currentCell == firstBridge)
            {
                _playerMovement.currentCell = secondBridge;
                player.transform.position = _playerMovement.cells[_playerMovement.currentCell].position;
                turnManager.nextTurnPlayer = false;

            }
            else if (_playerMovement.currentCell == secondBridge)
            {
                _playerMovement.currentCell = firstBridge;
                player.transform.position = _playerMovement.cells[_playerMovement.currentCell].position;
                turnManager.nextTurnPlayer = false;

            }
        }
        void Posada()
        {
            _playerMovement.noPlayableTurns ++;
        }
        void Pozo()
        {
            /*
            while (player2.currentCell!=31 ||player3.currentCell ...)
            {
                _playerMovement.noPlayableTurns ++;
            }
            */
        }
        void Laberinto()
        {
            _playerMovement.currentCell = 30;
            _playerMovement.transform.position = _playerMovement.cells[_playerMovement.currentCell].position;

        }
        void Cárcel()
        {
            /*
             while(!player2.layerMovement.currentCell != 50 || ...)
            {
                _playerMovement.noPlayableTurns ++;
            }
             * */
        }
        void Dados()
        {
            if (_playerMovement.currentCell == firstDice)
            {
                _playerMovement.currentCell = secondDice;
                player.transform.position = _playerMovement.cells[_playerMovement.currentCell].position;

            }
            else if (_playerMovement.currentCell == secondDice)
            {
                _playerMovement.currentCell = firstDice;
                player.transform.position = _playerMovement.cells[_playerMovement.currentCell].position;

            }
        }
        void Calavera()
        {
            _playerMovement.currentCell = 1;
        }
        void Final()
        {
            gameManager.Win();
        }
        void Jardin()
        {
            _playerMovement.currentCell = _playerMovement.currentCell -
                            (_playerMovement.currentCell - finalCell);
            player.transform.position = _playerMovement.cells[_playerMovement.currentCell].position;

            CheckSpecialCell(_playerMovement, _playerMovement.gameObject);
        }


        /*
                case 30:
                    Debug.Log("Pozo");
                    Pozo();
                    break;

                case 55:
                    Debug.Log("Carcel");
                    Cárcel();
                    break;
         */
    }
}

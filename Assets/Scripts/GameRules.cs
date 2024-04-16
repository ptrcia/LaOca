using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameRules : MonoBehaviour
{
    GameManager gameManager;
    TurnManager turnManager;
    CellContainer cellContainer;

    int firstBridge = 5;
    int secondBridge = 11;

    int firstDice = 25;
    int secondDice = 51;

    int finalCell = 62;

    int lastCountWell = 0;

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

        switch (_playerMovement.currentCell)

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
            case 30:
                Debug.Log("Pozo");
                //Pozo();
                break;
            case 41:
                Debug.Log("Laberinto");
                Laberinto();
                break;
            case 25 or 52:
                Debug.Log("Dados");
                Dados();
                break;
            case 55:
                Debug.Log("Carcel");
                Cárcel();
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
        void Puente()
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
        void Posada()
        {
            _playerMovement.noPlayableTurns++;
        }
        void Pozo()
        {
            //aqui quiero coger la referencia de la celda con la que estoy colisionando
            //while(_playerMovement?)
            while(cellContainer.playersRegistry.Count <= lastCountWell)
            {
                _playerMovement.noPlayableTurns++;

            }
                Debug.Log("La lista ha aumentado");
                lastCountWell = cellContainer.playersRegistry.Count;
        }
        void Laberinto()
        {
            _playerMovement.currentCell = 30;
            CellManager.instance.transform.position = CellManager.instance.cells[_playerMovement.currentCell].position;

        }
        void Cárcel()
        {
            _playerMovement.noPlayableTurns = _playerMovement.noPlayableTurns + 2;
        }
        void Dados()
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
            //no funciona
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

}

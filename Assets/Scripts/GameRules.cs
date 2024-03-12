using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRules : MonoBehaviour
{
    int playerCurrentCell;
    PlayerMovement playerMovement;

    int firstBridge = 6;
    int secondBridge = 12;

    int firstDice = 26;
    int secondDice = 53;

    int finalCell = 63;

    private void Awake()
    {
        playerMovement = GameObject.
            FindGameObjectWithTag("Player").
            GetComponent<PlayerMovement>();
        playerCurrentCell = playerMovement.currentCell;

    }
    public void CheckSpecialCell()
    {
        switch (playerCurrentCell)
        {
            case 5 or 9 or 14 or 18 or 23 or 27 or 32 or 36 or
           41 or 45 or 50 or 54 or 59:
                Debug.Log("Oca");
                Oca();
                break;
            case 6 or 12:
                Debug.Log("Puente");
                Puente();
                break;
            case 19:
                Debug.Log("POsada");
                Posada();
                break;
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
    }
    void Oca()
    {
        if (playerCurrentCell != 59)
        {
            playerCurrentCell = playerCurrentCell + 5;
            //Repetir Turno /Volver a tirar el dado
        }
        else if (playerCurrentCell == 59)
        {
            //gameManager.Win();

        }
    }
    void Puente()
    {
        if (playerCurrentCell == firstBridge)
        {
            playerCurrentCell = secondBridge;
        }
        else if (playerCurrentCell == secondBridge)
        {
            playerCurrentCell = firstBridge;
        }
    }
    void Posada()
    {
        //turno.SaltarTurno();
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
        playerCurrentCell = 30;
    }
    void Cárcel()
    {
        /*
         while(!player2.currentPosition != 56 || ...)
        {
        turno.SaltarTurno();
        }
         * */
    }
    void Dados()
    {
        if (playerCurrentCell == firstDice)
        {
            playerCurrentCell = secondDice;
        }
        else if (playerCurrentCell == secondDice)
        {
            playerCurrentCell = firstDice;
        }
    }
    void Calavera()
    {
        playerCurrentCell = 1;
    }
    void Final()
    {
        //gameManager.Win();
    }
    void Jardin()
    {
        playerCurrentCell = playerCurrentCell -
                        (playerCurrentCell - finalCell);
        CheckSpecialCell();
    }
}

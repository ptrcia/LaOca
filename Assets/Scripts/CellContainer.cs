using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class CellContainer : MonoBehaviour
{
    public int currentPlayersInCell;
    public List<string> playersInCell = new List<string>();
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("COLISION==========");
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Players in cell: " + currentPlayersInCell);
            currentPlayersInCell++;
            playersInCell.Add(collision.gameObject.GetComponent<PlayerMovement>().playerID);
            //Debug.Log("List of players in cell: " +  playersInCell);
            Debug.Log("List of players in cell: " + string.Join(", ", playersInCell)); // Convertimos la lista a una cadena para imprimir

        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Players in cell: " + currentPlayersInCell);
            currentPlayersInCell--;
            playersInCell.Remove(collision.gameObject.GetComponent<PlayerMovement>().playerID); // Usamos Remove para eliminar el jugador de la lista
            //Debug.Log("List of players in cell: " + playersInCell);
            Debug.Log("List of players in cell: " + string.Join(", ", playersInCell));
        }
    }
}

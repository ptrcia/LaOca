using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class CellContainer : MonoBehaviour
{
    public int currentPlayersInCell;
    public List<string> playersInCell = new List<string>();
    public List<GameObject> playersObjectsInCell = new List<GameObject>();

    PlayerMovement playerMovementCtrl;

    private void OnCollisionEnter(Collision collision)
    {
        playerMovementCtrl = collision.gameObject.GetComponent<PlayerMovement>();

        if (collision.gameObject.CompareTag("Player"))
        {
            currentPlayersInCell++;
            playersObjectsInCell.Add(collision.gameObject);
            //playersObjectsInCell.Add(playerMovementCtrl.playerID);
            Debug.Log("Enter List  of the "+currentPlayersInCell+" players in " + gameObject.name + ": " + string.Join(", ", playersInCell));
            playerMovementCtrl.CellArragement(currentPlayersInCell, playersObjectsInCell);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            currentPlayersInCell--;
            playersObjectsInCell.Remove(collision.gameObject); 
            Debug.Log("Exit List of the " + currentPlayersInCell + " players in " + gameObject.name + ": " + string.Join(", ", playersInCell));
            playerMovementCtrl.CellArragement(currentPlayersInCell, playersObjectsInCell);
        }
    }
}

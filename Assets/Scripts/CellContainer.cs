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

        if (collision.gameObject.CompareTag("Player") &&  collision.gameObject != null)
        {
            currentPlayersInCell++;
            playersObjectsInCell.Add(collision.gameObject);
            Debug.Log("Enter List  of the "+currentPlayersInCell+" players in " + gameObject.name + ": " + string.Join(", ", playersInCell));
            playerMovementCtrl.CellArragement(currentPlayersInCell, playersObjectsInCell);
        }
        else if(collision.gameObject != null)
        {
            Debug.Log("Check this in the future");
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        //Debug.Log("THE PLAYER " + (collision.gameObject.GetComponent<PlayerMovement>().playerID).ToString() + " OUT FROM THE CELL");
        if (collision.gameObject.CompareTag("Player") && collision.gameObject !=null)
        {
            currentPlayersInCell--;
            if (currentPlayersInCell > 0)
            {
                playersObjectsInCell.Remove(collision.gameObject);
                Debug.Log("Exit List of the " + currentPlayersInCell + " players in " + gameObject.name + ": " + string.Join(", ", playersInCell));
                playerMovementCtrl.CellArragement(currentPlayersInCell, playersObjectsInCell);
                //Debug.Log("OUT");
            }
            else
            {
                //Debug.Log("NO MORE PLAYERS TO ARRANGE");
            }
            
        }
        else if (collision.gameObject == null)
        {
            Debug.Log("Null: collision.gameObject in OnCollisionExit.");
        }
    }
}

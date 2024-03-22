using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CellContainer : MonoBehaviour
{
    public int currentPlayersInCell;

    private void OnCollisionEnter(Collision collision)
    {
        if (gameObject.CompareTag("Player"))
        {
            Debug.Log("Players in cell: " + currentPlayersInCell);
            currentPlayersInCell++;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (gameObject.CompareTag("Player"))
        {
            Debug.Log("Players in cell: " + currentPlayersInCell);
            currentPlayersInCell--;
        }
    }
}

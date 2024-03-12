using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    #region
    int randomNumber;
    public bool diceRolled;

    public void OnMouseDown()
    {
        //Debug.Log("Rolled: "+ diceRolled);
        RollDice();
        Debug.Log("Random generated number: " + randomNumber);
        diceRolled = true;

    }
    private void OnMouseUp()
    {
        diceRolled = false;
    }


    public int RollDice()
    {
        randomNumber = Random.Range(1, 6);
        return randomNumber;
    }
    #endregion
}

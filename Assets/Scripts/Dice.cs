using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    #region
    int randomNumber;
    public bool diceRolled= true;
    public bool canRollDice;

    public int RollDice()
    {
        randomNumber = Random.Range(1, 6);
        return randomNumber;
    }
    private void OnMouseDown()
    {
        if (canRollDice)
        {
            //Debug.Log("Rolled: "+ diceRolled);
            RollDice();
            Debug.Log("Random generated number: " + randomNumber);
            diceRolled = true;
        }

    }
    private void OnMouseUp()
    {
        diceRolled = false;
    }

    public void DisableRolling() 
    {
        canRollDice = false;
    }
    public void EnableRolling()
    {
        canRollDice = true;
    }
    #endregion

    
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dice : MonoBehaviour
{
    #region
    //dado numero
    [SerializeField] TextMeshProUGUI diceText;

    int randomNumber;
    public bool diceRolled = true;
    public bool canRollDice;

    public int RollDice()
    {
        randomNumber = Random.Range(1, 6);
        //randomNumber = 63;
        return randomNumber;
    }
    private void OnMouseDown()
    {
        if (canRollDice)
        {
            RollDice();
            ToString();
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


    /*void ToString()
    {
        diceText.text = randomNumber.ToString();
    }*/
    #endregion

    
}

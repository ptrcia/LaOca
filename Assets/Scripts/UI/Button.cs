using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class Button : MonoBehaviour
{
    [SerializeField] GameObject rulesPanel;

    private void OnButtonClick()
    {
        if(rulesPanel != null && rulesPanel.activeInHierarchy)
        {
            rulesPanel.SetActive(false);
        }
        else if(rulesPanel != null && !rulesPanel.activeInHierarchy)
        {
            rulesPanel.SetActive(true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
//using UnityEngine.UI;

public class Button : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] GameObject specificRule;
    GameManagerUI gameManagerUI;

    private void Awake()
    {
        gameManagerUI = GameObject.FindGameObjectWithTag("GameManagerUI")
            .GetComponent<GameManagerUI>();
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        if (gameManagerUI.isOpen == true)
        {
            Debug.Log("entered?");
            specificRule.SetActive(true);
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (gameManagerUI.isOpen == true)
        {
            specificRule.SetActive(false);
        }
    }
}

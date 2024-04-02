using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManagerUI : MonoBehaviour
{
    public RectTransform diceImage;
    [SerializeField] GameObject startButton;
    [SerializeField] GameObject newRoundPanel;
    //[SerializeField] private GameObject currentPlayerTurn;
    [SerializeField] Vector3 targetPosition;
    RectTransform initialRectTransform;
    RectTransform roundTransform;
    Vector2 screenSizeZero;

    

    private void Awake()
    {
        initialRectTransform = startButton.GetComponent<RectTransform>();
        roundTransform = newRoundPanel.GetComponent<RectTransform>();
        startButton.SetActive(true);
        ClearScreenButton();
    }
    private void Start()
    {
        screenSizeZero = new Vector2(0, 0);
        //StartAnimatingRound();
    }
    public void AnimatingDiceImage()
    {
        diceImage.DOScale(new Vector3(0.5f, 0.5f, diceImage.localScale.z), 1f).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            diceImage.DOScale(Vector3.zero, 0.5f).SetEase(Ease.OutQuad);
        });
    }
    public void StartAnimatingRound()
    {
        StartCoroutine(AnimateRound());
    }
    IEnumerator AnimateRound()
    {
        newRoundPanel.SetActive(true);       
        roundTransform.DOLocalMove(targetPosition, 1f).SetEase(Ease.OutBounce);
        yield return new WaitForSeconds(2);
        roundTransform.DOLocalMove(new Vector3(0, -800, 0), 1f).SetEase(Ease.OutBounce);
        yield return new WaitForSeconds(2);
        newRoundPanel.SetActive(false);
    }
    public void CurrentTurn()
    {
        //no se mu ybien como hacerlo
        //argumentos?
    }

    private void ClearScreenButton()
    {
        initialRectTransform.DOSizeDelta(screenSizeZero, 2f);
    }

}

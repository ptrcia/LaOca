using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManagerUI : MonoBehaviour
{
    [Header("Dice")]
    public RectTransform diceImage;
    Dice dice;

    [Header("Rules Button")]
    [SerializeField] RectTransform rulesButton;
    RectTransform rulesButtonOriginalTransform;
    float originalLeft;
    public bool isOpen = false;

    [Header("Other")]
    TurnManager turnManager;
    [SerializeField] GameObject startButton;
    [SerializeField] GameObject newRoundPanel;
    [SerializeField] Vector3 targetPosition;
    RectTransform initialRectTransform;
    RectTransform roundTransform;
    Vector2 screenSizeZero;
    public float duration = 1f;

    private void Awake()
    {
        dice = GameObject.FindGameObjectWithTag("Dice").
            GetComponent<Dice>();
        turnManager = GameObject.FindGameObjectWithTag("TurnManager").
            GetComponent<TurnManager>();

        initialRectTransform = startButton.GetComponent<RectTransform>();
        roundTransform = newRoundPanel.GetComponent<RectTransform>();
        startButton.SetActive(true);
        ClearScreenButton();
    }
    private void Start()
    {
        originalLeft = rulesButton.anchoredPosition.x;
        rulesButtonOriginalTransform = rulesButton;
        screenSizeZero = new Vector2(0, 0);
    }

    public void AnimatingDiceImage()
    {
        diceImage.DOScale(new Vector3(0.5f, 0.5f, diceImage.localScale.z), 1f)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
        {
            diceImage.DOScale(Vector3.zero, duration/2)
            .SetEase(Ease.OutQuad);
        });
    }
    public void StartAnimatingRound()
    {
        StartCoroutine(nameof(AnimateRound));
    }
    IEnumerator AnimateRound()
    {
        dice.canRollDice = false;
        newRoundPanel.SetActive(true);       
        roundTransform.DOLocalMove(targetPosition, duration)
            .SetEase(Ease.OutBounce);
        yield return new WaitForSeconds(2);
        roundTransform.DOLocalMove(new Vector3(0, -800, 0), duration)
            .SetEase(Ease.OutBounce);
        yield return new WaitForSeconds(1);
        newRoundPanel.SetActive(false);
        dice.canRollDice = true;
    }
    
    public void CurrentTurnAnimation(RectTransform currentButton) 
    {
        currentButton.DOAnchorPos
            (new Vector2(-10, currentButton.anchoredPosition.y), duration)
        .SetEase(Ease.InBounce);
    }
    public void CurrentTurnAnimationClose(RectTransform currentButton)
    {
        currentButton.DOAnchorPos
            (new Vector2(94.4f, currentButton.anchoredPosition.y), duration)
            .SetEase(Ease.InBounce);
    }

    public void RulesButton()
    {
        if (rulesButtonOriginalTransform.anchoredPosition.x == originalLeft)
        {
            isOpen= true;
            rulesButton.DOAnchorPos(new Vector2(-115, rulesButtonOriginalTransform.anchoredPosition.y), duration).SetEase(Ease.InBounce);
        }
        else
        {
            isOpen = false;
            rulesButton.DOAnchorPos(new Vector2(originalLeft, rulesButtonOriginalTransform.anchoredPosition.y), duration).SetEase(Ease.InBounce);
        }  
    }

    private void ClearScreenButton()
    {
        initialRectTransform.DOSizeDelta(screenSizeZero, 2f);
    }

}
    

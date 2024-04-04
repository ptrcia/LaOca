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
    public float duration = 1f;

    //List<GameObject> cloneButtonPrefabAnimation = new List<GameObject>();
    GameObject cloneButtonPrefabAnimation;
    Vector3 cloneButtonPrefabAnimationOriginalScale;
    TurnManager turnManager;

    private void Awake()
    {
        
        turnManager = GameObject.FindGameObjectWithTag("TurnManager").
            GetComponent<TurnManager>();
        cloneButtonPrefabAnimation = turnManager.cloneButtonPrefab; //coge el ultimo

        initialRectTransform = startButton.GetComponent<RectTransform>();
        roundTransform = newRoundPanel.GetComponent<RectTransform>();
        startButton.SetActive(true);
        ClearScreenButton();
    }
    private void Start()
    {
        cloneButtonPrefabAnimationOriginalScale = cloneButtonPrefabAnimation.transform.localScale;
        screenSizeZero = new Vector2(0, 0);
    }
    private void Update()
    {
        CurrentTurnAnimation(turnManager.currentPlayer);
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
        newRoundPanel.SetActive(true);       
        roundTransform.DOLocalMove(targetPosition, duration)
            .SetEase(Ease.OutBounce);
        yield return new WaitForSeconds(2);
        roundTransform.DOLocalMove(new Vector3(0, -800, 0), duration)
            .SetEase(Ease.OutBounce);
        yield return new WaitForSeconds(2);
        newRoundPanel.SetActive(false);
    }

    public void CurrentTurnAnimation(GameObject currentPlayer) //EN PROCESO
    {
        //Debug.Log(cloneButtonPrefabAnimation.GetComponentInChildren<TextMeshProUGUI>().text + currentPlayer.GetComponent<PlayerMovement>().playerID);
        if (cloneButtonPrefabAnimation.GetComponentInChildren<TextMeshProUGUI>().text == currentPlayer.GetComponent<PlayerMovement>().playerID)
        {
            cloneButtonPrefabAnimation.transform.DOScale(new Vector3(2, 1, 0), duration)
            .SetEase(Ease.InElastic)
            .OnComplete(() =>
            {
                cloneButtonPrefabAnimation.transform.DOScale(cloneButtonPrefabAnimationOriginalScale, duration)
                    .SetEase(Ease.InElastic);
            });
        }
    }

    private void ClearScreenButton()
    {
        initialRectTransform.DOSizeDelta(screenSizeZero, 2f);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PanelAnimation : MonoBehaviour
{
    [SerializeField] private RectTransform panelTransform;
    [SerializeField] private Vector3 initialPosition;
    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private float animationDuration = 1f;

    private void Start()
    {
        panelTransform.localPosition = initialPosition;
    }

    public void AnimatePanel()
    {
        DOTween.Init();

        gameObject.SetActive(true);

        panelTransform.DOLocalMove(targetPosition, animationDuration).SetEase(Ease.OutBounce);
    }
}
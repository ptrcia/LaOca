using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public RectTransform diceImage;

    public void Quit()
    {
        Debug.Log("Quitting...");
        Application.Quit();
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void LoadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void AnimatingDiceImage()
    {
        diceImage.DOScale(new Vector3(0.5f, 0.5f, diceImage.localScale.z), 1f).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            diceImage.DOScale(Vector3.zero, 0.5f).SetEase(Ease.OutQuad);
        });
    }
}

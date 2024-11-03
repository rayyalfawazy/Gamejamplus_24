using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PuzzleUI : MonoBehaviour
{
    [Header("Panel UI")]
    [SerializeField] private Transform gameOverUI;
    [SerializeField] private Transform gamePauseUI;
    [SerializeField] private TMP_Text winCount;
    [SerializeField] private TMP_Text bestCount;

    [Header("Button UI")]
    [SerializeField] private Button pasueButton;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button restartButton;

    private void Start()
    {
        pasueButton.onClick.AddListener(ShowPauseUI);
        resumeButton.onClick.AddListener(HidePauseUI);
        restartButton.onClick.AddListener(RestartGame);
    }

    public void ShowGameOverUI()
    {
        gameOverUI.gameObject.SetActive(true);
        gameOverUI.localScale = Vector3.zero;
        gameOverUI.DOScale(Vector3.one, 0.25f);
    }

    public void ShowPauseUI()
    {
        gamePauseUI.gameObject.SetActive(true);
        gamePauseUI.localScale = Vector3.zero;
        gamePauseUI.DOScale(Vector3.one, 0.25f);
    }

    public void HidePauseUI()
    {
        gamePauseUI.localScale = Vector3.one;
        gamePauseUI.DOScale(Vector3.zero, 0.25f).OnComplete(() => { gamePauseUI.gameObject.SetActive(false); });
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void AddWinCount(int value)
    {
        winCount.text = $"Win Count: {value}";
    }

    public void AddBestWinCount(int value)
    {
        bestCount.text = $"Best Win: {value}";
    }
}

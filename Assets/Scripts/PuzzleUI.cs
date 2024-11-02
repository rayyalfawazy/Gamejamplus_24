using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleUI : MonoBehaviour
{
    [Header("Panel UI")]
    [SerializeField] private Transform gameOverUI;

    public void ShowGameOverUI()
    {
        gameOverUI.gameObject.SetActive(true);
    }
}

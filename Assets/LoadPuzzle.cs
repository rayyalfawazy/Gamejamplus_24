using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadPuzzle : MonoBehaviour
{
    [Header("Puzzle UI")]
    [SerializeField] private GridLayoutGroup puzzlePanel;
    [SerializeField] private PuzzleButton puzzleButton;

    [Header("Puzzle Settings")]
    [SerializeField] private int puzzleAmount;

    private void Awake()
    {
        puzzlePanel.spacing = new Vector2(20,20);
        if (puzzleAmount % 2 == 0 )
        {
            for (int i = 0; i < puzzleAmount; i++)
            {
                PuzzleButton button = Instantiate(puzzleButton, puzzlePanel.transform);
                button.name = "PuzzleButton" + i.ToString();
            }
        }
        else
        {
            Debug.LogError("Puzzle Amount must an EVEN value");
        }

    }
}

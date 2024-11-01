using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadPuzzle : MonoBehaviour
{
    [SerializeField] private GridLayoutGroup puzzlePanel;
    [SerializeField] private PuzzleButton puzzleButton;

    private void Awake()
    {
        puzzlePanel.spacing = new Vector2(20,20);
        for (int i = 0; i < 8; i++)
        {
            PuzzleButton button = Instantiate(puzzleButton,puzzlePanel.transform);
            button.name = "PuzzleButton" + i.ToString();
        }
    }
}

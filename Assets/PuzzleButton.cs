using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleButton : MonoBehaviour
{
    private int puzzleId;
    public int PuzzleID {  get { return puzzleId; } set { puzzleId = value; } }

    public Sprite hiddenSprite;
    public Sprite defaultSprite;

    private void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(HandleButtonClick);
    }

    private void HandleButtonClick()
    {
        FindAnyObjectByType<PuzzleManager>().SelectPuzzle(this);
    }
}

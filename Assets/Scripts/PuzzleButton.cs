using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleButton : MonoBehaviour
{
    private int puzzleId;
    private bool isCorrectMatch;

    public int PuzzleID {  get { return puzzleId; } set { puzzleId = value; } }
    public bool IsCorrectMatch { get { return isCorrectMatch; } set { isCorrectMatch = value; } }

    public Sprite hiddenSprite;
    public Sprite defaultSprite;

    private Animator animator;

    private void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(HandleButtonClick);

        animator = GetComponent<Animator>();
    }

    private void HandleButtonClick()
    {
        FindAnyObjectByType<PuzzleManager>().SelectPuzzle(this);
    }

    public void FlipAnimation()
    {
        animator.Play("FlipAnimation");
    }

    public void BackflipAnimation()
    {
        animator.Play("BackflipAnimation");
    }
} 

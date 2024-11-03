using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleButton : MonoBehaviour
{
    private int puzzleId;
    private bool isCorrectMatch;
    private bool isOpen;

    public int PuzzleID {  get { return puzzleId; } set { puzzleId = value; } }
    public bool IsCorrectMatch { get { return isCorrectMatch; } set { isCorrectMatch = value; } }
    public bool IsOpen { get { return isOpen; } } 

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

    public void OpenAnimation()
    {
        animator.Play("FlipAnimation");
        transform.DOScale(new Vector2(1.15f, 1.15f), 0.75f)
            .OnComplete(() =>
            {
                transform.DOScale(Vector2.one, 0.25f);
                transform.GetChild(0).gameObject.SetActive(true);
            })
            .SetEase(Ease.InOutQuad);
    }

    public void CloseAnimation()
    {
        animator.Play("BackflipAnimation");
        transform.GetChild(0).gameObject.SetActive(false);
        transform.DOScale(new Vector2(1.15f, 1.15f), 0.75f)
            .OnComplete(() =>
            {
                transform.DOScale(Vector2.one, 0.25f);
            })
            .SetEase(Ease.InOutQuad);
    }

}

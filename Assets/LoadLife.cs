using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadLife : MonoBehaviour
{
    [Header("Life UI")]
    [SerializeField] private HorizontalLayoutGroup lifePanel;
    [SerializeField] private Image lifeImage;

    [Header("Life Settings")]
    [SerializeField] private int lifeAmount;

    private void Awake()
    {
        for (int i = 0; i < lifeAmount; i++)
        {
            Image button = Instantiate(lifeImage, lifePanel.transform);
            button.name = "PuzzleButton" + i.ToString();
        }
    }
}

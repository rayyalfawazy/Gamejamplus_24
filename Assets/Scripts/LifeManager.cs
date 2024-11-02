using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour
{
    [Header("Life UI")]
    [SerializeField] private HorizontalLayoutGroup lifePanel;
    [SerializeField] private Image lifeImage;

    private List<Image> lifeIcons = new List<Image>();
    private bool isDead = false;

    public bool IsDead {  get { return isDead; } }

    public void SetupLives(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Image lifeIcon = Instantiate(lifeImage, lifePanel.transform);
            lifeIcons.Add(lifeIcon);
            lifeIcon.name = "PuzzleButton" + i.ToString();
        }
    }

    public void LoseLife()
    {
        if (lifeIcons.Count > 0)
        {
            // Mengubah ikon saat nyawa habis
            Image lifeIcon = lifeIcons[^1];
            lifeIcons.Remove(lifeIcon);
            lifeIcon.color = Color.gray;

            if (lifeIcons.Count == 0)
            {
                isDead = true;
            }
        }
    }

    public void HalfLife()
    {
        if (lifeIcons.Count > 0)
        {
            Image lifeIcon = lifeIcons[^1];
            lifeIcon.color = new Color(0.51f, 0.26f, 0.26f, 1f);
        }
    }
}

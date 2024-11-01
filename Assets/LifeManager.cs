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
    public bool isDead = false;

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
            // Menghapus ikon nyawa terakhir
            Image lifeIcon = lifeIcons[lifeIcons.Count - 1];
            lifeIcons.Remove(lifeIcon);
            Destroy(lifeIcon);

            if (lifeIcons.Count == 0)
            {
                isDead = true;
            }
        }
    }
}

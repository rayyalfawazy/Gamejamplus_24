using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour
{
    [Header("Life UI")]
    [SerializeField] public HorizontalLayoutGroup lifePanel;
    [SerializeField] private Image lifeImage;
    [SerializeField] private Sprite[] lifeSprite;

    public List<Image> lifeIcons = new List<Image>();
    private bool isDead = false;

    public bool IsDead {  get { return isDead; } }

    public void SetupLives(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Image lifeIcon = Instantiate(lifeImage, lifePanel.transform);
            lifeIcons.Add(lifeIcon);
            lifeIcon.sprite = lifeSprite[0];
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
            lifeIcon.sprite = lifeSprite[2];

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
            lifeIcon.sprite = lifeSprite[1];
        }
    }
}

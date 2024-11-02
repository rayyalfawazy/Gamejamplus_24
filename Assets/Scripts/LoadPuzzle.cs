using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadPuzzle : MonoBehaviour
{
    [Header("Puzzle UI")]
    [SerializeField] private GridLayoutGroup puzzlePanel;
    [SerializeField] private PuzzleButton puzzleButton;
    [SerializeField] private List<Sprite> fillSprite;

    public List<PuzzleButton> puzzleButtonList = new List<PuzzleButton>();

    public void SetupPuzzle(int amount)
    {
        // Pastikan jumlah sprites sesuai
        if (fillSprite.Count < amount / 2)
        {
            Debug.LogError("Insufficient sprites for the puzzle pairs.");
            return;
        }

        // Cek bahwa amount bernilai genap
        if (amount % 2 != 0)
        {
            Debug.LogError("Puzzle Amount must be an EVEN value");
            return;
        }

        // Buat daftar ID puzzle dengan setiap ID muncul dua kali
        List<int> puzzleIDs = new List<int>();
        for (int i = 0; i < amount / 2; i++)
        {
            puzzleIDs.Add(i);
            puzzleIDs.Add(i);
        }

        // Acak urutan ID puzzle
        ShuffleList(puzzleIDs);

        // Atur layout dan buat puzzle button dengan ID dan sprite acak
        puzzlePanel.spacing = new Vector2(20, 20);
        for (int i = 0; i < amount; i++)
        {
            PuzzleButton button = Instantiate(puzzleButton, puzzlePanel.transform);
            button.PuzzleID = puzzleIDs[i];
            button.hiddenSprite = fillSprite[button.PuzzleID]; // Mengaitkan sprite berdasarkan PuzzleID
            button.name = "PuzzleButton" + " " + button.PuzzleID;
            puzzleButtonList.Add(button);
        }
    }

    // Fungsi untuk mengacak daftar (Shuffle)
    private void ShuffleList(List<int> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = Random.Range(i, list.Count);
            int temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}

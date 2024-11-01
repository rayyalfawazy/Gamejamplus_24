using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager instance;

    private LoadPuzzle loadPuzzle; // Referensi ke skrip LoadPuzzle
    private LifeManager lifeManager; // Referensi ke skrip LoadLife

    [Header("Puzzle Settings")]
    [SerializeField] private int puzzleAmount; // Jumlah puzzle untuk di-load
    [SerializeField] private int lifeAmount; // Jumlah nyawa untuk di-load
    [SerializeField] private float resetDuration; // Durasi Delay sebelum Reset (Saat Salah)

    private List<PuzzleButton> selectedPuzzles = new List<PuzzleButton>();

    // Singleton Pattern
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance);
        } else
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
    }

    private void Start()
    {
        // Mengambil komponen dalam satu objek
        lifeManager = GetComponent<LifeManager>();
        loadPuzzle = GetComponent<LoadPuzzle>();

        // Memulai game dengan memuat puzzle dan mengatur nyawa
        loadPuzzle.SetupPuzzle(puzzleAmount);
        lifeManager.SetupLives(lifeAmount);
    }

    public void SelectPuzzle(PuzzleButton pb)
    {
        if (selectedPuzzles.Contains(pb) || selectedPuzzles.Count >= 2)
            return;


        selectedPuzzles.Add(pb);
        pb.GetComponent<Image>().sprite = pb.hiddenSprite;

        // Memeriksa jika dua puzzle telah dipilih
        if (selectedPuzzles.Count == 2)
        {
            CheckMatch();
        }
    }

    private void CheckMatch()
    {
        if (selectedPuzzles[0].PuzzleID == selectedPuzzles[1].PuzzleID)
        {
            // Jika cocok, biarkan tetap terbuka
            selectedPuzzles.Clear();
            Debug.Log("Correct Match");
        }
        else
        {
            // Jika tidak cocok, kurangi nyawa
            lifeManager.LoseLife();
            StartCoroutine(ResetInDelay(resetDuration));
            if (lifeManager.isDead)
            {
                GameOver();
            }
            Debug.Log("Wrong Match");
        }
    }

    private IEnumerator ResetInDelay(float resetTime)
    {
        yield return new WaitForSeconds(resetTime);
        ResetPuzzles();
        selectedPuzzles.Clear();
    }

    private void ResetPuzzles()
    {
        foreach (PuzzleButton pb in selectedPuzzles)
        {
            pb.GetComponent<Image>().sprite = pb.defaultSprite; // Kembalikan sprite ke defaultSprite
        }
    }

    private void GameOver()
    {
        // Logika saat game over
        Debug.Log("Game Over! You have no lives left.");
    }
}

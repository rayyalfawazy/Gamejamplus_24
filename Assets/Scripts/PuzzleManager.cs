using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager instance;

    private LoadPuzzle loadPuzzle; // Referensi ke skrip LoadPuzzle
    private LifeManager lifeManager; // Referensi ke skrip LoadLife
    private PuzzleUI puzzleUI; // Referensi ke skrip UI Puzzle

    [Header("Puzzle Settings")]
    [SerializeField] private int puzzleAmount; // Jumlah puzzle untuk di-load
    [SerializeField] private int lifeAmount; // Jumlah nyawa untuk di-load
    [SerializeField] private float resetDuration; // Durasi Delay sebelum Reset (Saat Salah)

    [Header("Debug Variable")]
    [SerializeField] private int attemptCount;
    [SerializeField] private int wrongAttemptStreak = 0;

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

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void Start()
    {
        // Mengambil komponen dalam satu objek
        lifeManager = GetComponent<LifeManager>();
        loadPuzzle = GetComponent<LoadPuzzle>();
        puzzleUI = GetComponent<PuzzleUI>();

        InitialStart();
    }

    private void InitialStart()
    {
        // Memulai game dengan memuat puzzle dan mengatur nyawa
        loadPuzzle.SetupPuzzle(puzzleAmount);
        lifeManager.SetupLives(lifeAmount);
    }

    public void SelectPuzzle(PuzzleButton pb)
    {
        if (selectedPuzzles.Contains(pb) || selectedPuzzles.Count >= 2) return;
        if (pb.IsCorrectMatch) return; // Periksa apakah button merupakan correct match

        pb.FlipAnimation();

        selectedPuzzles.Add(pb);

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
            HandleCorrectMatch();
        }
        else
        {
            HandleWrongMatch();
        }

        if (loadPuzzle.puzzleButtonList.Count == 0)
        {
            Debug.Log("Game Finished");
        }

        attemptCount++;
    }

    private void HandleCorrectMatch()
    {
        foreach (PuzzleButton pb in selectedPuzzles)
        {
            pb.IsCorrectMatch = true;
            loadPuzzle.puzzleButtonList.Remove(pb);
        }

        // Jika cocok, biarkan tetap terbuka
        selectedPuzzles.Clear();
        Debug.Log("Correct Match");
    }

    private void HandleWrongMatch()
    {
        wrongAttemptStreak++;
        lifeManager.HalfLife();
        if (wrongAttemptStreak >= 2)
        {
            lifeManager.LoseLife();
            wrongAttemptStreak = 0;

            if (lifeManager.IsDead)
            {
                GameOver();
            }
        }
        Debug.Log("Wrong Match");
        StartCoroutine(ResetInDelay(resetDuration));
    }

    private IEnumerator ResetInDelay(float resetTime)
    {
        yield return new WaitForSeconds(resetTime); // Tunggu selama resetTime
        ResetPuzzles(); // Setelah delay, panggil ResetPuzzles
        selectedPuzzles.Clear(); // Bersihkan daftar setelah reset
    }

    private void ResetPuzzles()
    {
        foreach (PuzzleButton pb in selectedPuzzles)
        {
            pb.BackflipAnimation();
        }
    }

    private void GameOver()
    {
        // Logika saat game over
        puzzleUI.ShowGameOverUI();
        Debug.Log("Game Over! You have no lives left.");
    }
}

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
    private PuzzleAudio puzzleAudio;

    [Header("Puzzle Settings")]
    [SerializeField] private int puzzleAmount; // Jumlah puzzle untuk di-load
    [SerializeField] private int lifeAmount; // Jumlah nyawa untuk di-load
    [SerializeField] private float resetDuration; // Durasi Delay sebelum Reset (Saat Salah)

    private int attemptCount;
    private int wrongAttemptStreak = 0;
    private int winCount;
    private int bestWinCount;
    private List<PuzzleButton> selectedPuzzles = new List<PuzzleButton>();

    private void Start()
    {
        // Mengambil komponen dalam satu objek
        lifeManager = GetComponent<LifeManager>();
        loadPuzzle = GetComponent<LoadPuzzle>();
        puzzleUI = GetComponent<PuzzleUI>();
        puzzleAudio = GetComponent<PuzzleAudio>();

        InitialStart();
    }

    private void InitialStart()
    {
        // Memulai game dengan memuat puzzle dan mengatur nyawa
        loadPuzzle.SetupPuzzle(puzzleAmount);
        lifeManager.SetupLives(lifeAmount);
        puzzleUI.AddWinCount(winCount);

        bestWinCount = PlayerPrefs.GetInt("WinCount");
        puzzleUI.AddBestWinCount(bestWinCount);
    }

    public void SelectPuzzle(PuzzleButton pb)
    {
        if (selectedPuzzles.Contains(pb) || selectedPuzzles.Count >= 2) return;
        if (pb.IsCorrectMatch) return; // Periksa apakah button merupakan correct match

        pb.OpenAnimation();
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
            GameWin();
        }

        attemptCount++;
    }

    private void HandleCorrectMatch()
    {
        puzzleAudio.PlaySFX("Correct");
        foreach (PuzzleButton pb in selectedPuzzles)
        {
            pb.IsCorrectMatch = true;
            loadPuzzle.puzzleButtonList.Remove(pb);
        }

        // Jika cocok, biarkan tetap terbuka
        selectedPuzzles.Clear();
    }

    private void HandleWrongMatch()
    {
        puzzleAudio.PlaySFX("Incorrect");
        wrongAttemptStreak++;
        lifeManager.HalfLife();
        if (wrongAttemptStreak >= 2)
        {
            lifeManager.LoseLife();
            wrongAttemptStreak = 0;

            if (lifeManager.IsDead)
            {
                GameLost();
            }
        }

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
            pb.CloseAnimation();
        }
    }

    private void GameLost()
    {
        puzzleUI.ShowGameOverUI();
    }

    private void GameWin()
    {
        winCount++;
        if (winCount >= bestWinCount)
        {
            PlayerPrefs.SetInt("WinCount", winCount);
            bestWinCount = PlayerPrefs.GetInt("WinCount");
        }

        puzzleUI.AddWinCount(winCount);
        puzzleUI.AddBestWinCount(bestWinCount);

        // Mulai Coroutine untuk restart game dengan delay
        StartCoroutine(RestartGameInDelay(resetDuration));
    }

    private IEnumerator RestartGameInDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Hapus semua PuzzleButton dari puzzlePanel sebelum reset
        ClearPanel();
        ResetLife();

        // Reset nilai-nilai game yang diperlukan
        selectedPuzzles.Clear();
        loadPuzzle.puzzleButtonList.Clear();
        wrongAttemptStreak = 0; // Reset streak kesalahan

        // Atur ulang puzzle dengan ID yang diacak ulang
        loadPuzzle.SetupPuzzle(puzzleAmount);
        lifeManager.SetupLives(lifeAmount);

        StopAllCoroutines();

    }
    
    private void ClearPanel()
    {
        foreach (Transform loadedButton in loadPuzzle.puzzlePanel.transform)
        {
            Destroy(loadedButton.gameObject);
        }
    }

    // Fungsi untuk menghapus dan mengatur ulang jumlah Life
    private void ResetLife()
    {
        // Hapus semua Life di dalam LifeManager
        foreach (Transform child in lifeManager.lifePanel.transform)
        {
            Destroy(child.gameObject);
            lifeManager.lifeIcons.Clear();
        }
    }
}
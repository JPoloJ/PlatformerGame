using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int finalScore;

    private void Awake()
    {
        // Singleton setUp
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        // End of Singleton
    }

    private void Update()
    {
        HandleGlobalInput();
    }

    private void HandleGlobalInput()
    {
        if (Keyboard.current.enterKey.wasPressedThisFrame || Keyboard.current.numpadEnterKey.wasPressedThisFrame)
        {
            string currentScene = SceneManager.GetActiveScene().name;

            if (currentScene == "Title")
            {
                OnStart();
            }
            else if (currentScene == "Gameplay")
            {
                OnRestart();
            }
            else if (currentScene == "Ending")
            {
                OnRestart();
            }
        }

        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            string currentScene = SceneManager.GetActiveScene().name;

            if (currentScene == "Title")
            {
                OnExit();
            }
            else if (currentScene == "Gameplay" || currentScene == "Ending")
            {
                ReturnToTitle();
            }
        }
    }

    public void OnStart()
    {
        if (SceneManager.GetActiveScene().name == "Title")
        {
            finalScore = 0;
            SceneManager.LoadScene("Gameplay");
        }
        else if (SceneManager.GetActiveScene().name == "Ending")
        {
            SceneManager.LoadScene("Title");
        }
    }

    public void OnRestart()
    {
        finalScore = 0;
        SceneManager.LoadScene("Gameplay");
    }

    public void ReturnToTitle()
    {
        SceneManager.LoadScene("Title");
    }

    public void OnExit()
    {
        Debug.Log("Exited");
        Application.Quit();
    }

    private void OnEnable()
    {
        WinCon.Winner += OnWin;
        LoseCon.Loser += OnLose;
        ScoreSystem.OnScoreUpdated += SaveFinalScore;
    }
    private void OnDisable()
    {
        WinCon.Winner -= OnWin;
        LoseCon.Loser -= OnLose;
        ScoreSystem.OnScoreUpdated -= SaveFinalScore;
    }

    private void OnWin(bool win)
    {
        if (win)
        {
            SceneManager.LoadScene(sceneName: "Ending");
        }
    }

    private void OnLose(bool lose)
    {
        if (lose)
        {
            SceneManager.LoadScene(sceneName: "Ending");
        }
    }

    private void SaveFinalScore(int score)
    {
        finalScore = score;
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;
using static WinCon;
using static LoseCon;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        //Singleton setUp
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        //End of Singleton
    }
    public int finalScore;
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

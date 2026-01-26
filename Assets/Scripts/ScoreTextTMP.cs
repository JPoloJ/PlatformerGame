using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ScoreTextTMP: MonoBehaviour
{
    private TextMeshProUGUI label;

    private void Awake()
    {
        label = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "Title")
        {
            label.text = "PRESS <color=yellow>ENTER</color> TO START\n\nPRESS <color=yellow>ESC</color> TO EXIT";
            label.alignment = TextAlignmentOptions.Center;
            label.fontSize = 36;
            label.color = Color.white;

            label.fontStyle = FontStyles.Bold;
        }
        else if (currentScene == "Gameplay")
        {
            label.text = "SCORE: <color=#FFD700>0</color>";
            label.alignment = TextAlignmentOptions.TopLeft;
            label.fontSize = 28;
            label.color = Color.white;

            label.outlineWidth = 0.2f;
            label.outlineColor = Color.black;
        }
        else if (currentScene == "Ending")
        {
            if (GameManager.Instance != null)
            {
                label.text = "GAME OVER\n\n" +
                            "FINAL SCORE: <color=#FFD700>" + GameManager.Instance.finalScore + "</color>\n\n" +
                            "PRESS <color=yellow>ENTER</color> TO RESTART\n" +
                            "PRESS <color=yellow>ESC</color> FOR MENU";
            }
            else
            {
                label.text = "GAME OVER\n\n" +
                            "FINAL SCORE: <color=#FFD700>0</color>\n\n" +
                            "PRESS <color=yellow>ENTER</color> TO RESTART\n" +
                            "PRESS <color=yellow>ESC</color> FOR MENU";
            }

            label.alignment = TextAlignmentOptions.Center;
            label.fontSize = 32;
            label.color = Color.white;
            label.fontStyle = FontStyles.Bold;
        }
    }

    private void OnEnable()
    {
        ScoreSystem.OnScoreUpdated += UpdateScoreText;
    }

    private void OnDisable()
    {
        ScoreSystem.OnScoreUpdated -= UpdateScoreText;
    }

    private void UpdateScoreText(int score)
    {
        if (SceneManager.GetActiveScene().name == "Gameplay")
        {
            label.text = "SCORE: <color=#FFD700>" + score.ToString() + "</color>";
        }
    }
}
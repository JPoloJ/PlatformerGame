using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Text))]
public class ScoreText : MonoBehaviour
{
    private Text label;

    private void Awake()
    {
        label = GetComponent<Text>();
    }
    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Ending")
        {
            if (GameManager.Instance != null)
            {
                label.text = "The END!\nFinal Score: " + GameManager.Instance.finalScore; 
            }
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
        label.text = score.ToString();
    }
}

using UnityEngine;
using TMPro;

public class BlinkingText_TMP : MonoBehaviour
{
    [SerializeField] private float blinkSpeed = 1f;
    [SerializeField] private bool startBlinking = true;

    private TextMeshProUGUI textComponent;
    private float timer;

    void Start()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
        timer = 0f;
    }

    void Update()
    {
        if (!startBlinking) return;

        timer += Time.deltaTime;

        if (timer >= blinkSpeed)
        {
            textComponent.enabled = !textComponent.enabled;
            timer = 0f;
        }
    }

    public void StartBlinking()
    {
        startBlinking = true;
        textComponent.enabled = true;
    }

    public void StopBlinking()
    {
        startBlinking = false;
        textComponent.enabled = true;
    }
}
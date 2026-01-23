using UnityEngine;

public class LoseCon : MonoBehaviour
{
    public delegate void Lose(bool isLose);
    public static event Lose Loser;
    public static void OnPlayerDied()
    {
        Loser?.Invoke(true);
    }
}

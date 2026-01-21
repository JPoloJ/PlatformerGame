using UnityEngine;

public class WinCon : MonoBehaviour
{
    public delegate void Win(bool isWin);
    public static event Win Winner;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Winner?.Invoke(true);
        }
    }
}

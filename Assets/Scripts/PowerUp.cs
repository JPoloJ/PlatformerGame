using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private string pTag = "Player";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == pTag)
        {
            PlayerJump playerJump = collision.GetComponent<PlayerJump>();
            if (playerJump != null)
            {
                playerJump.PowerUpBoost(1.5f);
                Destroy(gameObject);
            }
        }
    }
}

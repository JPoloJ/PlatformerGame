using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private string pTag = "Player";
    [SerializeField] private float multiplier = 2f;

    private bool _hasBeenCollected = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(pTag) && !_hasBeenCollected)
        {
            _hasBeenCollected = true;
            PlayerJump playerJump = collision.GetComponent<PlayerJump>();

            if (playerJump != null/* && playerJump.jumpForce <= 4.1f*/)
            {
                playerJump.PowerUpBoost(multiplier);
            }

            Destroy(gameObject);
        }
    }
 }

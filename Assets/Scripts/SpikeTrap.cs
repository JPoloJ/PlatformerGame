using UnityEngine;
using System.Collections;

public class SpikeTrap : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(DeathSequence(other.gameObject));
        }
    }

    private IEnumerator DeathSequence(GameObject player)
    {
        player.GetComponent<PlayerMove>().enabled = false;
        player.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;

        Animator anim = player.GetComponent<Animator>();
        if (anim != null)
        {
            anim.SetTrigger("Die");
        }

        yield return new WaitForSeconds(1.2f);

        LoseCon.OnPlayerDied();
    }
}

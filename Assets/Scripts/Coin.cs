using System;
using System.Collections;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int Value = 5;


    public static Action<Coin> OnCoinCollected;

    private Animator _animator;
    private Collider2D _collider;
    private bool _hasBeenCollected = false;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _collider = GetComponent<Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !_hasBeenCollected)
        {
            _hasBeenCollected = true;
            StartCoroutine(CollectSequence());
        }
    }
    private IEnumerator CollectSequence()
    {
        OnCoinCollected?.Invoke(this);

        if (_collider != null) _collider.enabled = false;

        if (_animator != null)
        {
            _animator.SetTrigger("Collected");
        }

        yield return new WaitForSeconds(0.5f);

        Destroy(gameObject);
    }
}

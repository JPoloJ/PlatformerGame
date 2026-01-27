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

    [SerializeField] private AudioClip coinPicked;
    [SerializeField][Range(0f, 1f)] private float coinPickedVolume = 0.5f;

    AudioSource audioSource;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _collider = GetComponent<Collider2D>();

        SetupAudio();
    }

    private void SetupAudio()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.playOnAwake = false;
        audioSource.volume = coinPickedVolume;
        audioSource.spatialBlend = 0f;
    }

    private void PlaySound()
    {
        if (audioSource != null && coinPicked != null)
        {
            audioSource.PlayOneShot(coinPicked);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !_hasBeenCollected)
        {
            _hasBeenCollected = true;
            PlaySound();
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

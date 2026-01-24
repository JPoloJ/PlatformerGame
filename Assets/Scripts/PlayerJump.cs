using UnityEngine;
using System.Collections;

public class PlayerJump : MonoBehaviour
{
    public bool isGrounded = false;
    public int jumpsToDo;
    public int maxJumps = 2;
    public float jumpForce = 4f;

    public float wallSlidingSpeed = 0.5f;
    public float wallJumpDuration = 0.2f;

    public BoxCollider2D groundCol;

    [Header("Audio Settings")]
    [SerializeField] private AudioClip jumpSound;
    [SerializeField][Range(0f, 1f)] private float jumpVolume = 0.5f;

    AudioSource audioSource;
    Rigidbody2D _rigidbody;
    Animator _animator;
    PlayerMove _playerMove;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _playerMove = GetComponent<PlayerMove>();

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
        audioSource.volume = jumpVolume;
        audioSource.spatialBlend = 0f;
    }

    private void PlayJumpSound()
    {
        if (audioSource != null && jumpSound != null)
        {
            audioSource.PlayOneShot(jumpSound);
        }
    }

    void FixedUpdate()
    {
        if (IsOnWall() && !isGrounded && _rigidbody.linearVelocity.y < 0)
        {
            _rigidbody.linearVelocity = new Vector2(_rigidbody.linearVelocity.x, -wallSlidingSpeed);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            isGrounded = true;
            _animator.SetBool("IsGrounded", true);
            jumpsToDo = maxJumps;
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall") && !isGrounded)
        {
            _animator.SetBool("IsOnWall", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            isGrounded = false;
            _animator.SetBool("IsGrounded", false);
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            _animator.SetBool("IsOnWall", false);
        }
    }

    private void Jump(bool isDoubleJump)
    {
        if (_rigidbody == null) return;
        _rigidbody.linearVelocity = new Vector2(
            _rigidbody.linearVelocity.x,
            jumpForce
        );

        PlayJumpSound();

        if (isDoubleJump)
        {
            _animator.SetTrigger("DoubleJump");
        }
    }

    void OnJump()
    {
        
        if (isGrounded)
        {
            jumpsToDo--;
            Jump(false);
        }
        else if (IsOnWall())
        {
            StartCoroutine(PerformWallJump());
        }
        else if (jumpsToDo > 0)
        {
            jumpsToDo--;
            Jump(true);
        }
    }

    private IEnumerator PerformWallJump()
    {
        jumpsToDo = maxJumps - 1;

        float direction = -Mathf.Sign(transform.Find("TopWallCheck").localPosition.x);

        
        _rigidbody.linearVelocity = new Vector2(
            _rigidbody.linearVelocity.x,
            jumpForce
        );


        yield return new WaitForSeconds(wallJumpDuration);

    }

    private bool IsOnWall()
    {
        return _playerMove != null && _playerMove.IsTouchingWall();
    }

    public void PowerUpBoost(float multiplier)
    {
        jumpForce = jumpForce*multiplier;
    }
}

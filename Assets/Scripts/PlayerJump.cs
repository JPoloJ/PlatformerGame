using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public bool isGrounded = false;
    public int jumpsToDo;

    public int maxJumps = 2;
    public float jumpForce = 4f; 
    public BoxCollider2D groundCol;

    Rigidbody2D _rigidbody;
    Animator _animator;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        isGrounded = true;
        _animator.SetBool("IsGrounded", true);
        jumpsToDo = maxJumps;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isGrounded = false;
        _animator.SetBool("IsGrounded", false);
    }

    private void Jump(bool isDoubleJump)
    {
        if (_rigidbody == null) return;
        _rigidbody.linearVelocity = new Vector2(
            _rigidbody.linearVelocity.x,
            jumpForce
        );

        if (isDoubleJump)
        {
            _animator.SetTrigger("DoubleJump");
        }
    }

    void OnJump()
    {
        
        if (isGrounded)
        {
            Jump(false);
        }
        
        else if (jumpsToDo > 0)
        {
            jumpsToDo--;
            Jump(true);
        }
    }

    public void PowerUpBoost(float multiplier)
    {
        jumpForce = jumpForce*multiplier;
    }
}

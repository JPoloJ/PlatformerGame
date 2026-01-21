using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private float Speed = 5.0f;

    Rigidbody2D _rigidbody;
    Animator _animator;
    SpriteRenderer _spriteRenderer;
    private float _horizontalDir; // Horizontal move direction value [-1, 1]

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

    }

    void FixedUpdate()
    {
        Vector2 velocity = _rigidbody.linearVelocity;
        velocity.x = _horizontalDir * Speed;
        _rigidbody.linearVelocity = velocity;
        _animator.SetFloat("Speed", Mathf.Abs(_horizontalDir));
    }

    // NOTE: InputSystem: "move" action becomes "OnMove" method
    void OnMove(InputValue value)
    {
        // Read value from control, the type depends on what
        // type of controls the action is bound to
        var inputVal = value.Get<Vector2>();
        _horizontalDir = inputVal.x;
        FlipSprite();
    }
    private void FlipSprite()
    {
        if (_horizontalDir > 0)
        {
            _spriteRenderer.flipX = false; 
        }
        else if (_horizontalDir < 0)
        {
            _spriteRenderer.flipX = true; 
        }
    }
}

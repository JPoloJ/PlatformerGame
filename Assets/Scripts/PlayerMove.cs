using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float Speed = 5.0f;
    [SerializeField] private Transform topWallCheck;
    [SerializeField] private Transform bottomWallCheck;
    [SerializeField] private float checkRadius = 0.2f;
    [SerializeField] private LayerMask wallLayer;

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
        float moveX = _horizontalDir;

        if (IsTouchingWall() && Mathf.Abs(_horizontalDir) > 0.01f)
        {
            float wallSide = Mathf.Sign(topWallCheck.localPosition.x);
            if (Mathf.Sign(_horizontalDir) == wallSide)
            {
                moveX = 0; 
            }
        }

        Vector2 velocity = _rigidbody.linearVelocity;
        velocity.x = moveX * Speed;
        _rigidbody.linearVelocity = velocity;

        _animator.SetFloat("Speed", Mathf.Abs(moveX));
    }
    private bool IsTouchingWall()
    {
        bool topTouch = Physics2D.OverlapCircle(topWallCheck.position, checkRadius, wallLayer);
        bool bottomTouch = Physics2D.OverlapCircle(bottomWallCheck.position, checkRadius, wallLayer);

        return topTouch || bottomTouch;
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
            UpdateCheckPositions(Mathf.Abs(topWallCheck.localPosition.x));
        }
        else if (_horizontalDir < 0)
        {
            _spriteRenderer.flipX = true;
            UpdateCheckPositions(-Mathf.Abs(topWallCheck.localPosition.x));
        }
    }
    private void UpdateCheckPositions(float xPos)
    {
        topWallCheck.localPosition = new Vector3(xPos, topWallCheck.localPosition.y, 0);
        bottomWallCheck.localPosition = new Vector3(xPos, bottomWallCheck.localPosition.y, 0);
    }


    private void OnDrawGizmos()
    {
        if (topWallCheck != null && bottomWallCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(topWallCheck.position, checkRadius);
            Gizmos.DrawWireSphere(bottomWallCheck.position, checkRadius);
        }
    }
}

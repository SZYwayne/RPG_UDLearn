using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Move Info")]
    public float xSpeed;
    public float jumpForce;
    public float dashColddown = 5f;
    public float dashSpeed;
    public float dashDuration;
    public float dashTime;

    [Header("Collision Info")]
    [SerializeField] public float groundCheckDistance;
    public Transform groundCheck;
    [SerializeField] public float wallCheckDistance;
    public Transform wallCheck;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask whatIsWall;

    public bool facingRight = true;
    public int facingDir { get; private set; } = 1;

    #region Components
    public Animator anim {  get; private set; }
    public Rigidbody2D rb { get; private set; }
    #endregion

    #region States
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    #endregion
    
    private void Awake()
    {
        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airState = new PlayerJumpState(this, stateMachine, "Jump");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
    }

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        stateMachine.Initialize(idleState);
    }

    private void Update()
    {
        stateMachine.currentState.Update();
        dashTime -= Time.deltaTime;
        checkForDash();
    }
    private void checkForDash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashTime < 0)
        {
            stateMachine.ChangeState(dashState);
        }
    }

    public void SetVelocity(float _xInput, float _yInput)
    {
        rb.velocity = new Vector2(_xInput, _yInput);
        FlipController(_xInput);
    }

    public bool isGrounded() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);

    public void Flip()
    {
        facingDir *= -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    public void FlipController(float _x)
    {
        if (_x > 0 && !facingRight)
        {
            Flip();
        }
        else if (_x < 0 && facingRight)
        {
            Flip();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
    }
}

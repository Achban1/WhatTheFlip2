using System.Collections;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;


public enum PlayerID
{
    Player1,
    Player2
}

public enum PlayerState
{
    Idle,
    Run,
    Jump,
}

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance { get; private set; }

    [Header("Player ID")]
    public PlayerID playerID;

    [Header("Movement")]
    public float maxSpeed = 5;
    public float acceleration = 20;
    public float deacceleration = 4;
    internal float velocityX;

    [Header("Jump")]
    public float jumpPower = 8;
    public float groundCheckDistance = 0.01f;

    public bool onGround = true;
    float groundCheckLength;
    int[] maxJumps = { 2, 2 }; // Array to hold maxJumps for Player1 and Player2
    int[] currentJumps = { 0, 0 }; // Array to hold currentJumps for Player1 and Player2
    private Vector2 lastFrameVelocity;

    Rigidbody2D rb2D;
    public PlayerState state = PlayerState.Idle;

    bool isBouncing = false;

    bool canMove = true;
    bool canMoveAtAll = true;       //to turn off all movement when changing scene or flipping

    CameraScript camerascript;

    public new Transform transform;

    private AudioScriptPlay audioScriptPlayPlayer1Jump;
    private AudioScriptPlay audioScriptPlayPlayer2Jump;

    public GameObject quickDash;
    public GameObject jumpEffect;

    private void Start()
    {
        Instance = this;
        Physics2D.queriesStartInColliders = false;
        rb2D = GetComponent<Rigidbody2D>();
        var collider = GetComponent<Collider2D>();
        groundCheckLength = collider.bounds.size.y + groundCheckDistance;

        camerascript = Camera.main.GetComponent<CameraScript>();

        transform = GetComponent<Transform>();

        audioScriptPlayPlayer1Jump = GameObject.FindGameObjectWithTag("Player1AudioSourceJump").GetComponent<AudioScriptPlay>();
        audioScriptPlayPlayer2Jump = GameObject.FindGameObjectWithTag("Player2AudioSourceJump").GetComponent<AudioScriptPlay>();
    }

    private float GetPlayerInputHorizontal()
    {
        switch (playerID)
        {
            case PlayerID.Player1:
                return Input.GetKey(KeyCode.A) ? -1 : Input.GetKey(KeyCode.D) ? 1 : 0;
            case PlayerID.Player2:
                return Input.GetKey(KeyCode.LeftArrow) ? -1 : Input.GetKey(KeyCode.RightArrow) ? 1 : 0;
            default:
                return 0;
        }
    }

    private bool GetPlayerJumpInputDown()
    {
        switch (playerID)
        {
            case PlayerID.Player1:
                return Input.GetKeyDown(KeyCode.W);
            case PlayerID.Player2:
                return Input.GetKeyDown(KeyCode.UpArrow);
            default:
                return false;
        }
    }

    private bool GetPlayerJumpInputReleased()
    {
        switch (playerID)
        {
            case PlayerID.Player1:
                return Input.GetKeyUp(KeyCode.W);
            case PlayerID.Player2:
                return Input.GetKeyUp(KeyCode.UpArrow);
            default:
                return false;
        }
    }
    private void GravityAdjust()
    {
        if (rb2D.velocity.y < 0)
            rb2D.gravityScale = 4;
        else
            rb2D.gravityScale = 1;
    }
    private void Jump()
    {
        int playerIndex = (playerID == PlayerID.Player1) ? 0 : 1;

        if (GetPlayerJumpInputDown() && currentJumps[playerIndex] < maxJumps[playerIndex])
        {
            onGround = false;
            currentJumps[playerIndex]++;
            rb2D.velocity = new Vector2(rb2D.velocity.x, jumpPower);         
            state = PlayerState.Jump;

            if (playerID == PlayerID.Player1)
            {
                audioScriptPlayPlayer1Jump.PlayAuido();
            }
            else if (playerID == PlayerID.Player2)
            {
                audioScriptPlayPlayer2Jump.PlayAuido();
            }
        }
        else if (GetPlayerJumpInputReleased() && rb2D.velocity.y > 0)
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, rb2D.velocity.y * 0.5f);
        }

        onGround = Physics2D.Raycast(transform.position, Vector2.down, groundCheckLength);
        if (onGround)
            currentJumps[playerIndex] = 0;
    }

    private void HorizontalMovement()
    {
        float x = GetPlayerInputHorizontal();

        if (Mathf.Abs(velocityX) > 0.5f)
        {
            transform.localScale = new Vector3(Mathf.Sign(velocityX) * 0.5f, 0.5f, 0.5f);
            state = PlayerState.Run;
        }
        else
        {
            state = PlayerState.Idle;
        }

        velocityX += x * acceleration * Time.deltaTime;
        velocityX = Mathf.Clamp(velocityX, -maxSpeed, maxSpeed);

        if (x == 0 || (x < 0 == velocityX > 0))
        {
            velocityX *= 1 - deacceleration * Time.deltaTime;
        }

        rb2D.velocity = new Vector2(velocityX, rb2D.velocity.y);

        // Change the player's orientation based on the direction of movement
        if (velocityX > 0.1f)
        {
            transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
        else if (velocityX < -0.1f)
        {
            transform.localScale = new Vector3(-0.5f, 0.5f, 0.5f);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerMovement otherPlayer = collision.gameObject.GetComponent<PlayerMovement>();
        if (otherPlayer != null && !isBouncing)
        {

            float bounce = 1f;

            Vector2 bounceDirection = (this.rb2D.position - otherPlayer.rb2D.position).normalized;
            Vector2 bounceForce = bounceDirection * bounce * Mathf.Abs(rb2D.velocity.magnitude);

            rb2D.AddForce(bounceForce, ForceMode2D.Impulse);
            otherPlayer.rb2D.AddForce(-bounceForce, ForceMode2D.Impulse); // Bounce the other player in the opposite direction

            isBouncing = true;
            Invoke(nameof(StopBounce), 0.3f);

            DisableMovement();
            otherPlayer.DisableMovement();

            camerascript.Shake(0.1f);
        }
    }

    public void DisableMovement()
    {
        canMove = false;
        Invoke(nameof(EnableMovement), 0.5f);  // Enable movement after 0.5 seconds
    }

    void EnableMovement()
    {
        canMove = true;
        velocityX = rb2D.velocity.x;
    }
    public void DisableAllMovement()
    {
        canMoveAtAll = false;
        rb2D.velocity = Vector2.zero;
        rb2D.gravityScale = 0f;
        Invoke(nameof(EnableAllMovement),0.5f);
        //turnof colliders
    }
    void EnableAllMovement()
    {
        rb2D.gravityScale = 1f;
        canMoveAtAll = true;
    }

    void StopBounce()
    {
        isBouncing = false;
    }

    void Update()
    {
        if (!canMoveAtAll)
        {
            return;
        }

        if (canMove)
        {
            HorizontalMovement();
            CheckForQuickDash();
        }
        Jump();
        GravityAdjust();
        CheckForJumpEffect();
    }

    private void CheckForQuickDash()
    {
        float velocityThreshold = 0.5f;
        bool hasVelocity = rb2D.velocity.magnitude > velocityThreshold;
        bool hasTurned = Mathf.Sign(rb2D.velocity.x) != Mathf.Sign(lastFrameVelocity.x) && rb2D.velocity.x != 0 && lastFrameVelocity.x != 0;
        bool isOnGround = onGround;

        // If all conditions are met, enable quick dash
        if (hasVelocity && hasTurned && isOnGround)
        {
            EnableQuickDash();
        }
        // Update last frame velocity for the next frame
        lastFrameVelocity = rb2D.velocity;
    }

    public void EnableQuickDash()
    {
        Invoke(nameof(ActivateQuickDash), 0f);  
    }

    private void ActivateQuickDash()
    {
        if (quickDash != null)
        {
            quickDash.SetActive(true);
            Invoke(nameof(DeactivateQuickDash), 0.2f);  
        }
    }

    private void DeactivateQuickDash()
    {
        if (quickDash != null)
        {
            quickDash.SetActive(false);
        }
    }
    private void CheckForJumpEffect()
    {
        if (state == PlayerState.Jump && jumpEffect != null)
        {
            jumpEffect.SetActive(true);
            Invoke(nameof(DisableJumpEffect), 0.1f);
        }
    }
    private void DisableJumpEffect()
    {
        if (jumpEffect != null)
        {
            jumpEffect.SetActive(false);
        }
    }

}

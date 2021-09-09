using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    const float RADIUS = 0.01f;
    private int DEFAULT_EXTRA_JUMPS_VALUE = 0;
    private bool facingRight = true;
    private bool isGrounded;

    private float speed;
    private float jumpForce;
    private float moveInput;
    private int extraJumps;
   
    private Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask whatIsGround;
    public Animator playerAnimation;

    void SetPlayerSpeed(float speed)
    {
        if (speed < 0)
        {
            throw new System.Exception("Error.SPEED");
        }
        this.speed = speed;
    }

    void SetPlayerExtraJumps(int jumpValue)
    {
        if (jumpValue < 0)
        {
            throw new System.Exception("Error.EXTRAJUMPS");
        }
        this.extraJumps = jumpValue;
    }

    void SetPlayerMoveInput(float move)
    {
        if ( move < -1 || move > 1)
        {
            throw new System.Exception("Error.MOVEINPUT");
        }
        this.moveInput = move;
    }
    void SetPlayerJumpForce(float jumpforce)
    {
        if (jumpforce < 0)
        {
            throw new System.Exception("Error.JUMPFORCE");
        }
        this.jumpForce = jumpforce;
    }
    float GetPlayerSpeed()
    {
        return this.speed;
    }
    float GetPlayerJumpForce()
    {
        return this.jumpForce;
    }
    float GetPlayerMoveInput()
    {
        return this.moveInput;
    }
    int GetPlayerExtraJumps()
    {
        return this.extraJumps;
    }
        private void Start()
    {
        SetPlayerSpeed(2);
        SetPlayerJumpForce(4f);
        rb = GetComponent<Rigidbody2D>();
        transform.position = new Vector2(PlayerPrefs.GetFloat("xPos"), PlayerPrefs.GetFloat("yPos"));
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, RADIUS, whatIsGround);
        SetPlayerMoveInput(Input.GetAxis("Horizontal"));
        rb.velocity = new Vector2(GetPlayerMoveInput() 
            * GetPlayerSpeed(), rb.velocity.y);
        if (facingRight == false && GetPlayerMoveInput() > 0)
        {
            Flip();
        }
        else if (facingRight == true && GetPlayerMoveInput() < 0)
        {
            Flip();
        }
    }

    private void Update()
    {
        if (isGrounded == true)
        {
            SetPlayerExtraJumps(DEFAULT_EXTRA_JUMPS_VALUE);
        }

        if (Input.GetKeyDown(KeyCode.W) && GetPlayerExtraJumps() > 0)
        {
            rb.velocity = Vector2.up * GetPlayerJumpForce();
            SetPlayerExtraJumps(GetPlayerExtraJumps() - 1);
        }
        else if (Input.GetKeyDown(KeyCode.W) && 
            GetPlayerExtraJumps() == 0 && isGrounded == true)
        {
            rb.velocity = Vector2.up * GetPlayerJumpForce();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            playerAnimation.SetBool("isJump", true);
        }
        else
        {
            if (isGrounded == true)
            {
                playerAnimation.SetBool("isJump", false);
            }
        }

        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) && isGrounded == true)
        {
            playerAnimation.SetBool("walk", true);
        }
        else
        {
            playerAnimation.SetBool("walk", false);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            playerAnimation.SetBool("isShoot", true);
        }
        else
        {
            playerAnimation.SetBool("isShoot", false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            playerAnimation.SetInteger("switch", 1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            playerAnimation.SetInteger("switch", 0);
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "spikes")
        {
            SceneManager.LoadScene(0);
        }

        if (other.tag == "checkpoint")
        {
            PlayerPrefs.SetFloat("xPos", transform.position.x);
            PlayerPrefs.SetFloat("yPos", transform.position.y);
        }
    }

}


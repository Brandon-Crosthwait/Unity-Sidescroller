using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    //movementSpeed holds the value for the speed of the player's movement
    public float movementSpeed;
    public float jumpForce;
    private float gravityScale = 5.0f;
    public Transform feet;
    public Transform startLocation;
    public Transform checkPointLocation;
    public LayerMask groundLayer;

    //rb is the RigidBody2D assigned to the player
    private Rigidbody2D rb;
    // Animator assigned to the player
    private Animator animator;
    // Float value for the x-axis movement
    private float movementX;
    // Bool for when the player is jumping
    private bool isJumping;
    // Bool for if the player is on the ground
    private bool isGrounded;
    public bool canMove;
    private bool endLevel = false;
    
    //Bool for disallowing player movement buffering while paused
    public static bool isPaused = false;

    // Variable to update the score
    //public ScoreScript scoreValue; ??
    // Object used to check and update the players health
    public Health health;
    // Variable which will determine how long a power up lasts
    private int powerUpTime;

    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip collectSound;

    // Wall Jump Variables
    private float wallJumpTime = 0.2f;
    private float wallSlideSpeed = -1.0f;
    private float wallDistance = 0.5f;
    bool isWallSliding = false;
    RaycastHit2D WallCheckHit;
    float jumpTime;
    [SerializeField] private float initialwallJumpVelocity;
    private float currentwallJumpVelocity;

    private float time;
    private float timer;

    // Double Jump Variables
    private bool doubleJumpActive = false;
    private int doubleJumpCount;
    private int timesJumped = 0;

    private void Start() {
        animator = GetComponent<Animator>();
            rb = GetComponent<Rigidbody2D>();

            health = FindObjectOfType<Health>();
            canMove = true;

            time = 0.5f;
            timer = Time.time;

            Respawn();
    }

    // Runs every frame
    // Detect Input and update animation here.
    private void Update() {
        movementX = Input.GetAxisRaw("Horizontal");
        SetFacingDirection();

        // Respawns player upon pressing r to the last checkpoint
        if(Input.GetKeyDown(KeyCode.R))
        {
            //UIManager.RemoveText();
            if (endLevel)
            {
                // Do Nothing
            }
            else
            {
                Respawn();
            }
        }


        // Set the speed variable in the animator to match the x input
        if (canMove)
            animator.SetFloat("Speed", Mathf.Abs(movementX));
        else
            animator.SetFloat("Speed", 0f);

        //if the player presses space and they are on the ground, the player jumps
        /*
        if (Input.GetButtonDown("Jump") && isGrounded || doubleJumpActive && doubleJumpCount > 0 && Input.GetButtonDown("Jump") || isWallSliding && Input.GetButtonDown("Jump")) {
            Jump();

            //determines the push back when a player is trying to wall jump
            if ((movementX < -0.01f) && (isWallSliding) && (Input.GetButtonDown("Jump"))) //facing left
            {
                currentwallJumpVelocity = initialwallJumpVelocity;
            }
            else if ((movementX > 0.01f) && (isWallSliding) && (Input.GetButtonDown("Jump"))) //facing right
            {
                currentwallJumpVelocity = -initialwallJumpVelocity;
            }
            
            //determines if the player has picked up a JumpPowerUpCollectable and allows the player to double jump
            if (doubleJumpActive && timesJumped > 0 && !isGrounded)
            {
                doubleJumpCount--;
                doubleJumpActive = false;
            }
        }
        */
        

        
        timer += Time.deltaTime;
        if (timer >= time)
        {
            if (Input.GetButtonDown("Jump") && isGrounded || doubleJumpActive && doubleJumpCount > 0 && Input.GetButtonDown("Jump") || isWallSliding && Input.GetButtonDown("Jump"))
            {
                Jump();
                timer = 0;

                if(doubleJumpActive)
                {
                    timer = 0.3f;
                }
                
                //determines the push back when a player is trying to wall jump
                if ((movementX < -0.01f) && (isWallSliding) && (Input.GetButtonDown("Jump"))) //facing left
                {
                    currentwallJumpVelocity = initialwallJumpVelocity;
                }
                else if ((movementX > 0.01f) && (isWallSliding) && (Input.GetButtonDown("Jump"))) //facing right
                {
                    currentwallJumpVelocity = -initialwallJumpVelocity;
                }

                //determines if the player has picked up a JumpPowerUpCollectable and allows the player to double jump
                if (doubleJumpActive && timesJumped > 0 && !isGrounded)
                {
                    doubleJumpCount--;
                    doubleJumpActive = false;
                }
                
            }
        }
        


        // Updated the powerUpTime variable to count how long a player should have a powerup
        if (powerUpTime > 0)
        { 
            powerUpTime--;
        }
        if (powerUpTime == 0)
        {
            movementSpeed = 8;
        }

    }

    // Updated on a fixed time.
    // Used for movement and physics interactions
    private void FixedUpdate() {
        Move();
        bool wasGrounded = isGrounded;
        GroundedCheck();
        if (isGrounded && !wasGrounded) {
            animator.SetBool("isJumping", false);
        }
        isJumping = false;

        //Wall Jump
        if (movementX > 0.01f)
        {
            WallCheckHit = Physics2D.Raycast(transform.position, new Vector2(wallDistance, 0), wallDistance, groundLayer);
        }
        else if (movementX < -0.01f) 
        {
            WallCheckHit = Physics2D.Raycast(transform.position, new Vector2(-wallDistance, 0), wallDistance, groundLayer);
        }

        if (WallCheckHit && !isGrounded && movementX != 0)
        {
            isWallSliding = true;
            jumpTime = Time.time + wallJumpTime;
        }
        else if (jumpTime < Time.time)
        {
            isWallSliding = false;
        }

        if (isWallSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, wallSlideSpeed, float.MaxValue));
        }

        if (currentwallJumpVelocity > 0)
        {
            currentwallJumpVelocity--;
        }
        else if (currentwallJumpVelocity < 0)
        {
            currentwallJumpVelocity++;
        }

    }

    // Jump() takes the player's x-axis velocity and applies the jump force
    public void Jump() {
        if (canMove)
        {
            if (isPaused != true)
            {
                SoundManager.instance.PlaySound(jumpSound);
                Vector2 movement = new Vector2(rb.velocity.x, jumpForce);
                rb.AddForce(new Vector2(0, jumpForce));
                isJumping = true;
                animator.SetBool("isJumping", isJumping);
                timesJumped += 10;
            }
            else { }
            
        }
    }

    public bool IsGrounded() {
        Collider2D groundCheck = Physics2D.OverlapBox(feet.position, new Vector2(0.75f, 0.01f), 0, groundLayer);
    
        if (groundCheck != null) {
            return true;
        }
        return false;
    }

    private void Move() {
        if (canMove)
        {
            Vector2 movement = new Vector2((movementX * movementSpeed) + currentwallJumpVelocity, rb.velocity.y);
            rb.velocity = movement;
        }
        else
            rb.velocity = Vector2.zero;
    }

    private void GroundedCheck() {
        // Creates a box covering the distance of the player's feet and checks if there is any ground below.
        Collider2D groundCheck = Physics2D.OverlapBox(feet.position, new Vector2(0.6875f, 0.01f), 0, groundLayer);
        if (groundCheck != null) {
            isGrounded = true;
            timesJumped = 0;
        } 
        else
            isGrounded = false;
    }

    private void SetFacingDirection() {
        // Change the direction of the character based on the direction they are moving
        if (canMove)
        {
            if (movementX > 0.01f)
                transform.localScale = Vector3.one;
            else if (movementX < -0.01f)
                transform.localScale = new Vector3 (-1, 1, 1);
        }
    }

    //Method used to "pick up" collectables
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Adding to score if ScoreCollectable is picked up
        if (other.gameObject.CompareTag("ScoreCollectable"))
        {
            SoundManager.instance.PlaySound(collectSound);
            Destroy(other.gameObject);
            ScoreScript.scoreValue += 10;
        }
        //Adding to health if HealthCollectable is picked up
        if (other.gameObject.CompareTag("HealthCollectable") && health.currentHealth < 3)
        {
            SoundManager.instance.PlaySound(collectSound);
            Destroy(other.gameObject);
            health.IncreaseHealth(1);
        }
        //Players speed increases if a PowerUpCollectable is picked up
        if (other.gameObject.CompareTag("PowerUpCollectable"))
        {
            SoundManager.instance.PlaySound(collectSound);
            Destroy(other.gameObject);
            movementSpeed = 20;
            powerUpTime = 3000;
        }
        //Player is able to double jump if JumpPowerUpCollectable is picked up
        if (other.gameObject.CompareTag("JumpPowerUpCollectable"))
        {
            SoundManager.instance.PlaySound(collectSound);
            Destroy(other.gameObject);
            doubleJumpActive = true;
            doubleJumpCount = 1;
        }
        if (other.gameObject.CompareTag("Checkpoint"))
        {
            SoundManager.instance.PlaySound(collectSound);
            startLocation.position = checkPointLocation.position;
        }
        if (other.gameObject.CompareTag("LevelEnd"))
        {
            endLevel = true;
        }
    }


    public void Respawn() 
    {
        transform.SetPositionAndRotation(startLocation.position + new Vector3(0.5175f, 0.5175f, 0f), transform.rotation);
        animator.SetBool("isDead", false);
        animator.SetTrigger("Appear");
        SetCanMove(true);
        rb.gravityScale = gravityScale;
    }

    public void GetHit()
    {
        animator.SetTrigger("isHit");
    }

    public void EndLevel()
    {
        endLevel = true;
    }

    public void SetCanMove(bool canMove)
    {
        this.canMove = canMove;
    }

    public void PlayerDeath()
    {
        SetCanMove(false);
        animator.SetBool("isDead", true);
        animator.SetBool("isJumping", false);
        rb.gravityScale = 0f;
    }
}

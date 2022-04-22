using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    //holds which character is selected
    private int characterSelected = 0;
    //movementSpeed holds the value for the speed of the player's movement
    public float movementSpeed;
    // Holds value for jump force
    public float jumpForce;
    //Gravity scale is initialized
    private float gravityScale = 5.0f;
    public Transform feet;
    //Holds startup Location
    public Transform startLocation;
    //Holds checkPoint Location
    public Transform checkPointLocation;
    public LayerMask groundLayer;
    //Monitors "r" for respawn when dead
    private bool rToRespawn = false;
    //Gets set to true when checkpoint is unlocked.
    private bool checkPointUnlocked = false;

    //[SerializeField] private UnityEvent onContactTrigger;

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

    //transform for the bullet spawn point
    public Transform bulletSpawnPoint;

    // Variable to update the score
    //public ScoreScript scoreValue; ??
    // Object used to check and update the players health
    public Health health;
    // Variable which will determine how long a power up lasts
    private float powerUpTime;
    private float powerUpTimer;
    private bool powerUp = false;

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

    // Turtle Enemy Variables
    private float slowMovementTime;
    private float slowMovementTimer;
    bool slowMovement = false;

    private PlayerMovementHelper playerMovementHelper = new PlayerMovementHelper();

    private void Start() {
            characterSelected = PlayerPrefs.GetInt("CharacterAnimatorOverriderID");
            animator = GetComponent<Animator>();
            rb = GetComponent<Rigidbody2D>();

            health = FindObjectOfType<Health>();
            canMove = true;

            time = 0.5f;
            timer = Time.time;

            powerUpTime = 0.0f;
            powerUpTimer = Time.time;

            slowMovementTime = 0.0f;
            slowMovementTimer = Time.time;

            //initializes movement speed if speed character is selected (scuba)
            if (characterSelected == 1)
            {
                movementSpeed = 11f;
            }
            //initalizes jumpforce if jump character is selected (Phatphin)
            if (characterSelected == 2)
            {
                initialwallJumpVelocity = 24;
                //jumpForce = 1050;
            }


        Respawn();
    }

    // Runs every frame
    // Detect Input and update animation here.
    private void Update() {
        movementX = Input.GetAxisRaw("Horizontal");
        SetFacingDirection();

        if (health.currentHealth == 0)
        {
            Physics2D.IgnoreLayerCollision(7,8, true);
            rToRespawn = true;
            Timer.TimerOn = false;
        }

        // Respawns player upon pressing r to the last checkpoint
        if(Input.GetKeyDown(KeyCode.R) && rToRespawn == true)
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
        
        //Timer Countdown for Speed Power Up.
        if (powerUp)
        {
            powerUpTimer += Time.deltaTime;
        }
        if (powerUpTimer > powerUpTime)
        {
            powerUpTimer = 0;
            powerUp = false;
            //Resets back to speed upon character selection
            if (characterSelected == 1)
            {
                movementSpeed = 11f;
            }
            else
            {
                movementSpeed = 8;
            }
        }
        
        //Timer Countdown for Turtle Enemy.
        if (slowMovement)
        {
            slowMovementTimer += Time.deltaTime;
        }
        if (slowMovementTimer > slowMovementTime)
        {
            slowMovementTimer = 0;
            slowMovement = false;
            //Resets speed upon character selection
            if (characterSelected == 1)
            {
                movementSpeed = 11f;
            }
            else
            {
                movementSpeed = 8;
            }
        }

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
                    //does not allow high wall jumps for high jump character
                    if (characterSelected == 2)
                    {
                        jumpForce = 850;
                    }
                    currentwallJumpVelocity = initialwallJumpVelocity;
                }
                else if ((movementX > 0.01f) && (isWallSliding) && (Input.GetButtonDown("Jump"))) //facing right
                {
                    //does not allow high wall jumps for high jump character
                    if (characterSelected == 2)
                    {
                        jumpForce = 850;
                    }
                    currentwallJumpVelocity = -initialwallJumpVelocity;
                }

                //determines if the player has picked up a JumpPowerUpCollectable and allows the player to double jump
                if (doubleJumpActive && timesJumped > 0 && !isGrounded)
                {
                    doubleJumpCount--;
                    doubleJumpActive = false;
                }
                
            }
            //changes high jump power back to high if character is selected and not wall sliding
            if ((!isWallSliding) && (characterSelected == 2))
            {
                jumpForce = 1050;
            }
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
            {
                transform.localScale = Vector3.one;
  
            }
            else if (movementX < -0.01f)
            {
                transform.localScale = new Vector3(-1, 1, 1);

            }
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
        //Adding to health if HealthCollectable is picked up when health character is not selected
        if (other.gameObject.CompareTag("HealthCollectable") && health.currentHealth < 3 && characterSelected != 3)
        {
            SoundManager.instance.PlaySound(collectSound);
            Destroy(other.gameObject);
            health.IncreaseHealth(1);
        }
        //Adding to health if HealthCollectable is picked up when health character is selected
        if (other.gameObject.CompareTag("HealthCollectable") && health.currentHealth < 4 && characterSelected == 3)
        {
            SoundManager.instance.PlaySound(collectSound);
            Destroy(other.gameObject);
            health.IncreaseHealth(1);
        }
        //Players speed increases if a PowerUpCollectable is picked up
        if (other.gameObject.CompareTag("PowerUpCollectable"))
        {
            if (!powerUp)
            {
                SoundManager.instance.PlaySound(collectSound);
                Destroy(other.gameObject);
                movementSpeed = 13;
                powerUp = true;
                powerUpTime = 5.0f;
            }
        }
        //Player is able to double jump if JumpPowerUpCollectable is picked up
        if (other.gameObject.CompareTag("JumpPowerUpCollectable"))
        {
            SoundManager.instance.PlaySound(collectSound);
            Destroy(other.gameObject);
            doubleJumpActive = true;
            doubleJumpCount = 1;
        }
        //Player runs into Turtle Enemy and is slowed down for a duration of time
        if (other.gameObject.CompareTag("TurtleEnemy"))
        {
            if (!slowMovement)
            {
                GetHit();
                movementSpeed = 4;
                slowMovement = true;
                slowMovementTime = 5.0f;
            }
        }
        //Unlocks Checkpoint
        if (other.gameObject.CompareTag("Checkpoint"))
        {
            SoundManager.instance.PlaySound(collectSound);
            checkPointUnlocked = true;
        }

        if (other.gameObject.CompareTag("Projectile"))
        {
            GetHit();
                movementSpeed = 4;
                slowMovement = true;
                slowMovementTime = 5.0f;
        }

        //Might not need to be used
        if (other.gameObject.CompareTag("LevelEnd"))
        {
            endLevel = true;
        }
        //Player stomps on an enemy and kills them.
        if (other.gameObject.CompareTag("StompArea"))
        {
            SoundManager.instance.PlaySound(jumpSound);
            ScoreScript.scoreValue += 10;
        }
        //Player bounces off of an object
        if (other.gameObject.CompareTag("BounceArea"))
        {
            SoundManager.instance.PlaySound(jumpSound);
        }
    }


    public void Respawn() 
    {
        if (checkPointUnlocked == false)
        {
            transform.SetPositionAndRotation(startLocation.position + new Vector3(0.5175f, 0.5175f, 0f), transform.rotation);
        }
        else
        {
            transform.SetPositionAndRotation(checkPointLocation.position + new Vector3(0.5175f, 0.5175f, 0f), transform.rotation);
        }

        Physics2D.IgnoreLayerCollision(6,7, false);
        Physics2D.IgnoreLayerCollision(7,8, false);
        playerMovementHelper.Respawn(animator, rb, ref canMove, gravityScale);
        rToRespawn = false;
    }

    public void GetHit()
    {
        playerMovementHelper.GetHit(animator);
    }

    public void SetCheckpointActive()
    {
        playerMovementHelper.SetCheckpointActive(ref checkPointUnlocked);
        PlayerPrefs.SetString("Checkpoint", "true");
    }

    public void SetCanMove(bool canMove)
    {
        playerMovementHelper.SetCanMove(ref this.canMove, canMove);
    }

    public void PlayerDeath()
    {
        playerMovementHelper.PlayerDeath(animator, rb, ref canMove);
    }
}

public class PlayerMovementHelper
{
    public void GetHit(Animator animator)
    {
        animator.SetTrigger("isHit");
    }

    public void SetCheckpointActive(ref bool checkPointUnlocked)
    {
        checkPointUnlocked = true;
    }

    public void SetCanMove(ref bool currentMovement, bool newMovement)
    {
        currentMovement = newMovement;
    }

    public void PlayerDeath(Animator animator, Rigidbody2D rb, ref bool canMove)
    {
        SetCanMove(ref canMove, false);
        animator.SetBool("isDead", true);
        animator.SetBool("isJumping", false);
        rb.gravityScale = 0f;
    }

    public void Respawn(Animator animator, Rigidbody2D rb, ref bool canMove, float gravityScale)
    {
        animator.SetBool("isDead", false);
        animator.SetTrigger("Appear");
        SetCanMove(ref canMove, true);
        rb.gravityScale = gravityScale;
    }
}

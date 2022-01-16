using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour {

    // Movement
    public CharacterController2D controller;
    public Animator animator;
    float horizontalMove = 0f;
    public float runSpeed;
    bool jump = false;
    public static bool crouch = false;
    public bool isGrounded = false;

    // Jump Delay
    public float jumpPressedRemember;
    public float jumpPressedRememberTime = 0.15f;
    public bool jumpPressedFlag;

    // Fall Delay
    public float isGroundedRemember;
    public float isGroundedRememberTime = 0.2f;

    // Jump Key Held
    public bool jumpKeyHeld;

    // Health System
    public int maxHealth = 3;
    public int currentHealth;
    public HealthBar healthBar;
    public static bool damaged;
    public GameObject playerGameObject;
    public static bool invulnerableWindow;
    public bool tick = true;
    public GameObject gameOverMenu;
    public GameObject plus1HealthPopUp;
    private bool fruitFlag;
    public bool turnOffInputs = false;

    // Gems
    public GameObject changingText;
    private bool gemPickedUpFlag;
    public Animator gemAnimator;
    public int gemInfo;

    // Other
    string sceneName;

    // Sound
    bool playerRun;
    public bool playerRunSoundPlayed;
    bool playerInvulnerableSoundPlayed;

    void Start() 
    {
        // Special changes for specific scenes
        sceneName = SceneManager.GetActiveScene().name;

        // Gems amount saved
        if (sceneName == "GameScene")
        {
            gemInfo = 0;
        } 
        else
        {
            gemInfo = PlayerPrefs.GetInt("gemPickedUpInfo");
            changingText.GetComponent<Text>().text = gemInfo.ToString();
        }

        // Health System
        invulnerableWindow = false;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        InvokeRepeating("BlinkingSpriteRenderer", 0f, 0.20f);

        // If player started level by using crouching input, he will stand up
        if (Input.GetButtonUp("Crouch") == true)
        {
            crouch = false;
        }
    }

    public void Update()
    {
        // Gems amount updating
        PlayerPrefs.SetInt("gemPickedUpInfo", gemInfo);

        // Other
        if (PauseMenuScript.gameIsEnding == false) 
        {
            if (sceneName == "BossScene")
            {
                if (playerGameObject.transform.position.y < -1)
                {
                    Invoke("TurnOnGameOverScreenAfterTime", 0.25f);
                }
            }
            else if (playerGameObject.transform.position.y < -20)
            {
                Invoke("TurnOnGameOverScreenAfterTime", 0.25f);
            }

            // Health System
            if (currentHealth == 0)
            {
                Invoke("TurnOnGameOverScreenAfterTime", 0.25f);
            }
        }
        
        if (currentHealth == 1)
        {
            if (sceneName != "BossScene")
                StartCoroutine(ConstantShake(1.40f));
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(1);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            SceneManager.LoadScene("BossCutscene");
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
                plus1HealthPopUp.SetActive(true);
                if (currentHealth < 3)
                {
                    currentHealth++;
                    healthBar.SetHealth(currentHealth);
                }
                Invoke("TurnOffPlus1HealthPopUpAfterTime", 2.25f);
        }

        // Movement
        animator.SetFloat("yVelocity", controller.yVelocity); // Float used to check if player is jumping or falling
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (PauseMenuScript.gameIsEnding)
        {
            runSpeed = 0;
            horizontalMove = 0;
        } 
        else if (PauseMenuScript.gameIsPaused == false && PauseMenuScript.gameIsEnding == false) 
        {
            if(turnOffInputs)
            {
                runSpeed = 0;
                horizontalMove = 0;
            } 
            else 
            {
                runSpeed = 35f;
                horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

                // Sound
                if (horizontalMove != 0 && isGrounded)
                {
                    playerRun = true;
                } 
                else
                {
                    playerRun = false;
                }

                if (gameOverMenu.activeInHierarchy == false)
                {
                    if (playerRun && crouch == false)
                    {
                        if (playerRunSoundPlayed == false)
                        {
                            playerRunSoundPlayed = true;
                            FindObjectOfType<AudioManager>().Loop("PlayerRun");
                            FindObjectOfType<AudioManager>().Play("PlayerRun");
                        }
                    }
                    else
                    {
                        playerRunSoundPlayed = false;
                        FindObjectOfType<AudioManager>().Stop("PlayerRun");
                    }
                } else
                {
                    FindObjectOfType<AudioManager>().Stop("PlayerRun");
                }
            }

            // GroundCheck taken from controller to check if player is touching the ground
            isGrounded = controller.GroundCheck(); 

            // Jump Delay
            jumpPressedRemember -= Time.deltaTime;

            if (Input.GetButtonDown("Jump"))
            {
                jumpPressedRemember = jumpPressedRememberTime;
            }
            
            // Fall Delay
            isGroundedRemember -= Time.deltaTime;

            if (isGrounded)
            {
                isGroundedRemember = isGroundedRememberTime;
            }

            // Jump
            if ((jumpPressedRemember > 0) && (isGroundedRemember > 0) && (jumpPressedFlag == false)) 
            {
                jumpPressedFlag = true;
                jumpPressedRemember = 0;
                isGroundedRemember = 0;
                animator.SetBool("JumpFallBool", true);
                jump = true;
            }

            if (Input.GetButtonDown("Crouch") == true)
            {
                crouch = true;
            }
            else if (Input.GetButtonUp("Crouch") == true)
            {
                crouch = false;
            }

            if (isGrounded == false && damaged == false)
            {
                // Blend animation turned on whenever player is not touching the ground
                animator.SetBool("JumpFallBool", true);
            } 
            else if (isGrounded == false && damaged == true)
            {
                animator.SetBool("JumpFallBool", false);
            }
        }
    }

    public void OnLanding ()
    {
        jumpPressedFlag = false;
        animator.SetBool("JumpFallBool", false); // Blend animation turned off whenever player is touching the ground
        SetDamagedAnimationToFalse();
    }

    public void OnCrouching (bool isCrouching)
    {
        animator.SetBool("IsCrouching", isCrouching);
    }

    public void FixedUpdate ()
    {
        if (PauseMenuScript.gameIsPaused == false) 
        {
            controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
            FindObjectOfType<AudioManager>().UnPause("PlayerInvulnerable");
            FindObjectOfType<AudioManager>().UnPause("GemPickup");
            FindObjectOfType<AudioManager>().UnPause("FruitPickup");
            FindObjectOfType<AudioManager>().UnPause("PlayerHit");
            FindObjectOfType<AudioManager>().UnPause("PlayerRun");
            jump = false;
        } else
        {
            playerRunSoundPlayed = false;
            FindObjectOfType<AudioManager>().Pause("PlayerInvulnerable");
            FindObjectOfType<AudioManager>().Pause("GemPickup");
            FindObjectOfType<AudioManager>().Pause("FruitPickup");
            FindObjectOfType<AudioManager>().Pause("PlayerHit");
            FindObjectOfType<AudioManager>().Pause("PlayerRun");
        }

        if (GameOverMenuScript.gameIsOver)
        {
            FindObjectOfType<AudioManager>().Stop("PlayerInvulnerable");
            FindObjectOfType<AudioManager>().Stop("GemPickup");
            FindObjectOfType<AudioManager>().Stop("FruitPickup");
            FindObjectOfType<AudioManager>().Stop("PlayerInvulnerable");
            FindObjectOfType<AudioManager>().Stop("PlayerRun");
        }
    }

    void TakeDamage(int damage)
    {
        if (PauseMenuScript.gameIsPaused == false && invulnerableWindow == false && PauseMenuScript.gameIsEnding == false) 
        {
            // Sound
            playerInvulnerableSoundPlayed = false;
            if (gameOverMenu.activeInHierarchy == false) 
            {
                FindObjectOfType<AudioManager>().Play("PlayerHit");
            }     
            turnOffInputs = true;
            damaged = true;

            // If player is taking hit from Boss he will be always pushed away, no matter the player localScale.x value
            if (sceneName != "BossScene")
            {
                StartCoroutine(ShortShake(.25f, 2.75f));
                if (playerGameObject.transform.localScale.x < 0)
                {
                    CharacterController2D.m_Rigidbody2D.AddForce(new Vector2(550, 550));
                }
                else
                {
                    CharacterController2D.m_Rigidbody2D.AddForce(new Vector2(-550, 550));
                }
            } else
            {
                CharacterController2D.m_Rigidbody2D.AddForce(new Vector2(550, 550));
            }

            animator.SetBool("Damaged", true);
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);
            Invoke("SetDamagedAnimationToFalse", 1);
            invulnerableWindow = true;
            Invoke("SetInvulnerableWindowToFalse", 2);
            Invoke("SetTurnOffInputsToFalse", 0.5f);
        }
    }

    public void BlinkingSpriteRenderer()
    {
        if (invulnerableWindow)
        {   
            playerGameObject.GetComponent<SpriteRenderer>().enabled = false;
            if (tick)
            {
                playerGameObject.GetComponent<SpriteRenderer>().enabled = false;
                tick = false;
            }
            else
            {
                playerGameObject.GetComponent<SpriteRenderer>().enabled = true;
                tick = true;
            }
            // Sound
            if (gameOverMenu.activeInHierarchy == false)
            {
                if (playerInvulnerableSoundPlayed == false)
                {
                    if (gameOverMenu.activeInHierarchy == false && PauseMenuScript.gameIsPaused == false)
                    {
                        playerInvulnerableSoundPlayed = true;
                        Invoke("PlayPlayerInvulnerableSound", 0.5f);
                    }
                }
            }
            else
            {
                playerInvulnerableSoundPlayed = false;
            }
        }
    }

    // Functions to invoke after specific time
    public void SetJumpPressedFlagToFalse()
    {
        jumpPressedFlag = false;
    }

    public void SetDamagedAnimationToFalse ()
    {
        animator.SetBool("Damaged", false);
        damaged = false;
    }

    public void SetInvulnerableWindowToFalse ()
    {
        invulnerableWindow = false;
        playerGameObject.GetComponent<SpriteRenderer>().enabled = true;
    }

    public void TurnOnGameOverScreenAfterTime()
    {
        gameOverMenu.SetActive(true);
    }

    public void TurnOffPlus1HealthPopUpAfterTime()
    {
        plus1HealthPopUp.SetActive(false);
        fruitFlag = false;
    }

    public void SetGemPickedUpFlagToFalse()
    {
        gemPickedUpFlag = false;
    }

    public void SetTurnOffInputsToFalse()
    {
        turnOffInputs = false;
    }

    public void PlayPlayerInvulnerableSound()
    {
        if (gameOverMenu.activeInHierarchy == false && PauseMenuScript.gameIsPaused == false)
            FindObjectOfType<AudioManager>().Play("PlayerInvulnerable");
    }

    // Other functions
    IEnumerator ConstantShake (float magnitude)
    {
        Vector3 originalPos = healthBar.transform.localPosition;

        while (currentHealth == 1 && PauseMenuScript.gameIsPaused == false) 
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            healthBar.transform.localPosition = new Vector3(originalPos.x+x, originalPos.y+y, originalPos.z);

            yield return null;
        } 

        healthBar.transform.localPosition = originalPos;
    }

    public IEnumerator ShortShake(float duration, float magnitude)
    {
        Vector3 originalPos = healthBar.transform.localPosition;

        float elapsed;

        if (PauseMenuScript.gameIsPaused == false)
        {
            elapsed = 0.0f;

            while (elapsed < duration)
            {
                float x = Random.Range(-1f, 1f) * magnitude;
                float y = Random.Range(-1f, 1f) * magnitude;

                healthBar.transform.localPosition = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z);

                elapsed += Time.deltaTime;

                yield return null;
            }
        }
        else
        {
            elapsed = duration;
        }

        healthBar.transform.localPosition = originalPos;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            TakeDamage(1);
        }

        if(other.gameObject.tag == "Fruit")
        {
            if (fruitFlag == false) 
            {
                // Sound
                if (gameOverMenu.activeInHierarchy == false)
                    FindObjectOfType<AudioManager>().Play("FruitPickup");
                fruitFlag = true;
                plus1HealthPopUp.SetActive(true);
                if (currentHealth < 3)
                {
                    currentHealth++;
                    healthBar.SetHealth(currentHealth);
                }
                Invoke("TurnOffPlus1HealthPopUpAfterTime", 2.25f);
                Destroy(other.gameObject);
            }   
        }

        if (other.gameObject.tag == "Gem")
        {
            if (gemPickedUpFlag == false)
            {
                // Sound
                if (gameOverMenu.activeInHierarchy == false)
                    FindObjectOfType<AudioManager>().Play("GemPickup");
                gemPickedUpFlag = true;
                gemInfo++;
                changingText.GetComponent<Text>().text = gemInfo.ToString();
                gemAnimator.SetTrigger("PickUp");
                Destroy(other.gameObject);
                Invoke("SetGemPickedUpFlagToFalse", 0.25f);
            }
        }
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if(invulnerableWindow == false && other.gameObject.tag == "Enemy")
        {
            TakeDamage(1);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("MovingPlatform"))
            this.transform.parent = collision.transform;
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("MovingPlatform"))
            this.transform.parent = null;
    }
}

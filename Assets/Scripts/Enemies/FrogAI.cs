using System.Collections.Generic;
using UnityEngine;

public class FrogAI : MonoBehaviour
{
    // Patrol:
    public List<Transform> points;
    public int nextID = 0;
    int idChangeValue = 1;
    public float speed = 3;

    // Jumping:
    [SerializeField] float jumpHeight = 3;
    [SerializeField] public Rigidbody2D frogRB;
    [SerializeField] float circleRadius = 0.1f;
    [SerializeField] Transform groundCheckPoint;
    [SerializeField] LayerMask collidableLayer;
    [SerializeField] public bool isGrounded;
    [SerializeField] public bool frogIsWaitingForJump;
    [SerializeField] public bool itIsWednesdayMyDudes;
    public float yVelocity;

    // Animations:
    public Animator animator;

    // Sound
    public bool soundFlag;
    public AudioSource audioSourceFrogIdle;
    public AudioSource audioSourceFrogJump;
    public Transform player;
    public float distanceToPlayer;

    private void Start()
    {
        isGrounded = false;
        frogIsWaitingForJump = false;
    }

    void FixedUpdate()
    {
        distanceToPlayer = Vector2.Distance(transform.position, player.position);

        animator.SetFloat("yVelocity", yVelocity);
        yVelocity = frogRB.velocity.y;
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, circleRadius, collidableLayer);

        if (!isGrounded)
        {
            Transform goalPoint = points[nextID];

            if (goalPoint.transform.position.x > transform.position.x)
            {
                transform.localScale = new Vector3(-4, 4, 1);
            }
            else
            {
                transform.localScale = new Vector3(4, 4, 1);
            }

            transform.position = Vector2.MoveTowards(transform.position, goalPoint.position, 2 * Time.deltaTime);

            if (Vector2.Distance(transform.position, goalPoint.position) < 2f)
                itIsWednesdayMyDudes = true;

            if (Vector2.Distance(transform.position, goalPoint.position) < 1f)
            {

                if (nextID == points.Count - 1)
                {
                    idChangeValue = -1;
                }
                if (nextID == 0)
                {
                    idChangeValue = 1;
                }
                nextID += idChangeValue;
            }
        }

        if (isGrounded == true)
        {
            if (itIsWednesdayMyDudes)
            {
                animator.SetBool("FrogRibbit", true);
                if (isGrounded && soundFlag == false && animator.GetBool("FrogRibbit") == true && yVelocity == 0) 
                {
                    if (GameOverMenuScript.gameIsOver == false && distanceToPlayer < 10 && animator.GetBool("Death") == false && PauseMenuScript.gameIsPaused == false)
                    {
                        audioSourceFrogIdle.Play();
                    }
                    soundFlag = true;
                    Invoke("SetSoundFlagToFalse", 4);
                }
                //speed = 0;
                //FrogIsGrounded();
                //FrogIsWaitingForJump = true;
            }
            else
            {
                animator.SetBool("FrogIdle", true);
                //speed = 0;
                //FrogIsGrounded();
                //FrogIsWaitingForJump = true;
            }
            speed = 0;
            FrogIsGrounded();
            frogIsWaitingForJump = true;
        }

        if (isGrounded == false)
        {
            animator.SetBool("FrogIdle", false);
            animator.SetBool("FrogRibbit", false);
        }

        // Sound
        if (PauseMenuScript.gameIsPaused)
        {
            audioSourceFrogIdle.Pause();
            audioSourceFrogJump.Pause();
        }
        else if (PauseMenuScript.gameIsPaused == false)
        {
            audioSourceFrogIdle.UnPause();
            audioSourceFrogJump.UnPause();
        }

        if (animator.GetBool("Death") == true || GameOverMenuScript.gameIsOver)
        {
            audioSourceFrogIdle.Stop();
            audioSourceFrogJump.Stop();
        }
    }

    public void FrogIsGrounded() 
    {
        if (isGrounded == true && frogIsWaitingForJump == true)
        {
            Invoke("FrogIsJumping", 2);
        }
    }

    public void FrogIsJumping()
    {
        if (isGrounded == true && frogIsWaitingForJump == true)
        {
            itIsWednesdayMyDudes = false;
            frogRB.AddForce(new Vector2(0f, jumpHeight), ForceMode2D.Impulse);
            frogIsWaitingForJump = false;
            // Sound
            if (GameOverMenuScript.gameIsOver == false && distanceToPlayer < 10 && animator.GetBool("Death") == false && PauseMenuScript.gameIsPaused == false)
                audioSourceFrogJump.Play();
        }
    }

    public void SetSoundFlagToFalse ()
    {
        soundFlag = false;
    }
}

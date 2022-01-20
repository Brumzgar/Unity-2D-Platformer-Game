using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogAI2 : MonoBehaviour
{
    // Patrol
    public List<Transform> points;
    public int nextID = 0;
    int idChangeValue = 1;
    public float speed = 3;

    // Jumping
    [SerializeField] float jumpHeight = 3;
    [SerializeField] public Rigidbody2D frogRB;
    [SerializeField] float circleRadius = 0.1f;
    [SerializeField] Transform groundCheckPoint;
    [SerializeField] LayerMask collidableLayer;
    [SerializeField] public bool isGrounded;
    [SerializeField] public bool itIsWednesdayMyDudes;
    public bool animFlag;
    public float yVelocity;

    // Animations
    public Animator animator;

    // Sound
    public AudioSource audioSourceFrogIdle;
    public AudioSource audioSourceFrogJump;
    public Transform player;
    public float distanceToPlayer;


    void Start()
    {
        animFlag = false;
        isGrounded = false;
        this.StartCoroutine(FrogJumps3TimesThenRests());
    }

    void FixedUpdate()
    {
        distanceToPlayer = Vector2.Distance(transform.position, player.position);
        animator.SetFloat("yVelocity", yVelocity);
        yVelocity = frogRB.velocity.y;
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, circleRadius, collidableLayer);

        if (isGrounded)
        {
            if (itIsWednesdayMyDudes)
            {
                if (isGrounded && GameOverMenuScript.gameIsOver == false && distanceToPlayer < 10 && animator.GetBool("Death") == false && PauseMenuScript.gameIsPaused == false && PauseMenuScript.gameIsEnding == false) // && yVelocity == 0
                {
                    animator.SetBool("FrogRibbit", true);
                    audioSourceFrogIdle.Play();
                }
                itIsWednesdayMyDudes = false;
            }
            else
            {
                animator.SetBool("FrogIdle", true);
            }

            speed = 0;
        }

        if (!isGrounded)
        {
            animator.SetBool("FrogIdle", false);
            animator.SetBool("FrogRibbit", false);

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

        FrogSoundsController();
    }

    public void FrogSoundsController()
    {
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

        if (animator.GetBool("Death") == true || GameOverMenuScript.gameIsOver || PauseMenuScript.gameIsEnding)
        {
            audioSourceFrogIdle.Stop();
            audioSourceFrogJump.Stop();
        }
    }

    IEnumerator FrogJumps3TimesThenRests()
    {
        while(true)
        {
            for (int i = 0; i < 3; i++)
            {
                 if (isGrounded)
                 {
                     yield return new WaitForSeconds(1);
                     frogRB.AddForce(new Vector2(0f, jumpHeight), ForceMode2D.Impulse);
                     if (GameOverMenuScript.gameIsOver == false && distanceToPlayer < 10 && animator.GetBool("Death") == false && PauseMenuScript.gameIsPaused == false && PauseMenuScript.gameIsEnding == false)
                         audioSourceFrogJump.Play();
                 }
            }

            yield return new WaitForSeconds(1);
            if (animFlag == false)
            {
                animFlag = true;
            } 
            else
            {
                itIsWednesdayMyDudes = true;
                animFlag = false;
            }
            yield return new WaitForSeconds(1);
        }
    }
}

using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EagleAI : MonoBehaviour
{
    // Patrol:
    public List<Transform> points;
    public int nextID = 0;
    int idChangeValue = 1;
    public bool enemySpotted = false;
    [SerializeField] private Collider2D PatrolCircleColliderDisabled;

    // Aggro:
    [SerializeField]
    public Transform player;
    [SerializeField]
    float aggroRange;
    public GameObject exclamationMark;
    public bool exclamationMarkFlag;
    public float distanceToPlayer;

    // Seeker
    public Transform target;
    public float speed = 450f;
    public float nextWaypointDistance = 1f;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    Path path;
    Seeker seeker;
    Rigidbody2D rb;

    // Animations
    public Animator animator;

    // Sound
    public PlayerScript playerScript;
    public AudioSource audioSourcePatrol;
    public AudioSource audioSourceChase;
    public bool eagleChaseSoundPlayed;
    public bool eaglePatrolSoundPlayed;

    void Start()
    {
        // Seeker
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("UpdatePath", 0f, .5f);
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
            seeker.StartPath(rb.position, target.position, OnPathComplete);                  
    }

    void OnPathComplete(Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void MoveToNextPoint()
    {
        Transform goalPoint = points[nextID];
        if (goalPoint.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(-7, 7, 1);
        }
        else
        {
            transform.localScale = new Vector3(7, 7, 1);
        }

        transform.position = Vector2.MoveTowards(transform.position, goalPoint.position, 3 * Time.deltaTime);

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

    void FixedUpdate()
    {
        if (PauseMenuScript.gameIsPaused)
        {
            audioSourceChase.Pause();
            audioSourcePatrol.Pause();
            FindObjectOfType<AudioManager>().Pause("EaglePlayerSpotted");
        }
        else
        {
            audioSourceChase.UnPause();
            audioSourcePatrol.UnPause();
            FindObjectOfType<AudioManager>().UnPause("EaglePlayerSpotted");
        }

        if (GameOverMenuScript.gameIsOver || animator.GetBool("Death") == true)
        {
            audioSourceChase.Stop();
            audioSourcePatrol.Stop();
            FindObjectOfType<AudioManager>().Stop("EaglePlayerSpotted");
        }

        if (animator.GetBool("Death") == false)
        {
            // Aggro:
            distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceToPlayer < aggroRange && enemySpotted == false)
            {
                enemySpotted = true;
                animator.SetBool("ChasingBool", true);
            }
            else if (distanceToPlayer > 1.5 * aggroRange && enemySpotted == true)
            {
                enemySpotted = false;
                animator.SetBool("ChasingBool", false);
            }

            // Chase
            if (enemySpotted == true)
            {
                // Chase Sound
                audioSourcePatrol.Stop();
                eaglePatrolSoundPlayed = false;
                EagleChasingSound();

                // Exclamation Mark Pop-up
                if (exclamationMarkFlag == false)
                {
                    exclamationMark.SetActive(true);
                    // Sound
                    if (playerScript.gameOverMenu.activeInHierarchy == false)
                    {
                        FindObjectOfType<AudioManager>().Play("EaglePlayerSpotted");
                    }
                    Invoke("TurnOffObjectAfterTime", 0.5f);
                    exclamationMarkFlag = true;
                }

                // Seeker
                PatrolCircleColliderDisabled.enabled = true;

                if (path == null)
                    return;

                if (currentWaypoint >= path.vectorPath.Count)
                {
                    reachedEndOfPath = true;
                    return;
                }
                else
                {
                    reachedEndOfPath = false;
                }

                Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
                Vector2 force = direction * 2 * speed * Time.deltaTime;

                rb.AddForce(force);

                float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

                if (distance < nextWaypointDistance)
                {
                    currentWaypoint++;
                }

                if (player.position.x < transform.position.x)
                {
                    transform.localScale = new Vector3(7, 7, 1);
                }
                else if (player.position.x > transform.position.x)
                {
                    transform.localScale = new Vector3(-7, 7, 1);
                }
            } else
            {
                // Sound Patrol
                audioSourceChase.Stop();
                eagleChaseSoundPlayed = false;
                EaglePatrolSound();

                // Other
                MoveToNextPoint();
                PatrolCircleColliderDisabled.enabled = false;
                exclamationMarkFlag = false;
            }
        }
    }

    public void TurnOffObjectAfterTime ()
    {
        exclamationMark.SetActive(false);
    }

    public void EaglePatrolSound ()
    {
        if (playerScript.gameOverMenu.activeInHierarchy == false && distanceToPlayer < 12)
        {
            if (eaglePatrolSoundPlayed == false)
            {
                audioSourcePatrol.loop = true;
                if (PauseMenuScript.gameIsPaused == false && GameOverMenuScript.gameIsOver == false)
                {
                    audioSourcePatrol.Play();
                    eaglePatrolSoundPlayed = true;
                }
            }
        }
        else if (playerScript.gameOverMenu.activeInHierarchy == false && distanceToPlayer > 12)
        {
            audioSourcePatrol.Stop();
            eaglePatrolSoundPlayed = false;
        }
    }

    public void EagleChasingSound()
    {
        if (playerScript.gameOverMenu.activeInHierarchy == false)
        {
            if (eagleChaseSoundPlayed == false)
            {
                audioSourceChase.loop = true;
                if (PauseMenuScript.gameIsPaused == false && GameOverMenuScript.gameIsOver == false)
                {
                    audioSourceChase.Play();
                    eagleChaseSoundPlayed = true;
                }
            }
        }
    }
}

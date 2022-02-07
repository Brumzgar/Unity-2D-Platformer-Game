using System.Collections;
using UnityEngine;
using Pathfinding;
using UnityEngine.SceneManagement;

public class BossAI : MonoBehaviour
{
    // Seeker
    public Transform target;
    public float speed = 200;
    public float nextWaypointDistance = 1f;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    Path path;
    Seeker seeker;
    Rigidbody2D rb;

    // Animations
    public Animator animator;

    // Boss Hit
    public Rigidbody2D playerRigidBody;
    public float hitForce = 40;
    public bool hitFlag;

    // Boss Damaged
    public float damageTime;
    public SpriteRenderer bossRenderer;

    // Boss Health
    public int bossHealth = 3;
    public int bossCurrentHealth;
    public BossHealthBar bossHealthBar;
    public Vector3 originalPos;

    // Boss Death
    public GameObject gameOverScreenCheck;

    void Start()
    {
        // Seeker
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("UpdatePath", 0f, .25f);

        // Boss Health
        bossCurrentHealth = bossHealth;
        bossHealthBar.SetBossMaxHealth(bossHealth);
        originalPos = bossHealthBar.transform.localPosition;

        // Sound
        FindObjectOfType<AudioManager>().Play("BossFlying");
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
            seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    public void Update()
    {
        // Ignore Platforms
        Physics2D.IgnoreLayerCollision(7, 10);
        Physics2D.IgnoreLayerCollision(11, 10);
    }

    public void BossTakeDamage(int bossDamage)
    {
        FindObjectOfType<AudioManager>().Stop("BossFlying");
        FindObjectOfType<AudioManager>().Play("BossHit");
        bossCurrentHealth -= bossDamage;
        bossHealthBar.SetBossHealth(bossCurrentHealth);
        StartCoroutine(ShortShake(1, 5f));
        StartCoroutine(Damage());
        animator.SetTrigger("Damaged");
        Invoke("PlayBossFlyingSoundAfterAnim", 1);
    }

    public IEnumerator Damage()
    {
        Color originalColor = bossRenderer.color;
        WaitForSeconds wait = new WaitForSeconds(damageTime);
        bossRenderer.color = new Color32(255, 0, 0, 255);
        yield return wait;
        bossRenderer.color = originalColor;
    }

    void FixedUpdate()
    {
        if (PauseMenuScript.gameIsPaused == false)
        {
            FindObjectOfType<AudioManager>().UnPause("BossFlying");
            FindObjectOfType<AudioManager>().UnPause("BossHit");
        }
        else
        {
            FindObjectOfType<AudioManager>().Pause("BossFlying");
            FindObjectOfType<AudioManager>().Pause("BossHit");
        }

        if (GameOverMenuScript.gameIsOver)
        {
            FindObjectOfType<AudioManager>().Stop("BossFlying");
            FindObjectOfType<AudioManager>().Stop("BossHit");
        }

        if (bossCurrentHealth <= 0)
        {
            Invoke("LoadGameOverSceneAfterAnim", 1);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            BossTakeDamage(3);
        }

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
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && hitFlag == false)
        {
            hitFlag = true;
            playerRigidBody.AddForce(transform.right * hitForce, ForceMode2D.Impulse);
            Invoke("SetHitFlagToFalse", 1f);
        }
    }

    public void SetHitFlagToFalse()
    {
        hitFlag = false;
    }

    public IEnumerator ShortShake(float duration, float magnitude)
    {
        float elapsed;

        if (PauseMenuScript.gameIsPaused == false)
        {
            elapsed = 0.1f;

            while (elapsed < duration && PauseMenuScript.gameIsPaused == false)
            {
                float x = Random.Range(-1f, 1f) * magnitude;

                bossHealthBar.transform.localPosition = new Vector2(bossHealthBar.transform.localPosition.x + x, bossHealthBar.transform.localPosition.y);

                elapsed += Time.deltaTime;

                yield return null;
            }
        } 
        else
        {
            elapsed = duration;
        }

        bossHealthBar.transform.localPosition = originalPos;
    }

    public void LoadGameOverSceneAfterAnim()
    {
        if (gameOverScreenCheck.activeInHierarchy == false)
            SceneManager.LoadScene("GameOverScene");
    }

    public void PlayBossFlyingSoundAfterAnim()
    {
        if (PauseMenuScript.gameIsPaused == false)
        {
            FindObjectOfType<AudioManager>().Play("BossFlying");
        }
    }

}

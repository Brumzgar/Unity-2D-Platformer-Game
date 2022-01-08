using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    public int enemyHp;
    private int currentHp;
    public GameObject enemyColliderTrigger;
    public GameObject hurtbox;
    public Collider2D enemyCollider;
    private Animator enemyAnimator;
    public static bool enemyIsDead;

    void Start()
    {
        currentHp = enemyHp;
        enemyAnimator = transform.parent.GetComponent<Animator>();
    }

    void Update()
    {
        if(GameOverMenuScript.gameIsOver)
        {
            FindObjectOfType<AudioManager>().Stop("EnemyDeath");
        }

        if(currentHp <= 0)
        {
            FindObjectOfType<AudioManager>().Play("EnemyDeath");
            enemyIsDead = true;
            enemyAnimator.SetBool("Death", enemyIsDead);
            enemyColliderTrigger.SetActive(false);
            hurtbox.SetActive(false);
            enemyCollider.enabled = false;
            Invoke("DestroyEnemyAfterDeathAnimation", 0.5f);
        }

        if(PauseMenuScript.gameIsPaused)
        {
            FindObjectOfType<AudioManager>().Pause("EnemyDeath");
        } 
        else
        {
            FindObjectOfType<AudioManager>().UnPause("EnemyDeath");
        }
    }

    public void TakeDamage(int damage)
    {
        currentHp -= damage;
    }

    public void DestroyEnemyAfterDeathAnimation()
    {
        Destroy(transform.parent.gameObject);
    }
}

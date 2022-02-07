using UnityEngine;
using UnityEngine.SceneManagement;

public class OpossumAI : MonoBehaviour
{
    // Patrol
    public float speed;
    public float distance;
    private bool movingLeft = true;
    public Transform groundDetection;
    public bool groundInfoBlocked = false;
    public bool playerTouched = false;

    // Sound
    public Animator animator;
    public Transform player;
    bool opossSoundPlayed;
    public AudioSource opossAudioSource;
    float distanceToPlayer;

    // Scene
    string sceneName;

    public void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
    }

    void FixedUpdate()
    {
        // Sound
        if (GameOverMenuScript.gameIsOver == false && distanceToPlayer < 10 && animator.GetBool("Death") == false && PauseMenuScript.gameIsPaused == false && PauseMenuScript.gameIsEnding == false)
        {
            if (opossSoundPlayed == false)
            {
                opossSoundPlayed = true;
                opossAudioSource.loop = true;
                opossAudioSource.Play();
            }
        }
        else
        {
            opossSoundPlayed = false;
            opossAudioSource.Stop();
        }

        if (GameOverMenuScript.gameIsOver || PauseMenuScript.gameIsEnding)
            opossAudioSource.Stop();

        distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (PauseMenuScript.gameIsPaused == true)
        {
            opossAudioSource.Pause();
        }
        else if (PauseMenuScript.gameIsPaused == false)
        {
            opossAudioSource.UnPause();
        }

        if (animator.GetBool("Death") == true)
        { 
            opossAudioSource.Stop();
        }
    }

    void Update()
    {   
        // Patrol
        transform.Translate(Vector2.left * speed * Time.deltaTime);
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);

        if(groundInfo.collider == false && groundInfoBlocked == false)
        {
            if(movingLeft == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingLeft = false;
            } else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingLeft = true;
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && playerTouched == false)
        {
            playerTouched = true;

            if (movingLeft == true)
            {
                if (transform.position.x > player.position.x)
                {
                    transform.eulerAngles = new Vector3(0, -180, 0);
                    movingLeft = false;
                }     
            }
            else
            {
                if (transform.position.x < player.position.x)
                {
                    if (sceneName != "BossScene")
                    {
                        transform.eulerAngles = new Vector3(0, 0, 0);
                        movingLeft = true;
                    }
                }    
            }

            Invoke("SetPlayerTouchedToFalse", 0.2f);
        }
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && playerTouched == false)
        {
            playerTouched = true;

            if (movingLeft == true)
            {
                if (transform.position.x > player.position.x)
                {
                    transform.eulerAngles = new Vector3(0, -180, 0);
                    movingLeft = false;
                }
            }
            else
            {
                if (transform.position.x < player.position.x)
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    movingLeft = true;
                }
            }

            Invoke("SetPlayerTouchedToFalse", 0.2f);
        }
    }

    public void SetPlayerTouchedToFalse()
    {
        playerTouched = false;
    }
}

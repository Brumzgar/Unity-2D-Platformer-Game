using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpecialPlatformScript : MonoBehaviour
{
    public bool platformFallingSoundFlag;
    public bool platformTouchedSoundFlag;
    public bool platformTouched;
    public GameObject cameraToTurnOff;
    public Transform playerTransform;
    public Animator gameOverAnimations;
    public Animator playerAnimator;
    bool flag;
    public PlayerScript playerScript;

    void Start()
    {
        platformFallingSoundFlag = false;
        platformTouchedSoundFlag = false;
        platformTouched = false;
    }

    public void FixedUpdate()
    {
        if (playerTransform.position.y <= -28)
        {
            cameraToTurnOff.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {    
            if (flag == false)
            {
                // Music
                FindObjectOfType<AudioManager>().Stop("GameSceneMusic");
                FindObjectOfType<AudioManager>().Stop("GamePausedMusic");

                // Shake
                StartCoroutine(PlatformShake(1.5f, 0.5f));

                // Player model reaction
                Invoke("PlayerModelOnPlatform", 0.375f);

                // Fall
                Invoke("DropPlatformAfterTime", 1f);

                // Turned off Controls
                PauseMenuScript.gameIsEnding = true;
                         
                // Other
                Invoke("GameOverAnimationTrigger", 2.75f);
                Invoke("TransitionToBossCutscene", 3.75f);

                flag = true;
            }
        }
    }

    public IEnumerator PlatformShake(float duration, float magnitude)
    {
        Vector3 originalPos = gameObject.transform.localPosition;

        float elapsed = 0.1f;

        if (platformTouchedSoundFlag == false)
        {
            platformTouchedSoundFlag = true;
            FindObjectOfType<AudioManager>().Play("PlatformTouched");
        }

        while (elapsed < duration)
        {
            float x = Random.Range(-0.25f, 0.25f) * magnitude;

            if (PauseMenuScript.gameIsPaused == false)
                gameObject.transform.localPosition = new Vector3(originalPos.x + x, originalPos.y, originalPos.z) * (0.5f / elapsed);
            else
                gameObject.transform.localPosition = originalPos;

            elapsed += Time.deltaTime;

            yield return null;
        }

        gameObject.transform.localPosition = originalPos;
    }

    public IEnumerator DropPlatform()
    {
        float y = 0.01f;

        Vector2 platformPos = gameObject.transform.localPosition;

        if (platformFallingSoundFlag == false)
        {
            platformFallingSoundFlag = true;
            Invoke("PlayPlatformFallingSound", 0.25f);
        }

        while (platformTouched == true)
        {
            playerAnimator.SetBool("JumpFallBool", true);

            gameObject.transform.localPosition = new Vector2(0, platformPos.y - y);

            yield return null;

            y = y + 0.5f;
        }
    }

    public void DropPlatformAfterTime()
    {
        platformTouched = true;
        StartCoroutine(DropPlatform());
    }

    public void GameOverAnimationTrigger()
    {
        gameOverAnimations.SetTrigger("End");
    }

    public void TransitionToBossCutscene()
    {
        SceneManager.LoadScene("BossCutscene");
    }

    public void PlayPlatformFallingSound()
    {
        FindObjectOfType<AudioManager>().Play("PlatformFalling");
    }

    public void PlayerModelOnPlatform()
    {
        CharacterController2D.m_Rigidbody2D.AddForce(new Vector2(0, 550));
        playerScript.animator.SetBool("Damaged", true);
        FindObjectOfType<AudioManager>().Play("PlayerHit");
    }
}

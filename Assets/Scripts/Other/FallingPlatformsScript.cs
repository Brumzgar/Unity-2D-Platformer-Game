using System.Collections;
using UnityEngine;

public class FallingPlatformsScript : MonoBehaviour
{
    public bool platformTouched;
    public bool bossDamaged;
    public BossAI boss;
    public Transform bossPosition;
    public Transform distanceCheckPosition;
    public float distanceToBoss;
    bool platformTouchedSoundFlag;
    bool platformFallingSoundFlag;

    void Start()
    {
        platformFallingSoundFlag = false;
        platformTouchedSoundFlag = false;
        bossDamaged = false;
        platformTouched = false;
    }

    public void FixedUpdate()
    {
        if (PauseMenuScript.gameIsPaused == false)
        {
            FindObjectOfType<AudioManager>().UnPause("PlatformFalling");
            FindObjectOfType<AudioManager>().UnPause("PlatformTouched");
        }
        else
        {
            FindObjectOfType<AudioManager>().Pause("PlatformFalling");
            FindObjectOfType<AudioManager>().Pause("PlatformTouched");
        }

        if (GameOverMenuScript.gameIsOver)
        {
            FindObjectOfType<AudioManager>().Stop("PlatformFalling");
            FindObjectOfType<AudioManager>().Stop("PlatformTouched");
        }

        distanceToBoss = Vector2.Distance(distanceCheckPosition.position, bossPosition.position);

        if (bossDamaged == false && platformTouched == true && distanceToBoss < 4)
        {
            boss.BossTakeDamage(1);
            bossDamaged = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals ("Player"))
        {
            // Shake
            StartCoroutine(PlatformShake(1.5f, 0.5f));

            // Fall
            Invoke("DropPlatformAfterTime", 1.5f);
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
        float y = 0.25f;

        Vector2 platformPos = gameObject.transform.localPosition;

        if (platformFallingSoundFlag == false)
        {
            platformFallingSoundFlag = true;
            FindObjectOfType<AudioManager>().Play("PlatformFalling");
        }

        while (platformTouched == true) 
        {
            if (PauseMenuScript.gameIsPaused == false)
                gameObject.transform.localPosition = new Vector2(0, platformPos.y - y);

            yield return null;

            if (PauseMenuScript.gameIsPaused == false)
                y = y + 0.025f;
        }
    }

    public void DropPlatformAfterTime()
    {
        platformTouched = true;
        StartCoroutine(DropPlatform());
    }

}

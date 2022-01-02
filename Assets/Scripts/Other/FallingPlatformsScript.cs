using System.Collections;
using UnityEngine;

public class FallingPlatformsScript : MonoBehaviour
{
    //Rigidbody2D rb;
    public bool platformTouched;
    public bool bossDamaged;
    public BossAI boss;
    public Transform bossPosition;
    public Transform distanceCheckPosition;
    public float distanceToBoss;


    void Start()
    {
        bossDamaged = false;
        platformTouched = false;
        //rb = GetComponent<Rigidbody2D>();
    }

    public void FixedUpdate()
    {
        distanceToBoss = Vector2.Distance(distanceCheckPosition.position, bossPosition.position);

        if (bossDamaged == false && platformTouched == true && distanceToBoss < 3.75)
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
            StartCoroutine(PlatformShake(1.5f, 0.25f));

            // Fall
            Invoke("DropPlatformAfterTime", 1.5f);

            // Destroy
            Destroy(gameObject, 4f);
        }
    }
    public IEnumerator PlatformShake(float duration, float magnitude)
    {
        Vector3 originalPos = gameObject.transform.localPosition;

        float elapsed = 0.1f;

        while (elapsed < duration)
        {
            float x = Random.Range(-0.25f, 0.25f) * magnitude;

            gameObject.transform.localPosition = new Vector3(originalPos.x + x, originalPos.y, originalPos.z); // * (0.5f / elapsed); // sprawdzic czy jak platforma jest bardzo daleko na osi x to czy nie glupieje

            elapsed += Time.deltaTime;

            yield return null;
        }

        gameObject.transform.localPosition = originalPos;
    }

    public IEnumerator DropPlatform()
    {
        float y = 0.25f;

        Vector2 platformPos = gameObject.transform.localPosition;

        while (platformTouched == true) 
        {
            gameObject.transform.localPosition = new Vector2(0, platformPos.y - y);

            yield return null;

            y = y + 0.025f;
        }
    }

    public void DropPlatformAfterTime()
    {
        platformTouched = true;
        StartCoroutine(DropPlatform());
    }

}

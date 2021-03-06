using UnityEngine;

public class MovingPlatformScript : MonoBehaviour
{
    public float moveSpeed = 3f;
    public bool moveRight = true;
    public Vector3 originalPosition;

    public void Start()
    {
        Vector3 originalPosition = transform.localPosition;
    }

    void FixedUpdate()
    {
        if (transform.position.x + originalPosition.x > 4f)
            moveRight = false;
        if (transform.position.x + originalPosition.x < -4f)
            moveRight = true;

        if (moveRight)
            transform.position = new Vector2(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y);
        else
            transform.position = new Vector2(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y);
    }
}

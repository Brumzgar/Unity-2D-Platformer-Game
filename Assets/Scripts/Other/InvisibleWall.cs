using UnityEngine;

public class InvisibleWall : MonoBehaviour
{
    public GameObject gameOverCanvas;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            gameOverCanvas.SetActive(true);
        }
    }
}

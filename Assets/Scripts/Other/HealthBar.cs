using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public GameObject healthBar;
    public Gradient gradient;
    public Image fill;
    string sceneName;
    public Vector3 offset;
    public GameObject player;

    public void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
    }

    public void SetMaxHealth (int health) // All scripts are public so we can call them from other scripts
    {
        slider.maxValue = health;
        slider.value = health;

        fill.color = gradient.Evaluate(1f); // Green hp at full hp
    }

    public void SetHealth(int health)
    {
        slider.value = health;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void Update()
    {
        if (sceneName == "BossScene" )
        {
            healthBar.transform.localScale = new Vector3(2, 2, 1);
            healthBar.transform.position = Camera.main.WorldToScreenPoint(player.transform.position + offset);
        }
    }
}

using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    public GameObject bossHealthBar;
    public GameObject player;
    public Vector3 offset;
    public Slider bossSlider;

    public void SetBossMaxHealth(int bossHealth)
    {
        bossSlider.maxValue = bossHealth;
        bossSlider.value = bossHealth;
    }

    public void SetBossHealth(int bossHealth)
    {
        bossSlider.value = bossHealth;
    }
}

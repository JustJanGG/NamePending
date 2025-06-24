using UnityEngine;
using UnityEngine.UIElements;

public class HealthBar : MonoBehaviour
{
    private PlayerStats playerStats;

    public GameObject[] healthBarImage;
    void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        UpdateHealthBar();
    }

    public void UpdateHealthBar()
    {
        for (int i = 0; i < healthBarImage.Length; i++)
        {
            healthBarImage[i].SetActive(false);

        }
        for (int i = 0; i < playerStats.health; i++)
        {
            healthBarImage[i].SetActive(true);
        }
    }
}

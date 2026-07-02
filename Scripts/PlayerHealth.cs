using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public Slider healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (healthBar != null) healthBar.value = currentHealth;

        if (currentHealth <= 0) Die();
    }

    void Die()
    {
        if (GameManager.instance == null)
        {
            Debug.LogError("GameManager is NULL!");
            SceneManager.LoadScene("GameOver");
            return;
        }

        Debug.Log("Wave: " + GameManager.instance.wave);
        Debug.Log("Score: " + GameManager.instance.score);

        PlayerPrefs.SetInt("FinalScore", GameManager.instance.score);
        PlayerPrefs.SetInt("FinalWave", GameManager.instance.wave);
        PlayerPrefs.Save();
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameOver");
    }
}

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Text waveText;
    public Text scoreText;
    public EnemySpawner spawner;

    public int score = 0;
    public int wave = 1;
    public int killsToNextWave = 10;
    private int killsThisWave = 0;

    void Awake()
    {
        instance = this;
    }

    public void EnemyKilled()
    {
        score++;
        killsThisWave++;
        scoreText.text = "Score: " + score;

        if (killsThisWave >= killsToNextWave)
        {
            wave++;
            killsThisWave = 0;
            killsToNextWave += 5; // keeps going forever, no cap
            waveText.text = "Wave: " + wave;
            spawner.OnNewWave(wave);
        }
    }

    public void GameOver()
    {
        // Save final score and wave before switching scenes
        PlayerPrefs.SetInt("FinalScore", score);
        PlayerPrefs.SetInt("FinalWave", wave);
        PlayerPrefs.Save();
        SceneManager.LoadScene("GameOver");
    }
}
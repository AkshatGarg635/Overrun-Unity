using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public Text finalScoreText;
    public Text finalWaveText;

    void Start()
    {
        // Grab the saved score and wave from PlayerPrefs
        finalScoreText.text = "Score: " + PlayerPrefs.GetInt("FinalScore", 0);
        finalWaveText.text = "Wave Reached: " + PlayerPrefs.GetInt("FinalWave", 1);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("StarterScene");
    }
}
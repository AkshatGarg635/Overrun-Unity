using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    public AudioSource clickSound;
    public AudioSource menuMusic;
    public GameObject settingsPanel;
    public Slider musicSlider;
    public Toggle sfxToggle;

    void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
        sfxToggle.isOn = PlayerPrefs.GetInt("SFXOn", 1) == 1;

        menuMusic.volume = PlayerPrefs.GetFloat("MusicVolume", 1f);

        settingsPanel.SetActive(false);
    }

    public void PlayGame()
    {
        if (PlayerPrefs.GetInt("SFXOn", 1) == 1)
            StartCoroutine(LoadWithSound("MainScene"));
        else
            SceneManager.LoadScene("MainScene");
    }

    public void QuitGame()
    {
        if (PlayerPrefs.GetInt("SFXOn", 1) == 1)
            StartCoroutine(QuitWithSound());
        else
        {
            UnityEditor.EditorApplication.isPlaying = false;
            Application.Quit();

        }
    }

    public void OpenSettings()
    {
        PlayClick();
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        PlayClick();
        settingsPanel.SetActive(false);
    }

    public void OnMusicSlider()
    {
        float volume = musicSlider.value;
        PlayerPrefs.SetFloat("MusicVolume", volume);
        menuMusic.volume = volume;
    }

    public void OnSFXToggle()
    {
        PlayerPrefs.SetInt("SFXOn", sfxToggle.isOn ? 1 : 0);
    }

    void PlayClick()
    {
        if (PlayerPrefs.GetInt("SFXOn", 1) == 1)
            clickSound.Play();
    }

    IEnumerator LoadWithSound(string sceneName)
    {
        StartCoroutine(FadeOut(menuMusic, 0.2f));
        yield return new WaitForSeconds(0.1f);
        clickSound.Play();
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene(sceneName);
    }

    IEnumerator QuitWithSound()
    {
        StartCoroutine(FadeOut(menuMusic, 0.2f));
        yield return new WaitForSeconds(0.1f);
        clickSound.Play();
        yield return new WaitForSeconds(0.2f);
        Application.Quit();
    }

    IEnumerator FadeOut(AudioSource audio, float duration)
    {
        float startVolume = audio.volume;
        while (audio.volume > 0)
        {
            audio.volume -= startVolume * Time.deltaTime / duration;
            yield return null;
        }
        audio.Stop();
        audio.volume = startVolume;
    }
}
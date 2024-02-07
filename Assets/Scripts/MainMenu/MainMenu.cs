using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class MainMenu : MonoBehaviour
{
    public int freeRunIndex = 1;

    public GameObject baseButtons;
    public GameObject optionButtons;

    public List<AudioSource> masterSound;
    public Slider volumeSlider;
    public AudioSource musicSound;
    public Slider musicSlider;
    public Slider graphicsSlider;

    public PostProcessVolume profile;

    private void Start()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("volume", 1);
        musicSlider.value = PlayerPrefs.GetFloat("music", 1);
        graphicsSlider.value = PlayerPrefs.GetFloat("graphics", 1);
    }

    public void GoToFreeRun()
    {
        SceneManager.LoadScene(freeRunIndex);
    }

    public void LeaveGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

    public void HideOptions()
    {
        baseButtons.SetActive(true);
        optionButtons.SetActive(false);
    }

    public void ShowOptions()
    {
        baseButtons.SetActive(false);
        optionButtons.SetActive(true);
    }

    public void ChangeVolume()
    {
        foreach (AudioSource sound in masterSound)
        {
            sound.volume = volumeSlider.value;
        }
    }

    public void ChangeGraphics()
    {
        profile.weight = graphicsSlider.value;
    }

    public void ChangeMusicVolume()
    {
        musicSound.volume = musicSlider.value;
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetFloat("volume", volumeSlider.value);
        PlayerPrefs.SetFloat("music", musicSlider.value);
        PlayerPrefs.SetFloat("graphics", graphicsSlider.value);
        PlayerPrefs.Save();
    }
}
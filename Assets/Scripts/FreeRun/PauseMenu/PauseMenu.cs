using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;


public class PauseMenu : MonoBehaviour
{
    private static PauseMenu _instance;

    public static PauseMenu Instance { get { return _instance; } }


    [SerializeField]
    UIMenu pauseMenu;

    public KeyCode pauseButton;
    public SwapCharacter activePlayer;
    public Image pausePanel;

    public List<AudioSource> masterSound;
    public Slider volumeSlider;
    public AudioSource musicSound;
    public Slider musicSlider;
    public Slider graphicsSlider;

    public LineRender lr;

    public PostProcessVolume profile;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    // Use this for initialization
    void Start()
    {
        Time.timeScale = 1;
        HidePaused();

        volumeSlider.value = PlayerPrefs.GetFloat("volume", 1);
        musicSlider.value = PlayerPrefs.GetFloat("music", 1);
        graphicsSlider.value = PlayerPrefs.GetFloat("graphics", 1);
    }

    private void Update()
    {
        if (Input.GetKeyDown(pauseButton))
        {
            if (Time.timeScale == 1)
            {
                ShowPaused(false);
            }
            else if (Time.timeScale == 0)
            {
                HidePaused();
            }
        }


        if (lr.timeRemaining <=0.1f)
        {
            ShowPaused(true);
        }
    }

    public void JoinOptions()
    {
        pauseMenu.pauseMenu.SetActive(false);
        pauseMenu.optionsMenu.SetActive(true);
    }
    public void LeaveOptions()
    {
        pauseMenu.optionsMenu.SetActive(false);
        pauseMenu.pauseMenu.SetActive(true);
    }

    public void HidePaused()
    {
        Time.timeScale = 1;
        pauseMenu.backPanel.SetActive(false);

        activePlayer.currentPlayer.GetComponent<FPS_Behaviour>().canMove = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void ShowPaused(bool gameOver)
    {
        Time.timeScale = 0;
        pausePanel.color = activePlayer.colorDisplay.color;

        pauseMenu.backPanel.SetActive(true);
        if (gameOver)
        {
            pauseMenu.playButton.SetActive(false);
            pauseMenu.pauseText.gameObject.SetActive(true);
            pauseMenu.pauseHeader.SetText("Game Over");
        }
        else
        {
            pauseMenu.playButton.SetActive(true);
            pauseMenu.pauseText.gameObject.SetActive(false);
            pauseMenu.pauseHeader.SetText("Paused");
        }


        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        activePlayer.currentPlayer.GetComponent<FPS_Behaviour>().canMove = false;
    }

    public void RestartCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LeaveScene()
    {
        SceneManager.LoadScene(0);
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

    public void ColourUpdate()
    {
        pausePanel.color = activePlayer.colorDisplay.color;
    }
}

[System.Serializable]
class UIMenu
{
    public GameObject backPanel;
    public GameObject optionsMenu;
    public GameObject pauseMenu;
    public TMPro.TextMeshProUGUI pauseText;
    public TMPro.TextMeshProUGUI pauseHeader;
    public GameObject playButton;
}

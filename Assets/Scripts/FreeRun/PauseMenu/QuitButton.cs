using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class QuitButton : MonoBehaviour
{
    public void OnButtonPress()
    {
        SceneManager.LoadScene(sceneName: "Main");
    }
}

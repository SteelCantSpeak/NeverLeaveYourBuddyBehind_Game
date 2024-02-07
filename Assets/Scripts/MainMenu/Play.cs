using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class Play : MonoBehaviour
{

    public void OnButtonPress()
    {
        SceneManager.LoadScene(sceneBuildIndex: 1);
    }
}

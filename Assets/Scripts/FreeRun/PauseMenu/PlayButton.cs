using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayButton : MonoBehaviour
{
    [SerializeField]
    PauseMenu menu;

    private void Start()
    {
        menu = GetComponentInParent<PauseMenu>();
    }

    public void OnButtonPress()
    {
        Time.timeScale = 1;
        menu.HidePaused();
        
    }
}

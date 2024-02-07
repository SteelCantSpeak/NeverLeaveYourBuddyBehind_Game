using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandleColour : MonoBehaviour
{
    [Header("Player One")]
    public Image handle;
    public Gradient colour;
    public float initialColour;
    public float colourValue;
    public Slider colourSlider;

    // Start is called before the first frame update
    void Start()
    {
        colourSlider.value = initialColour;
        colourValue = PlayerPrefs.GetFloat("PlayerColour", initialColour);
        handle.color = colour.Evaluate(colourValue);
    }

    // Update is called once per frame
    public void OnColourChange()
    {
        colourValue = colourSlider.value;
        handle.color = colour.Evaluate(colourValue);
    }

    public void OnDestroy()
    {
        PlayerPrefs.GetFloat("PlayerColour", colourValue);
        PlayerPrefs.Save();
    }
}

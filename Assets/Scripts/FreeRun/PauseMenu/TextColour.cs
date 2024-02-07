using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TextColour : MonoBehaviour
{
    public TextMeshProUGUI[] text;
    // Start is called before the first frame update
    void Start()
    {
        text = GameObject.FindObjectsOfType<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

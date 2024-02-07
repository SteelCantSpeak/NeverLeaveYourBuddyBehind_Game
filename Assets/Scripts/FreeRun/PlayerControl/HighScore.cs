using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScore : MonoBehaviour
{
    int best;
    public TextMeshProUGUI topscore;
    public LineRender line;

    // Start is called before the first frame update
    void Start()
    {
        best = PlayerPrefs.GetInt("highScore", 0);
        topscore.text = "Best: " +best.ToString();
        line = FindObjectOfType<LineRender>();
    }

    // Update is called once per frame
    void Update()
    {
        if (line.playerscore > best)
        {
            best = line.playerscore;
            topscore.text = "Best: " + best.ToString();
        }
    }

    void OnDestroy()
    {
        PlayerPrefs.SetInt("highScore", best);
        PlayerPrefs.Save();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LineRender : MonoBehaviour
{
    public GameObject lineStart;
    public GameObject lineEnd;
    public LineRenderer LR;
    public Vector3 offset;
    [Space]
    public Material lineMaterial;
    public float currentDST;
    public float MaxDST;
    Color lerpedColor;
    [Space]
    public Image ropeLengthDisplay;
    public int playerscore;
    public int maxTimeAway = 10;
    public float timeRemaining;
    public TextMeshProUGUI scoreDisplay;
    [Space]
    public TextMeshProUGUI countDown;
    public int maxPlatforms;

    // Start is called before the first frame update
    void Start()
    {
        LR.SetPosition(0, lineStart.transform.position + offset);
        LR.SetPosition(1, lineEnd.transform.position + offset);
        playerscore = 0;
        GiveScore();
    }

    // Update is called once per frame
    private void Update()
    {
        if ((currentDST / MaxDST) > 0.9)
        {
            countDown.enabled = true;
            timeRemaining -= Time.deltaTime;
            countDown.text = Mathf.FloorToInt(timeRemaining).ToString();
        }
        else
        {
            countDown.enabled = false;
            timeRemaining = maxTimeAway;

        }
    }


    private void LateUpdate()
    {
        currentDST = Vector3.Distance(lineStart.transform.position, lineEnd.transform.position);
        lerpedColor = Color.Lerp(Color.green, Color.red, (currentDST / MaxDST));

        LR.SetPosition(0, lineStart.transform.position + offset);
        LR.SetPosition(1, lineEnd.transform.position + offset);
        lineMaterial.color = lerpedColor;

        ropeLengthDisplay.color = lerpedColor;
        ropeLengthDisplay.fillAmount = currentDST / MaxDST;
    }

    public void GiveScore()
    {
        scoreDisplay.text = "Score: " + playerscore.ToString();
    }

    void StartCount()
    {

        maxTimeAway -= Mathf.FloorToInt(Time.deltaTime);
        countDown.text = maxTimeAway.ToString();
    }
}

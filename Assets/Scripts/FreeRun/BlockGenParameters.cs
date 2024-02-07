using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGenParameters : MonoBehaviour
{
    public GameObject movingPlatform;
    public GameObject staticPlatform;
    LineRender line;

    public AnimationCurve movingBlockLikelihood;

    [HideInInspector]
    public  List<Color> Playercolours = new List<Color>();
    public float blockIntensity = 3f;
    public float inertSpeed = 2f;
    public int blockColour;

    // Start is called before the first frame update
    void Awake()
    {
        line = GetComponentInChildren<LineRender>();

        foreach (CharacterController player in GetComponent<SwapCharacter>().playerList)
        {
            Playercolours.Add(player.gameObject.GetComponent<ActiveColor>().primaryColour);
        }
    }

    public void NextStep(GameObject block)
    {
        float item = Random.Range(0f, 1f);
        float score = line.playerscore/100f;
        GameObject newBlock;

        if (item < (movingBlockLikelihood.Evaluate(Mathf.Clamp(score,0f,1f))))
        {
           newBlock = Instantiate(movingPlatform, block.transform.parent.position + new Vector3(Random.Range(-6.0f, 6.0f), -10f, 6 +score*10f), Quaternion.identity);
        }
        else
        {
            newBlock = Instantiate(staticPlatform, block.transform.parent.position + new Vector3(Random.Range(-6.0f, 6.0f), -10f, 6 + score * 10f), Quaternion.identity);
        }

        StartCoroutine(Incoming(newBlock));
    }

    IEnumerator Incoming(GameObject block)
    {

        while (block.transform.position.y < -0.1f)
        {
            float difference = -block.transform.position.y;

            block.transform.position += new Vector3(0f, 0.1f * difference, 0f);

            yield return new WaitForFixedUpdate();
        }

        yield return null;
    }

    //public void LateUpdate()
    //{

    //    ActiveColor[] activeColourArray = FindObjectsOfType<ActiveColor>();
    //    for (int i = 0; i < Playercolours.Count; i++)
    //    {
    //        if (Playercolours[i] != activeColourArray[i].primaryColour)
    //        {
    //            Playercolours[i] = activeColourArray[i].primaryColour;
    //        }
    //    }
    //}
}
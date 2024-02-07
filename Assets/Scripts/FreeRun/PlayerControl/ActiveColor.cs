using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveColor : MonoBehaviour
{
    public Color primaryColour;
    public HandleColour playerColour;

    private void Awake()
    {
        transform.Find("xbot/Beta_Surface").gameObject.GetComponent<SkinnedMeshRenderer>().material.color = primaryColour;
    }

    //public void Update()
    //{
    //    primaryColour = transform.Find("xbot/Beta_Surface").gameObject.GetComponent<SkinnedMeshRenderer>().material.color = playerColour.colour.Evaluate(playerColour.colourValue);
    //}
}

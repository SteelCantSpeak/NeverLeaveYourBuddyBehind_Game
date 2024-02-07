using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwapCharacter : MonoBehaviour
{
    public List<CharacterController> playerList;
    [HideInInspector]
    public GameObject currentPlayer;
    [HideInInspector]
    public int initialCharacter = 0;

    public Camera playerCamera;
    public Vector3 cameraPositionOffset;

    public Image colorDisplay;

    // Start is called before the first frame update
    void Awake()
    {
        foreach (CharacterController a in playerList)
        {

            foreach (CharacterController b in playerList)
            {

                Physics.IgnoreCollision(a, b);
            }
        }


        initialCharacter = 0;

        currentPlayer = playerList[initialCharacter].gameObject;

        for (int i = 0; i < playerList.Count; i++)
        {
            if (playerList[i] != currentPlayer)
            {
                playerList[i].GetComponent<FPS_Behaviour>().canMove = false;
                playerList[i].GetComponentInChildren<SkinnedMeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
            }
        }
        playerCamera.transform.rotation = currentPlayer.transform.localRotation;

        playerCamera.transform.position = currentPlayer.transform.position + cameraPositionOffset;

        playerCamera.transform.parent = currentPlayer.transform;
        currentPlayer.GetComponent<FPS_Behaviour>().canMove = true;

        foreach (SkinnedMeshRenderer skin in currentPlayer.GetComponent<FPS_Behaviour>().mesh)
        {
            skin.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
        }

        Color image = currentPlayer.transform.Find("xbot/Beta_Surface").gameObject.GetComponent<SkinnedMeshRenderer>().material.color;
        image.a = 1f;
        colorDisplay.color = image;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && currentPlayer.GetComponent<FPS_Behaviour>().canMove)
        {
            CharacterShift();
        }
        //internalColour.material.color = colours[initialCharacter].color;
    }

    void CharacterShift()
    {
        initialCharacter++;

        if (initialCharacter > (playerList.Count - 1))
        {
            initialCharacter = 0;
        }

        //Undo Scores On Old
        currentPlayer.GetComponent<FPS_Behaviour>().canMove = false;
        currentPlayer.GetComponentInChildren<SkinnedMeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;

        foreach (SkinnedMeshRenderer skin in currentPlayer.GetComponent<FPS_Behaviour>().mesh)
        {
            skin.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        }

        //set Scores for New
        currentPlayer = playerList[initialCharacter].gameObject;
        currentPlayer.GetComponent<FPS_Behaviour>().canMove = true;

        foreach (SkinnedMeshRenderer skin in currentPlayer.GetComponent<FPS_Behaviour>().mesh)
        {
            skin.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
        }

        playerCamera.transform.parent = currentPlayer.transform.GetChild(0);

        playerCamera.transform.rotation = currentPlayer.GetComponent<FPS_Behaviour>().characterView;
        playerCamera.transform.position = currentPlayer.transform.position + cameraPositionOffset;

        Color image = currentPlayer.transform.Find("xbot/Beta_Surface").gameObject.GetComponent<SkinnedMeshRenderer>().material.color;
        image.a = 1f;
        colorDisplay.color = image;
    }

    public void ColourUpdate()
    {
        Color image = currentPlayer.transform.Find("xbot/Beta_Surface").gameObject.GetComponent<SkinnedMeshRenderer>().material.color;
        image.a = 1f;
        colorDisplay.color = image;
    }
}

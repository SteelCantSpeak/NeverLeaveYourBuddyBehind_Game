using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPos : MonoBehaviour
{
    public Transform contactPlatform;
    public Collider controller;

    // Start is called before the first frame update

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        controller.transform.parent = hit.transform;
        contactPlatform = hit.transform;
    }

    private void OnCollisionExit(Collision collision)
    {
        
    }
}

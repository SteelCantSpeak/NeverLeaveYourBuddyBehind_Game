using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{

    FPS_Behaviour controller;
    Animator animator;
    float VelX = 0f;
    float VelZ = 0f ;
    float xMove = 0f;
    float zMove = 0f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponentInParent<FPS_Behaviour>();
    }

    private void Update()
    {
        VelX = Input.GetAxis("Horizontal");
        VelZ = Input.GetAxis("Vertical");
        bool Run = Input.GetKey("left shift");

        xMove = VelX * (Run ? 1 : 0.5f);
        zMove = VelZ * (Run ? 1 : 0.5f);
        if (controller.canMove)
        {
            animator.SetFloat("DirX", xMove);
            animator.SetFloat("DirZ", zMove);

            if (controller.isJumping)
            {
                animator.SetBool("Jump", true);
            }
            else
            {
                animator.SetBool("Jump", false);
            }

            animator.SetFloat("CamY", controller.rotationX);
        } else
        {
            animator.SetFloat("DirX", 0);
            animator.SetFloat("DirZ", 0);
            animator.SetBool("Jump", false);
        }
    }
}

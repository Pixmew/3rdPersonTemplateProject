using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField]
    private float speed = 0;
    [SerializeField]
    private Animator animator;
    Vector3 ang,angcam = Vector3.zero;
    public Transform cam;
    

    // Update is called once per frame
    void LateUpdate()
    {
        Move();
    }

    void Move()
    {
        if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0)
        {
            speed = 0;
            animator.SetBool("isIdle" , true);
            animator.SetBool("isRunning", false);
            animator.SetBool("isWalking", false);
        }
        else 
        {
            if (Input.GetAxis("Sprint") > 0)
            {
                speed = 5;
                animator.SetBool("isRunning" , true);
            }
            else
            {
                animator.SetBool("isRunning", false);
                animator.SetBool("isWalking", true);
                speed = 2;
            }
            
            Vector3 MoveDirectionNormal = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            transform.Translate(MoveDirectionNormal * speed * Time.deltaTime);
        }
        ang = new Vector3(0 , Input.GetAxis("Mouse X") , 0);
        angcam = new Vector3(-Input.GetAxis("Mouse Y")*0.5f , 0 , 0);
        transform.Rotate(ang);
        cam.transform.Rotate(angcam);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Controller : MonoBehaviour
{
    [SerializeField]
    private float speed = 0;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private CharacterController controller;
    Vector3 ang,angcam = Vector3.zero;
    public Transform cam;

    float smoothDampAngle;
    float SmoothTurnTime = 0.1f;

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }
    // Update is called once per frame
    void Update()
    {
        Move();
        Look();
    }


    void Move()
    {
        //if There is no Input 
        if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0)
        {
            //set speed to zero and play Idle animation
            speed = 0;
            animator.SetBool("isIdle" , true);
            //if Other animations is running stop it
            if (animator.GetBool("isRunning") == true || animator.GetBool("isWalking"))
            {
                animator.SetBool("isRunning", false);
                animator.SetBool("isWalking", false);
            } 
        }
        else //if there is some input
        {
            //if sprint/left-shift is pressed start Running
            if (Input.GetAxis("Sprint") > 0)
            {
                speed = 5;
                animator.SetBool("isRunning" , true);
            }
            else//if forward button is pressed start walking
            {
                speed = 2;
                animator.SetBool("isWalking", true);
                //if Running animations is running stop it
                if (animator.GetBool("isRunning") == true)
                {
                    animator.SetBool("isRunning", false);
                }  
            }
            //Moving Logic  
            Vector3 MoveDirectionNormal = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
            if (MoveDirectionNormal.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(MoveDirectionNormal.x , MoveDirectionNormal.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y , targetAngle , ref smoothDampAngle , SmoothTurnTime);
                transform.rotation = Quaternion.Euler(0f , smoothAngle , 0f);
                Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move(moveDirection * speed * Time.deltaTime);
            }  
        } 
    }
    void Look()
    {
        //Looking Logic
        ang = new Vector3(0, Input.GetAxis("Mouse X"), 0);
        angcam = new Vector3(-Input.GetAxis("Mouse Y") * 0.5f, 0, 0);
        transform.Rotate(ang);
        cam.transform.Rotate(angcam);
    }
}

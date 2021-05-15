using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;



public class Controller : MonoBehaviour
{
    [SerializeField]
    private float speed = 0;
    [SerializeField]
    public Animator animator;
    private CharacterController Character;
    [SerializeField]
    private Transform GroundChecker;
    public Transform cam;
    private GameObject Cameraa;
    public CinemachineFreeLook CinemaCamera;


    [Header("MovementSetting")]
    [SerializeField]
    private float SmoothTurnTime = 0.1f;
    public float idleSpeed = 0;
    public float WalkingSpeed = 2;
    public float RunningSpeed = 5;
    float smoothDampAngle;


    [Header("GravitySettings")]
    public bool ApplyGravity = true;
    [SerializeField]
    private float GravityIntensity = 5;
    [SerializeField]
    private float GroundedGravityIntensity = 1;


    [Header("ActionCheck")]
    private bool isGrounded = false;
    

    [SerializeField]
    private LayerMask GroundCheckTo;
    public float GravityVelocity = 0;


    [Header("MouseSetting")]
    public bool VisibleMouse = true;

    

    private void Start()
    {
        Cameraa = GameObject.Find("Camera");
        cam = Camera.main.transform;
        if (!VisibleMouse)
        {
            MouseSettingLock();
        }
        Character = gameObject.GetComponent<CharacterController>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        GroundCheck();
        Move(ApplyGravity);
    }


    void Move(bool HasGravity)
    {
        //if There is no Input 
        if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0)
        {
            if (speed >= 0)
            {
                speed -= 2f * Time.deltaTime;
                animator.SetFloat("Speed" , speed);
            }
        }
        else //if there is some input
        {
            
            if (speed < 2 && Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
            {
                speed += 2f * Time.deltaTime;
                animator.SetFloat("Speed", speed);
            }
            
            if (speed < 5 && Input.GetAxis("Sprint") > 0)
            {
                speed += 2f * Time.deltaTime;
                animator.SetFloat("Speed", speed);
            }
            else if (speed > 2)
            {
                speed -= 2.5f * Time.deltaTime;
                animator.SetFloat("Speed", speed);
            }
            

            //Moving Logic  
            Vector3 MoveDirectionNormal = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
            float targetAngle = Mathf.Atan2(MoveDirectionNormal.x , MoveDirectionNormal.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y , targetAngle , ref smoothDampAngle , SmoothTurnTime);
            transform.rotation = Quaternion.Euler(0f , smoothAngle , 0f);
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            Character.Move(moveDirection * speed * Time.deltaTime); 
        }
        if (HasGravity && isGrounded)
        {
            GravityVelocity = GroundedGravityIntensity;
        }
        else if (HasGravity && !isGrounded)
        {
            GravityVelocity += GravityIntensity * Time.deltaTime;
        }
        Character.Move(Vector3.down * GravityVelocity * Time.deltaTime);
    }

    void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(GroundChecker.position , 1f , GroundCheckTo);
    }

    void MouseSettingLock()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

   
}

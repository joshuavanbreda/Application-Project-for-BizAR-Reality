using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerController : MonoBehaviour
{

    public float speed = 1f;
    public float turnSpeed = 50.0f;
    public float horizontalInput;
    public float VerticalInput;

    public CinemachineVirtualCamera vCam1;

    public float sensitivity = 100f;
    private float xRotation = 0f;

    public Camera playerCam;
    public GameObject head;
    public Animator anim;

    private float maxLookHeigh = -1.5f;
    private float minLookHeight = 3.25f;

    public float verticalLookValue;

    public Transform followTransform;

    private void Start()
    {
        
    }

    void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.Equals(0, 0);
            Debug.Log("Application is focussed");
        }
        else
        {
            Debug.Log("Application lost focus");
        }
    }

    void Update()
    {
        //Mouse code
        #region Mouse Controls
        MouseLook();

        if (Input.GetMouseButtonDown(0))
        {
            anim.SetBool("isHitting", true);
            // start animation from the beginning
            anim.Play("Hit", -1, 0f);
        }
        if (Input.GetMouseButtonUp(0))
        {
            anim.SetBool("isHitting", false);
        }
        #endregion

        //Movement code
        #region Movement Controls
        if (Input.GetKey(KeyCode.W))
        {
            //move forward
            transform.Translate(Vector3.right * Time.deltaTime * speed);
            anim.SetBool("isWalking", true);
            anim.SetBool("isIdle", false);
        }
        if (Input.GetKeyUp(KeyCode.W))
            {
                //stop moving forward
                transform.Translate(Vector3.right * Time.deltaTime * 0);
                anim.SetBool("isWalking", false);
                anim.SetBool("isIdle", true);
            }

        if (Input.GetKey(KeyCode.S))
        {
            //move backward
            transform.Translate(Vector3.left * Time.deltaTime * speed);
            anim.SetBool("isWalking", true);
            anim.SetBool("isIdle", false);
        }

        if (Input.GetKeyUp(KeyCode.S))
            {
                //stop moving backward
                transform.Translate(Vector3.left * Time.deltaTime * 0);
                anim.SetBool("isWalking", false);
                anim.SetBool("isIdle", true);
            }

        if (Input.GetKey(KeyCode.A))
        {
            //move left
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            anim.SetBool("isWalking", true);
            anim.SetBool("isIdle", false);
        }

        if (Input.GetKeyUp(KeyCode.A))
            {
                //stop moving left
                transform.Translate(Vector3.forward * Time.deltaTime * 0);
                anim.SetBool("isWalking", false);
                anim.SetBool("isIdle", true);
            }

        if (Input.GetKey(KeyCode.D))
        {
            //move right
            transform.Translate(Vector3.back * Time.deltaTime * speed);
            anim.SetBool("isWalking", true);
            anim.SetBool("isIdle", false);
        }

        if (Input.GetKeyUp(KeyCode.D))
            {
                //stop moving right
                transform.Translate(Vector3.back * Time.deltaTime * 0);
                anim.SetBool("isWalking", false);
                anim.SetBool("isIdle", true);
            }
        #endregion
    }

    void MouseLook()
    {
        //write mouse look code here   

        horizontalInput = Input.GetAxis("Mouse X") * turnSpeed;
        VerticalInput = Mathf.Clamp((Input.GetAxis("Mouse Y") * turnSpeed), -45, 45);

        verticalLookValue = Mathf.Clamp(Input.GetAxis("Mouse Y") * 10, -1.5f, 3.5f);
        print(verticalLookValue);

        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        transform.Rotate(Vector3.up, horizontalInput * Time.deltaTime);
        //head.transform.rotation = Quaternion.Euler(head.transform.rotation.x, head.transform.rotation.y, head.transform.rotation.z + VerticalInput);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        head.transform.localRotation = Quaternion.Euler(0f, 0f, -xRotation);
        playerCam.transform.localRotation = Quaternion.Euler(0f, 0f, -xRotation);








    }
}

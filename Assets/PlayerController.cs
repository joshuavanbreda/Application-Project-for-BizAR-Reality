using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed = 1f;
    public float turnSpeed = 50.0f;
    public float horizontalInput;
    public float VerticalInput;

    public Camera playerCam;
    public GameObject head;
    public Animator anim;

    void Update()
    {
        MouseLook();

        if (Input.GetMouseButtonDown(0))
        {
            anim.SetBool("isHitting", true);
            anim.SetBool("isIdle", false);
            anim.SetBool("isWalking", false);
            // start animation from the beginning
            anim.Play("Hit", -1, 0f);
        }
        if (Input.GetMouseButtonUp(0))
        {
            anim.SetBool("isHitting", false);
            anim.SetBool("isIdle", true);
        }


        //Movement

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
    }

    void MouseLook()
    {
        //write mouse look code here   

        horizontalInput = Input.GetAxis("Mouse X") * turnSpeed;
        VerticalInput = Mathf.Clamp((Input.GetAxis("Mouse Y") * turnSpeed), -45, 45);

        //Mathf.Clamp(horizontalInput, -90f, 90f);

        transform.Rotate(Vector3.up, horizontalInput * Time.deltaTime);
        //head.transform.rotation = Quaternion.Euler(head.transform.rotation.x, head.transform.rotation.y, head.transform.rotation.z + VerticalInput);
    }
}

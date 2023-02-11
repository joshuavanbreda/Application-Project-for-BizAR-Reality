using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMovement : MonoBehaviour
{
    public float moveSpeed = 6f;
    public Animator animator;
    public Animation hit;
    public Transform playerCam;
    private string horizontalAxis = "Horizontal";
    private string verticalAxis = "Vertical";

    public float forceAmount = 10;
    public float hitDistance;

    public float sensitivity = 300f;
    private float xRotation = 0f;

    public GameObject head;

    void Start()
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

    void FixedUpdate()
    {
        Move();
    }

    private void Update()
    {
        #region Debug RaycastCheck
        Ray tempRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit tempHit;

        if (Physics.Raycast(tempRay, out tempHit, hitDistance, LayerMask.GetMask("Enemy")))
        {
            Debug.DrawLine(playerCam.transform.position, playerCam.transform.position + (playerCam.transform.forward * hitDistance), Color.red);
        }
        else
        {
            Debug.DrawLine(playerCam.transform.position, playerCam.transform.position + (playerCam.transform.forward * hitDistance), Color.green);
        }
        #endregion

        if (Input.GetMouseButtonDown(0))
        {
            animator.SetBool("isHitting", true);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, hitDistance, LayerMask.GetMask("Enemy")))
            {
                Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    Vector3 forceDirection = (hit.transform.position - transform.position).normalized;
                    rb.AddForce(forceDirection * forceAmount, ForceMode.Impulse);
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            animator.SetBool("isHitting", false);
        }
    }

    public void Move()
    {
        float horizontal = Input.GetAxis(horizontalAxis);
        float vertical = Input.GetAxis(verticalAxis);

        if (horizontal != 0 || vertical != 0)
        {
            Vector3 moveDirection = playerCam.forward * vertical + playerCam.right * horizontal;
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);

            Vector3 eulerAngles = targetRotation.eulerAngles;
            eulerAngles.x = 0;
            eulerAngles.z = 0;
            eulerAngles.y = eulerAngles.y - 90;
            targetRotation = Quaternion.Euler(eulerAngles);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10.0f);
            transform.position += moveDirection * moveSpeed * Time.deltaTime;

            animator.SetBool("isWalking", true);
            animator.SetBool("isIdle", false);
        }
        else
        {
            animator.SetBool("isIdle", true);
            animator.SetBool("isWalking", false);
        }
    }




    void Animate(float h, float v)
    {
        bool walking = h != 0f || v != 0f;
        bool idle = h == 0f && v == 0f;

        animator.SetBool("isWalking", walking);
        animator.SetBool("isIdle", idle);
    }
}
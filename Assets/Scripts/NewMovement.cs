using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMovement : MonoBehaviour
{
    public bool isInitialized = false;

    public float moveSpeed = 6f;
    private float tempMoveSpeed;
    public Animator animator;
    public Transform playerCam;
    private string horizontalAxis = "Horizontal";
    private string verticalAxis = "Vertical";

    public float forceAmount = 10;
    public float hitDistance;

    public float sensitivity = 300f;
    private float xRotation = 0f;

    private bool isGrounded = true;

    public TextureChanger textureChanger;

    public AudioController audioController;

    public PauseMenu pauseMenu;

    void Start()
    {
        pauseMenu = GameObject.Find("SCRIPTS").GetComponent<PauseMenu>();
        pauseMenu.pauseCanvas.gameObject.SetActive(false);
        transform.rotation = Quaternion.Euler(new Vector3(0, -90f, 0));

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.Equals(0, 0);

        isInitialized = true;
    }

    //void OnApplicationFocus(bool hasFocus)
    //{
    //    if (hasFocus)
    //    {
    //        Cursor.lockState = CursorLockMode.Locked;
    //        Cursor.Equals(0, 0);
    //        Debug.Log("Application is focussed");
    //    }
    //    else
    //    {
    //        Debug.Log("Application lost focus");
    //    }
    //}

    void FixedUpdate()
    {
        if (isInitialized)
        {
            Move();
        }
    }

    private void Update()
    {
        if (isInitialized)
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
                Debug.DrawLine(playerCam.transform.position, playerCam.transform.position + (playerCam.transform.forward * hitDistance), Color.green);                              //This section just casts a line that turns from green to red when you are able to
            }                                                                                                                                                                       //punch an enemy (line show in editor Scene View)
            #endregion

            #region Punch Controls
            if (Input.GetMouseButtonDown(0))
            {
                animator.SetBool("isHitting", true);                                                                    //Play punch animation
                                                                                                                        //ResetRotation();
                StartCoroutine(waitSound());
                
            }
            if (Input.GetMouseButtonUp(0))
            {
                animator.SetBool("isHitting", false);                                                                   //Stop punch animation
            }

            #endregion

            //Sprint
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                tempMoveSpeed = moveSpeed;
                moveSpeed = 10f;
                animator.speed = 1.5f;
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                moveSpeed = tempMoveSpeed;
                animator.speed = 1f;
            }

            // Jump
            if (Input.GetKey(KeyCode.Space))
            {
                if (isGrounded)
                {
                    transform.GetComponent<Rigidbody>().AddForce(Vector3.up * 50f, ForceMode.Impulse);
                    //StartCoroutine(JumpWaitTimer());
                    isGrounded = false;
                }
            }

            // Pause
            if (Input.GetKey(KeyCode.Escape))
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                pauseMenu.pauseCanvas.gameObject.SetActive(true);
                isInitialized = false;
            }
        }
    }

    public IEnumerator waitSound()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);                                            //Create new Ray from Main Camera to the mouse pos (centre screen basically).
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, hitDistance, LayerMask.GetMask("Enemy")))                             //If we hit an enemy that is close enough
        {
            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();                                              // get enenmy collider
            if (rb != null)
            {
                audioController.ChangeSound();
                yield return new WaitForSeconds(.1f);
                Vector3 forceDirection = (hit.transform.position - transform.position).normalized;              //set the direction the force will go towards
                rb.AddForce(forceDirection * forceAmount, ForceMode.Impulse);                                   //Apply force to the enemy

                textureChanger.ChangeTexture(rb);                                                               //Change Texture of enemy material to a random texture from our texture list
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Floor"))
        {
            isGrounded = true;
        }
    }

    // Movement Code
    public void Move()
    {
        float horizontal = Input.GetAxis(horizontalAxis);
        float vertical = Input.GetAxis(verticalAxis);

        if (horizontal != 0 || vertical != 0)       //if not moving
        {
            Vector3 moveDirection = playerCam.forward * vertical + playerCam.right * horizontal;                    //get direction we want to move in
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);                         // get rotation we want to look at

            Vector3 eulerAngles = targetRotation.eulerAngles;
            eulerAngles.x = 0;
            eulerAngles.z = 0;
            eulerAngles.y = eulerAngles.y - 90;
            targetRotation = Quaternion.Euler(eulerAngles);                                                         //fixing weird transform bug i have with my model (rmodel was rotated incorrectly by 90 degrees on y)

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10.0f);      //apply desired rotation to player
            transform.position += moveDirection * moveSpeed * Time.deltaTime;                                       //apply desired direction to move player to new position

            animator.SetBool("isWalking", true);                                                                    //Set animation states
            animator.SetBool("isIdle", false);
        }
        else
        {
            animator.SetBool("isIdle", true);
            animator.SetBool("isWalking", false);
        }
    }

    public void ResetRotation()                                                                             //Did not end up using it because i was having a visual hitching but left it in to show you what i was trying to do
    {                                                                                                       //Basically setting the rotation of the playerto the direction that the camera is looking towards. it works, just hitchy.
        float horizontal = Input.GetAxis(horizontalAxis);
        float vertical = Input.GetAxis(verticalAxis);

        //Vector3 moveDirection = playerCam.forward * vertical + playerCam.right * horizontal;
        Quaternion targetRotation = Quaternion.LookRotation(playerCam.forward, Vector3.up);

        Vector3 eulerAngles = targetRotation.eulerAngles;
        eulerAngles.x = 0;
        eulerAngles.z = 0;
        eulerAngles.y = eulerAngles.y - 90;
        targetRotation = Quaternion.Euler(eulerAngles);

        transform.rotation = targetRotation;
    }

    
}
using System.Collections;
using UnityEngine;
public class PlayerHead : MonoBehaviour
{

    //public float sensitivity = 800.0f;
    //public float minimumY = -30.0f;
    //public float maximumY = 60.0f;
    //public float minimumY = -30.0f;
    //public float maximumY = 60.0f;
    //private float _rotX = 0.0f;
    //private float _rotY = 0.0f;

    //public Transform headPos;

    //void Start()
    //{
    //    Cursor.lockState = CursorLockMode.Locked;
    //    Cursor.visible = false;
    //    _rotX = transform.localEulerAngles.y;
    //    _rotY = transform.localEulerAngles.x;

    //    Vector2 center = new Vector2(Screen.width / 2, Screen.height / 2);
    //}

    //void Update()
    //{
    //    _rotX += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
    //    _rotY -= Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
    //    _rotY = Mathf.Clamp(_rotY, minimumY, maximumY);
    //    _rotX = Mathf.Clamp(_rotX, minimumY, maximumY);
    //    transform.localEulerAngles = new Vector3(0, _rotX, -_rotY);

    //    transform.position = headPos.position;
    //}


    private float maxDistance = 10000.0f;
    public LayerMask layerMask;

    private Transform headTransform;

    void Start()
    {
        headTransform = transform;
    }

    void LateUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, maxDistance, layerMask))
        {
            Vector3 targetDirection = hit.point - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 20);
        }
        
        
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Transform cam;
    public Transform followTarget;

    void LateUpdate()
    {
        transform.position = followTarget.position + new Vector3(0, 2.2289f, 0);
        transform.LookAt(transform.position + cam.forward);
    }
}

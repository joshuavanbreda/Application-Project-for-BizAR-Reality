using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard1 : MonoBehaviour
{
    public Transform playerTransform;

    void LateUpdate()
    {
        Vector3 targetDirection = playerTransform.position - transform.position;
        float yRotation = Mathf.Atan2(targetDirection.x, targetDirection.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}

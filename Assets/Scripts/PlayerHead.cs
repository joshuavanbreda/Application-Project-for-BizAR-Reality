using System.Collections;
using UnityEngine;
public class PlayerHead : MonoBehaviour
{
    private float maxDistance = 10000.0f;
    public LayerMask layerMask;

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

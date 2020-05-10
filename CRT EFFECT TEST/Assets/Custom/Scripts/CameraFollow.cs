using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float heightCap;
    public float smoothSpeed = 0.125f;
    public PhysicMaterial pm;

    void FixedUpdate()
    {
        if(target.position.y < heightCap)
        {
            Vector3 desiredPosition = new Vector3(target.position.x, 7, target.position.z) + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
            target.GetComponent<Collider>().material = pm;
            target.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            target.GetComponent<PlayerMovement>().enabled = false;
            target.GetComponentInChildren<Holder>().enabled = false;
            target.GetComponentInChildren<ItemController>().enabled = false;
        } else
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
       

        transform.LookAt(target);
    }
}

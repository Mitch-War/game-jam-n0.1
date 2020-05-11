using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float heightCap;
    public float smoothSpeed = 0.125f;
    public PhysicMaterial pm;
    public Transform deathCamera;
    private bool isSpectating;
    private float timeDead = 3;
    

    void FixedUpdate()
    {
        if(isSpectating == false)
        {
            if (target.position.y < heightCap && target != null)
            {
                Vector3 desiredPosition = new Vector3(target.position.x, 7, target.position.z) + offset;
                Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
                transform.position = smoothedPosition;
                target.GetComponent<Collider>().material = pm;
                target.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                target.GetComponentInChildren<Holder>().enabled = false;
                target.GetComponentInChildren<ItemController>().enabled = false;
                target.GetComponent<PlayerMovement>().isDead = true;
                timeDead -= Time.deltaTime;

            }
            else if (target != null)
            {
                Vector3 desiredPosition = target.position + offset;
                Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
                transform.position = smoothedPosition;
            }

            
        }
        if (timeDead <= 0)
        {
            if(target != null)
                target.GetComponent<PlayerMovement>().OnDied();
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, deathCamera.position, -timeDead);
            transform.position = smoothedPosition;
            print(smoothedPosition);
            transform.LookAt(deathCamera.GetComponentsInChildren<Transform>()[1]);
            isSpectating = true;
        }
        else if (target != null)
        {
            transform.LookAt(target);
        }


    }
}

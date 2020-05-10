using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floating : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < 3.5)
        {
            GetComponent<Rigidbody>().AddForce(-transform.forward * Vector3.Distance(transform.position, new Vector3(transform.position.x, 3, transform.position.z)) * 25);
        }
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Player")
        {
            print("Hello");
            collision.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(collision.gameObject.GetComponent<Rigidbody>().velocity.x, transform.GetComponent<Rigidbody>().velocity.y, collision.gameObject.GetComponent<Rigidbody>().velocity.z);
        }
    }
}

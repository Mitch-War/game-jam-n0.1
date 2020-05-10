using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    public float timeAlive;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeAlive -= Time.deltaTime;
        if(timeAlive <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        Instantiate(Resources.Load<Transform>("BulletEffect"), transform.position, Quaternion.Euler(other.gameObject.transform.forward));
        Destroy(gameObject);
    }
}

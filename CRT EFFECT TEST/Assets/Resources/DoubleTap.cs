using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleTap : MonoBehaviour
{
    private float timebetween;
    public bool tapped;
    private KeyCode key;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKey(KeyCode.D) && timebetween <= -.25f || Input.GetKey(KeyCode.D) && key != KeyCode.D)
        {
            key = KeyCode.D;
            tapped = true;
            timebetween = .25f;
        }
        
        if (Input.GetKey(KeyCode.A) && timebetween <= -.25f || Input.GetKey(KeyCode.A) && key != KeyCode.A)
        {
            key = KeyCode.A;
            tapped = true;
            timebetween = .25f;
        }
        if (tapped)
        {
            timebetween -= Time.deltaTime;
            if(timebetween <.23f)
            {
                foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
                {
                    if (vKey == key && Input.GetKeyDown(key))
                    {

                        print("Tapped");
                        GetComponent<PlayerMovement>().Dash(key);
                        tapped = false;

                    }
                }
            }
            
        }
        if (timebetween <= 0)
        {
            tapped = false;
        }
    }
}

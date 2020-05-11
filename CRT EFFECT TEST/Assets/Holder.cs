using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holder : MonoBehaviour
{
    public Transform[] items;
    private int i;
    public Transform location;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        items[1].position = location.position;
        items[1].rotation = location.rotation;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            items[0].gameObject.SetActive(false);
            items[1].gameObject.SetActive(true);
        }
        else
        {
            items[1].gameObject.SetActive(false);
            items[0].gameObject.SetActive(true);
        }
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100))
        {
            transform.LookAt(new Vector3 (hit.point.x, hit.point.y, transform.position.z));
        }
    }
}

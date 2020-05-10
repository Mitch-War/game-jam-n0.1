using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holder : MonoBehaviour
{
    private Transform[] items;
    private int i;
    // Start is called before the first frame update
    void Start()
    {
        items = GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            items[1].gameObject.SetActive(false);
            items[2].gameObject.SetActive(true);
        }
        else
        {
            items[2].gameObject.SetActive(false);
            items[1].gameObject.SetActive(true);
        }
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100))
        {
            transform.LookAt(new Vector3 (hit.point.x, hit.point.y, transform.position.z));
        }
    }
}

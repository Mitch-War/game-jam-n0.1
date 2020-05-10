using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Rendering;

public class ItemController : MonoBehaviour
{
    public string itemName;
    private int ammoCount;
    private Transform bullet;
    public Transform eject;
    private float firerate;
    private Renderer mr;
    public Material shotmat;
    public Material normalmat;
    // Start is called before the first frame update
    void Start()
    {
        mr = GetComponent<Renderer>();
        bullet = Resources.Load<Transform>("Bullet");
        if (itemName == "Assault")
        {
            ammoCount = 10;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        firerate -= Time.deltaTime;
        if(itemName == "Shotgun")
        {
            if (Input.GetButtonDown("Fire1"))
            {

            }
        }
        if(itemName == "Assault")
        {
            if(ammoCount == 0)
            {
                Material[] mats = mr.materials;
                mats[0] = shotmat;
                mr.materials = mats;
            }
            else if(ammoCount != 10)
            {
                Material[] mats = mr.materials;
                mats[10 - ammoCount] = shotmat;
                mr.materials = mats;
            }
            if (Input.GetButton("Fire1") && firerate <= 0 && ammoCount > 0 && eject.gameObject.activeInHierarchy)
            {
                Transform shot = Instantiate(bullet, eject.position, eject.rotation);
                shot.GetComponent<Rigidbody>().AddForce(eject.up * 850);
                shot.GetComponent<Bullet>().timeAlive = 10f;
                firerate = .5f;
                ammoCount--;
            }

            if(firerate < -1.5 && ammoCount < 10)
            {
                if(ammoCount == 0)
                {
                    Material[] mats = mr.materials;
                    mats[0] = normalmat;
                    mr.materials = mats;
                }
                else
                {
                    Material[] mats = mr.materials;
                    mats[10 - ammoCount] = normalmat;
                    mr.materials = mats;
                }
                
                ammoCount++;
                
                firerate = 0;
            }
        }
    }
}

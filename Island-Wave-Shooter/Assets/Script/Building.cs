using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Building : MonoBehaviour
{
    [SerializeField] private LayerMask OpenSpace;
    public bool platform;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Camera.main == null) return;

        RaycastHit hit;

        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * 1000);
        if (Physics.SphereCast(ray, 0.5f, out hit, 10, OpenSpace))
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                platform = true;
            }

            if (platform == true) 
            {
                if (OpenSpace.value == (1 << hit.collider.gameObject.layer))
                {
                    MainBaseBuilnd Ghost = hit.collider.GetComponent<MainBaseBuilnd>();
                    if (Ghost != null)
                    {
                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            if (Vector3.Distance(transform.position, Ghost.transform.position) < Ghost.BuildRange)
                            {
                                Debug.Log("Opened");
                               // plat.build();
                            }
                        }
                    }
                }

            }

        }
    }
}

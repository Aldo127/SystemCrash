using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFX : MonoBehaviour
{
    [SerializeField] GameObject fireVFX;

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Destroy(gameObject);
            GameObject explosion = Instantiate(fireVFX, transform.position, transform.rotation);
        }     
    }
}

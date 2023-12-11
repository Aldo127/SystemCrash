using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOWBOWBOW : MonoBehaviour
{
    public Transform shootingPoint;
    public GameObject fireBall;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Instantiate(fireBall, shootingPoint.transform.position, Quaternion.identity);
        }
    }
}

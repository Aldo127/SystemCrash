using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMeteor : MonoBehaviour
{
    public GameObject vfx;
    public Transform startPoint;
    public Transform endPoint;

     public void MeteorTest()
    {
        var startPos = startPoint.position;
        GameObject objVFX = Instantiate(vfx, startPos, Quaternion.identity) as GameObject;

        var endPos = endPoint.position;

        RotateTo(objVFX, endPos);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            MeteorTest();
        }
    }

    public void RotateTo(GameObject obj, Vector3 destination)
    {
        var direction = destination - obj.transform.position;
        var rotation = Quaternion.LookRotation(direction);
        obj.transform.localRotation = Quaternion.Lerp  (obj.transform.rotation, rotation, 1);
    }
}
  
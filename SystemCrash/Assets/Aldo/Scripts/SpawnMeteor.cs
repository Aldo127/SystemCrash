using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMeteor : MonoBehaviour
{
    public GameObject vfx;
    public Transform startPoint;
    public Transform endPoint;
    public GameObject Image;

    [SerializeField] private Cooldown cooldown;

     public void MeteorTest()
    {
        if (cooldown.IsCoolingDown) return;

        var startPos = startPoint.position;
        GameObject objVFX = Instantiate(vfx, startPos, Quaternion.identity) as GameObject;

        var endPos = endPoint.position;

        RotateTo(objVFX, endPos);

        cooldown.StartCooldown();
    }

    private void Update()
    {

        if (cooldown.IsCoolingDown )
        {
            Image.SetActive(false);
        }
        else
        {
            Image.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            MeteorTest();

            cooldown.StartCooldown();
        }
    }

    public void RotateTo(GameObject obj, Vector3 destination)
    {
        var direction = destination - obj.transform.position;
        var rotation = Quaternion.LookRotation(direction);
        obj.transform.localRotation = Quaternion.Lerp  (obj.transform.rotation, rotation, 1);
    }
}
  
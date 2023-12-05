using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTester : MonoBehaviour
{
    public HealthManager playeratm;
    public HealthManager enemyAtm;
    public bool enemyIsClose;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && enemyIsClose)
        {
            playeratm.DealDamage(enemyAtm.gameObject);
        }

        if (Input.GetKeyDown(KeyCode.F12))
        {
            enemyAtm.DealDamage(playeratm.gameObject);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemyIsClose = true;
            Debug.Log("player close");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemyIsClose = false;

        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int health;
    public int attack;
    public GameObject character;

    public void TakeDamage(int amount)
    {
        health -= amount;
        GetComponent<MeshRenderer>().material.color = Color.red;
       // WaitForSeconds(5)
        GetComponent<MeshRenderer>().material.color = Color.white;
        
    }

    public void DealDamage(GameObject target)
    {
        var atm = target.GetComponent<HealthManager>();
        if(atm != null)
        {
            atm.TakeDamage(attack);
        }
    }

    public void Update()
    {
        if (health < 0)
        {
            StartCoroutine(Death());
        }
    }

    IEnumerator Death()
    {
        character.GetComponent<Animator>().Play("Death");
        yield return null;
    }
}

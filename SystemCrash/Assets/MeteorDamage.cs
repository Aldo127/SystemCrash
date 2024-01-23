using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorDamage : MonoBehaviour
{
    [SerializeField] private GameSettings gameSettings;

    void OnTriggerEnter(Collider other)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (other.gameObject.CompareTag("Enemy"))
        {
            // Destroy(other.gameObject);
            foreach (GameObject enemy in enemies)
            {
                GameObject.Destroy(enemy);
                GameObject newBit = Instantiate(gameSettings.bitPrefab, transform.position, Random.rotation);
                newBit.SetActive(true);
                newBit.GetComponent<Rigidbody>().AddForce(newBit.transform.forward * 280f, ForceMode.Force);
            }
            Debug.Log("Damage");
        }
    }
}

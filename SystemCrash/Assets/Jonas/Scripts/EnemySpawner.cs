using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> enemies;
    private int cooldown;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        cooldown += 1;
        if (cooldown >= 60)
        {
            transform.position = new Vector3(Random.Range(-39, 39), 1.5f, Random.Range(-39, 39));
            Vector3 distanceVector = gameObject.transform.position - player.transform.position;
            float distanceFromPoint = distanceVector.sqrMagnitude;
            if (distanceFromPoint > 70)
            {
                GameObject newEnemy = Instantiate(enemies[Random.Range(0, enemies.Count)], transform.position, transform.rotation);
                newEnemy.SetActive(true);
                cooldown = 0;
            }
        }
    }
}

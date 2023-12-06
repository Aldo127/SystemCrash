using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> enemies;
    private int cooldown;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        cooldown += 1;
        if (cooldown >= 60)
        {
            GameObject newEnemy = Instantiate(enemies[Random.Range(0, enemies.Count)], transform);
            newEnemy.SetActive(true);
            cooldown = 0;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> enemies;
    private int cooldown = -120;
    private GameObject player;
    public GameSettings gameSettings;
    public GameObject reminder;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        reminder.SetActive(true);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        cooldown += 1;
        if (cooldown >= 60 && gameSettings.playerAlive == true)
        {
            reminder.SetActive(false);
            transform.position = new Vector3(Random.Range(-39, 39), 1.5f, Random.Range(-39, 39));
            Vector3 distanceVector = gameObject.transform.position - player.transform.position;
            float distanceFromPoint = distanceVector.sqrMagnitude;
            if (distanceFromPoint > gameSettings.enemySpawnDistance)
            {
                GameObject newEnemy = Instantiate(enemies[Random.Range(0, enemies.Count)], transform.position, transform.rotation);
                newEnemy.SetActive(true);
                cooldown = 0;
            }
        }
    }
}

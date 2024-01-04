using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> enemies;
    private int cooldown = -100;
    private GameObject player;
    public GameSettings gameSettings;
    public GameObject reminder;
    private int time;
    public GameObject timeCounter;
    private bool startTimer = false;
    public int spawnRange;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        reminder.SetActive(true);
        gameSettings.gameIsActive = true;
        time = gameSettings.timeLimit * 50;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) startTimer = true;
    }
    void FixedUpdate()
    {
        if (startTimer && gameSettings.playerAlive && gameSettings.gameIsActive)
        {
            cooldown += 1;
            time -= 1;
            reminder.SetActive(false);
        }
        if (time % 50 == 0) timeCounter.GetComponent<Text>().text = "Time: " + time / 50;
        if (time <= 0) gameSettings.gameIsActive = false;
        if (cooldown >= 60 && gameSettings.playerAlive && gameSettings.gameIsActive)
        {
            transform.position = new Vector3(Random.Range(-spawnRange, spawnRange), 1.5f, Random.Range(-spawnRange, spawnRange));
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

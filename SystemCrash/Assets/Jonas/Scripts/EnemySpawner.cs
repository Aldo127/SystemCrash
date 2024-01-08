using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> enemies;
    private int cooldown;
    private GameObject player;
    public GameSettings gameSettings;
    public GameObject reminder;
    public GameObject timeOver;
    private int time;
    public GameObject timeCounter;
    private bool startTimer = false;
    public int spawnRange;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        reminder.SetActive(true);
        Reset();
    }

    public void Reset()
    {
        timeOver.SetActive(false);
        time = gameSettings.timeLimit * 50;
        cooldown = -100;
        gameSettings.gameIsActive = true;
        startTimer = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) startTimer = true;
        if (!gameSettings.playerAlive && Input.GetKeyDown(KeyCode.Mouse0)) SceneManager.LoadScene("SceneyScene");
        if (time <= 0 && Input.GetKeyDown(KeyCode.Mouse0)) SceneManager.LoadScene("SceneyScene");
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
        if (time <= 0)
        { 
            gameSettings.gameIsActive = false;
            timeOver.SetActive(true);
        }
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

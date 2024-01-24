using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public int maxHealth = 50;
    public int points = 0;
    public int health;
    public GameObject gameOverScreen;
    public GameObject pointCounter;
    public GameObject healthCounter;
    public GameObject pointGoal;
    public GameObject gameOverMessage;
    public GameSettings gameSettings;
    
    // Start is called before the first frame update
    void Start()
    {
        gameSettings.playerAlive = true;
        health = maxHealth;
        gameOverMessage.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        healthCounter.GetComponent<Text>().text = "Health: " + health;
        pointCounter.GetComponent<Text>().text = "Score: " + points;
        if (gameSettings.endless) pointGoal.GetComponent<Text>().text = "Goal: " + gameSettings.pointBest + " without crashing!";
        else pointGoal.GetComponent<Text>().text = "Goal: " + gameSettings.pointGoal + " before time over!";
        if (health > maxHealth) health -= 1;
        if (gameSettings.playerAlive == true)
        {
            gameOverScreen.SetActive(false);
            if (gameSettings.gameIsActive && !gameSettings.endless && gameSettings.pointGoal > 0 && points >= gameSettings.pointGoal)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                SceneManager.LoadScene("WinScreen");
                gameSettings.gameIsActive = false;
            }
        }
    }
    public void Damage(int amount)
    {
        if (gameSettings.playerAlive == true && gameSettings.godMode == false)
        {
            health -= amount;
            if (amount > 0 && health <= 0) GameOver();
            else if (amount > 0) 
            {
                gameOverScreen.SetActive(true);
                //Debug.Log("Hit!");
                if (gameObject.GetComponent<ParticleSystem>()) gameObject.GetComponent<ParticleSystem>().Play();
            }
        }
    }
    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        gameOverMessage.SetActive(true);
        gameSettings.playerAlive = false;
        if (gameSettings.pointBest < points) gameSettings.pointBest = points;
        Debug.Log("Game Over!");
        //Debug.Log("Final Score: " + points);
    }
}

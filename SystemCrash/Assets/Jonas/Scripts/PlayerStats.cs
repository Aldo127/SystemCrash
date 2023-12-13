using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public int maxHealth = 50;
    public int points = 0;
    public int health;
    public GameObject gameOverScreen;
    public GameObject pointCounter;
    public GameObject healthCounter;
    public GameSettings gameSettings;
    
    // Start is called before the first frame update
    void Start()
    {
        gameSettings.playerAlive = true;
        health = maxHealth;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        healthCounter.GetComponent<Text>().text = "Health: " + health;
        pointCounter.GetComponent<Text>().text = "Score: " + points;
        if (health > maxHealth) health -= 1;
        if (gameSettings.playerAlive == true)
        {
            gameOverScreen.SetActive(false);
            if (gameSettings.gameIsActive && gameSettings.pointGoal > 0 && points >= gameSettings.pointGoal)
            {
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
        gameSettings.playerAlive = false;
        Debug.Log("Game Over!");
        //Debug.Log("Final Score: " + points);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int maxHealth = 50;
    public int points = 0;
    public int health;
    public GameObject gameOverScreen;
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
        if (health > maxHealth) health -= 1;
        if (gameSettings.playerAlive == true)
        {
            gameOverScreen.SetActive(false);
        }
    }
    public void Damage(int amount)
    {
        if (gameSettings.playerAlive == true)
        {
            health -= amount;
            if (amount > 0 && health <= 0) GameOver();
            else if (amount > 0) {
                gameOverScreen.SetActive(true);
                Debug.Log("Hit!"); }
        }
    }
    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        gameSettings.playerAlive = false;
        Debug.Log("Game Over!");
        Debug.Log("Final Score: " + points);
    }
}

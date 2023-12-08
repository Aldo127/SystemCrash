using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new enemy.asset", menuName = "enemy")]
public class EnemyType : ScriptableObject
{
    public string movementType = "forward";
    public int maxHealth = 5;
    public int bitValue = 10;
    public int damage = 10;
    public float speed = 5;

    public string attackType = "melee";
    public GameObject projectilePrefab;
    public int attackCooldown = 10;
    public int lifetime = -1;
}

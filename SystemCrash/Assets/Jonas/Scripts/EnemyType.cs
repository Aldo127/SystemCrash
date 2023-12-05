using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new enemy.asset", menuName = "enemy")]
public class EnemyType : ScriptableObject
{
    public string movementType = "forward";
    public int maxHealth = 5;
}

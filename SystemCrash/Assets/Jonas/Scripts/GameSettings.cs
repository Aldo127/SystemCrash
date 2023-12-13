using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new game settings.asset", menuName = "gameSettings")]
public class GameSettings : ScriptableObject
{
    public float cameraDistanceFromPlayer = 10;

    public int pointGoal = 300;
    public int timeLimit = -1;

    public float enemySpawnDistance = 70;

    public float enemySpeedMultiplier = 1f;

    public bool godMode = false;

    [Header("Do Not Touch")]
    public GameObject bitPrefab;

    public bool playerAlive = true;

    public bool gameIsActive = false;


}

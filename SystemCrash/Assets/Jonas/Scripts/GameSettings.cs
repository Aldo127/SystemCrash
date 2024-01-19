using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new game settings.asset", menuName = "gameSettings")]
public class GameSettings : ScriptableObject
{
    public float cameraDistanceFromPlayer = 10;

    public int pointGoal = 300;
    public int timeLimit = -1;
    public float collectRange = 25f;

    public float enemySpawnDistance = 70;

    public float enemySpeedMultiplier = 1f;

    public bool godMode = false;

    public float mSensitivityX;
    public float mSensitivityY;
    public bool endless = false;
    public int pointBest = 0;

    [Header("Do Not Touch")]
    public GameObject bitPrefab;

    public bool playerAlive = true;

    public bool gameIsActive = false;

    public bool gamePaused = false;

    //public bool enemiesActive = false;


}

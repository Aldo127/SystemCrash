using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new game settings.asset", menuName = "gameSettings")]
public class GameSettings : ScriptableObject
{
    public GameObject bitPrefab;

    public bool playerAlive = true;
}

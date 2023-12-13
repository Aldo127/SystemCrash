using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugHit : MonoBehaviour
{
    [SerializeField] private float objectDistance;
    [SerializeField] private Collider objectCollider;
    public GameSettings gameSettings;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && gameSettings.playerAlive && gameSettings.gameIsActive)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit))
            {
                objectDistance = hit.distance;
                objectCollider = hit.collider;
                EnemyAI isEnemy = hit.transform.GetComponentInParent<EnemyAI>();
                if (isEnemy) isEnemy.Damage(5);
            }
        }
    }
}

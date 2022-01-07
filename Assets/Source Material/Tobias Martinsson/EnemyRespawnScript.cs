using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main Author: Tobias Martinsson

public class EnemyRespawnScript : MonoBehaviour
{
    [SerializeField]
    private GameObject enemy;
    [SerializeField]
    private Transform spawnPosition;
    [SerializeField]
    public List<Transform> PatrolPoints;


    //Re-instantiates an enemy when called at their original spawn position. 
    public void RespawnEnemy()
    {
        
        Instantiate(enemy, spawnPosition.position, Quaternion.Euler(0,0,0), gameObject.transform);
    }
}

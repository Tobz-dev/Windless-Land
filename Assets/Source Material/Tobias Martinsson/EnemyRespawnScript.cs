using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawnScript : MonoBehaviour
{
   public GameObject enemy;
   public Transform spawnPosition;
    public List<Transform> PatrolPoints;

    private void Start()
    {

    }
    public void RespawnEnemy()
    {
        
        Instantiate(enemy, spawnPosition.position, Quaternion.Euler(0,0,0), gameObject.transform);
    }
}

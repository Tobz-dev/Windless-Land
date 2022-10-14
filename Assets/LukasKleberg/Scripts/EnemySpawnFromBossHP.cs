using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnFromBossHP : MonoBehaviour
{
    //I can't get the enemies in the boss fight to land on the mesh.
    //So here's a cheat solution that just sets some enemies active.

    [SerializeField]
    private GameObject bossEnemy1, bossEnemy2, bossEnemy3, bossEnemy4, bossEnemy5, bossEnemy6;

    [SerializeField]
    private EnemyHealthScript enemyHealthScript;

    private bool firstGroupSpawned, secondGroupSpawned, thirdGroupSpawned;



    // Update is called once per frame
    void Update()
    {
        float bossHealthPercent = enemyHealthScript.GetHealthPercentage();

        if (bossHealthPercent < 0.75 && !firstGroupSpawned) 
        {
            bossEnemy1.SetActive(true);
            firstGroupSpawned = true;
        }

        if (bossHealthPercent < 0.50 && !secondGroupSpawned)
        {
            bossEnemy2.SetActive(true);
            bossEnemy3.SetActive(true);

            secondGroupSpawned = true;
        }

        if (bossHealthPercent < 0.25 && !thirdGroupSpawned)
        {
            bossEnemy4.SetActive(true);
            bossEnemy5.SetActive(true);
            bossEnemy6.SetActive(true);

            thirdGroupSpawned = true;
        }
    }
}

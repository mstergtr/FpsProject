using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SteamK12.FpsProject
{
    public class Spawner : MonoBehaviour
    {
        public GameObject enemy;
        public Transform[] spawnPoints;
        public float timeBetweenSpawns = 2.0f;
        public float spawnTime = 1.0f;
        public bool isSpawning = false;
        public bool randomizeSpawns = false;
        public int maxSpawns = 4;
        private int spawns;
        private void Start()
        {
            InvokeRepeating(nameof(SpawnEnemy), spawnTime, timeBetweenSpawns);
        }

        public void Spawning(bool spawning)
        {
            isSpawning = spawning;
        }

        private void SpawnEnemy()
        {
            if (!isSpawning || spawns >= maxSpawns) return;

            if (randomizeSpawns)
            {
                Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
                spawns++;
            }
            else
            {
                for (int i = 0; i < spawnPoints.Length; i++)
                {
                    Instantiate(enemy, spawnPoints[i].position, spawnPoints[i].rotation);
                    spawns++;
                }
            }          
        }
    }
}

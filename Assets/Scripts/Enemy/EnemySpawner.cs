using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Components")]
    private GameObject player;

    [Header("Enemy Prefab List")]
    public List<GameObject> enemyPrefabList;

    [Header("Spawn Points")]
    public List<Transform> spawnPoints;

    [Header("Spawn Settings")]
    public int playerDetectionRange;
    public int enemyCount;
    public float spawnInterval;
    public float spawntimer;

    private float coundtdown;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        coundtdown = spawntimer;
    }

    private void Update()
    {
        if (coundtdown > -1.0f)
        {
            coundtdown -= Time.deltaTime;
        }
        if (coundtdown <= 0 && Vector2.Distance(transform.position, player.transform.position) <= playerDetectionRange)
        {
            coundtdown = spawntimer;
            StartCoroutine(SpawnEnemies());
        }
    }

    private IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            GameObject newEnemy = Instantiate(enemyPrefabList[UnityEngine.Random.Range(0, enemyPrefabList.Count)], spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Count)].position, Quaternion.identity);
            GameManager.instance.enemyList.Add(newEnemy);
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}

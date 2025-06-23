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

    private int waveCount = 0;

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
        if (coundtdown <= 0 && Vector2.Distance(transform.position, player.transform.position) <= playerDetectionRange && Input.GetKeyDown(KeyCode.L) && GameManager.instance.enemyList.Count == 0)
        {
            coundtdown = spawntimer;
            waveCount++;
            StartCoroutine(SpawnEnemies());
        }
    }

    private IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < enemyCount + 2 * waveCount; i++)
        {
            GameObject newEnemy = Instantiate(enemyPrefabList[UnityEngine.Random.Range(0, enemyPrefabList.Count)], spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Count)].position, Quaternion.identity);
            newEnemy.GetComponent<EnemyStats>().health = waveCount * 10;
            GameManager.instance.enemyList.Add(newEnemy);
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}

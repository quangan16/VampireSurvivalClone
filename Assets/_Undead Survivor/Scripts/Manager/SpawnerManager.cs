using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnerManager : MonoBehaviour
{
    [SerializeField] public GameObject[] enemyPrefabs;
    [SerializeField] public float spawnRadius;
    [SerializeField] public float spawnInterval;


    public void Start()
    {
        StartCoroutine(SpawnAroundTarget());
    }

    public void Init()
    {
        spawnRadius = Camera.main.orthographicSize * Camera.main.aspect;
    }

    public void Update()
    {
        
    }
    public IEnumerator SpawnAroundTarget()
    {
        PlayerController player = GameManager.Instance.player;

        while (player.IsAlive)
        {
            yield return new WaitForSeconds(spawnInterval);
            Instantiate(GetRandomEnemy(), GetRandomPosAroundTarget(player.transform), Quaternion.identity);
        }
       
       
    }

    public GameObject GetRandomEnemy()
    {
        int randomIndex;
        randomIndex = Random.Range(0, enemyPrefabs.Length);
        return enemyPrefabs[randomIndex];
    }

    public Vector2 GetRandomPosAroundTarget(Transform target)
    {
        float randomAngle = Random.Range(0, 2.0f * Mathf.PI);
        float randomPosX = target.transform.position.x + spawnRadius * Mathf.Sin(randomAngle);
        float randomPosY = target.transform.position.y + spawnRadius * Mathf.Cos(randomAngle);

        return new Vector2(randomPosX, randomPosY);
    }
}

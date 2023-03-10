using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance;
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private GameObject Boss;
    private List<GameObject> spawnedEnemies = new List<GameObject>();
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float spawnInterval = 2.0f;

    private void Awake()
    {
        if (instance == null)
            instance = this;

    }

    void SpawnNewWaveOfEnemies()
    {
        if (spawnedEnemies.Count > 0)
            return;
        GameplayUIController.instance.SetWave();
        if ((GameplayUIController.instance.GetWaveCount()) % 5 != 0)
        {

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            int randIndex = Random.Range(0,enemies.Length);
            GameObject newEnemy = Instantiate(enemies[randIndex], spawnPoints[i].position,Quaternion.identity);
            spawnedEnemies.Add(newEnemy);
        }

        }
        else
        {
            Debug.Log("hello");
            
            GameObject Bossenemy = Instantiate(Boss, spawnPoints[2].position, Quaternion.identity);
            spawnedEnemies.Add(Bossenemy);
        }
        //ınform UI about wave number
        //GameplayUIController.instance.SetWave();
    }

    IEnumerator SpawnWave(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SpawnNewWaveOfEnemies();
    }

    private void Start()
    {
        StartCoroutine(SpawnWave(spawnInterval));
    }

    public void CheckToSpawnNewWave(GameObject shipToRemove)
    {
        spawnedEnemies.Remove(shipToRemove);

        if(spawnedEnemies.Count == 0)
            StartCoroutine(SpawnWave(spawnInterval));
    }

    
}

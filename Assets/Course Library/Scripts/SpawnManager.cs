using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;
    public float spawnRange;
    public float enemyCount;
    public int waveNum;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsByType<Enemy>(FindObjectsSortMode.None).Length;
        if(enemyCount == 0)
        {
            waveNum++;
            SpawnWave(waveNum);
            Instantiate(powerupPrefab, generateSpawnpoint(spawnRange), powerupPrefab.transform.rotation);
        }
    }

    void SpawnWave(int enemiesCount)
    {
        for(int i = 1; i <= enemiesCount; i++)
        {
            Instantiate(enemyPrefab, generateSpawnpoint(spawnRange), enemyPrefab.transform.rotation);
        }
    }
    
    private Vector3 generateSpawnpoint(float spawnRange)
    {
        float spawnPointX = Random.Range(-spawnRange, spawnRange);
        float spawnPointZ = Random.Range(-spawnRange, spawnRange);
        Vector3 spawnPoint = new Vector3(spawnPointX, 1, spawnPointZ);
        return spawnPoint;
    }
}

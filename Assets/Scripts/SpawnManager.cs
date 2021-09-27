using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab, _player;
    private GameManager _gameManager;
    // Detrmines boundaries for spawn position
    [SerializeField]
    private float _spawnRangeX, _spawnRangeZ;
    private int _enemyCount, _waveNumber = 0;
    public float spawnPos;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        _player = GameObject.Find("Player");
 
        //While loop, while the condition is true the below code will run.
        while (_gameManager.isGameActive == true)
        {
            SpawnEnemyWave(_waveNumber);
        }

    }

    // Update is called once per frame
    void Update()
    {
        // Sets the number of enemies to all enemies in the scene by referencing the Enemy script.
        _enemyCount = FindObjectsOfType<Enemy>().Length;

        // Spawns a new wave when enemyCount is at zero.
        if (_enemyCount == 0 && _gameManager.isGameActive == true)
        {
            // Increases the wave number each wave and then spawns that many enemies.
            _waveNumber++;
            SpawnEnemyWave(_waveNumber);
        }
    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {
        // Spawns enemies, i++ adds 1 to i each time.
        for (int i = 0; i < enemiesToSpawn; i++)
        {//spawns enemy at randomly set location and correct rotation by calling the GenerateSpawnPosition method

            Instantiate(_enemyPrefab, GenerateSpawnPosition(), _enemyPrefab.transform.rotation);
        }
    }

    private Vector3 GenerateSpawnPosition()
    {

        //randomly determines a position on the scene and stores it in this method
        float spawnPosX = Random.Range(-_spawnRangeX, _spawnRangeX);
        float spawnPosZ = Random.Range(-_spawnRangeZ, _spawnRangeZ);

        Vector3 spawnPos = new Vector3(spawnPosX, 1, spawnPosZ);

        if (Vector3.Distance(spawnPos, _player.transform.position) <= 5f)
        {
            StartCoroutine("DelaySpawnRoutine");
;       }

        do
        {
            return spawnPos; 
        }
        while (Vector3.Distance(spawnPos, _player.transform.position) > 5f);

    }

    IEnumerator DelaySpawnRoutine()
    { 
        yield return new WaitForSeconds(0.5f);
        GenerateSpawnPosition();
    }
   
}

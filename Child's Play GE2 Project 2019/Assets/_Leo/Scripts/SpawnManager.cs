using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform enemyTypeAPrefab;
    //public Transform enemyTypeBPrefab;

    public Transform spawnPoint;

    public float timeBetweenWaves = 5f;

    private float _countdownToNextWave;

    private int _waveIndex = 0;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_countdownToNextWave <= 0f)
        {
            StartCoroutine(WaveSpawner());
            _countdownToNextWave = timeBetweenWaves;
        }

        _countdownToNextWave -= Time.deltaTime;
                
    }

    IEnumerator WaveSpawner()
    {
        _waveIndex++;

        for (int i = 0; i < _waveIndex; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }
    }

    void SpawnEnemy()
    {
        Instantiate(enemyTypeAPrefab, spawnPoint.position, spawnPoint.rotation);
    }


}

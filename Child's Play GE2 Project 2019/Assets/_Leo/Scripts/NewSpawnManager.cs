using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewSpawnManager : MonoBehaviour
{
    [SerializeField]
    private WaveSetup_SO[] _waveSetup;

    //public Transform enemyTypeAPrefab;
    //public Transform enemyTypeBPrefab;

    public Transform spawnPoint;

    public float timeBetweenWaves = 5f;

    private float _countdownToNextWave;

    private int _waveIndex = 0;
    

    // Update is called once per frame
    void Update()
    {
        if (_countdownToNextWave <= 0f)
        {
            StartCoroutine(WaveSpawner());
            _countdownToNextWave = timeBetweenWaves;
        }

        _countdownToNextWave -= Time.deltaTime;

    }

    IEnumerator WaveSpawner()
    {        
        for (int i = 0; i < _waveSetup[_waveIndex].count; i++)
        {
            SpawnEnemy(_waveSetup[_waveIndex].enemy);
            yield return new WaitForSeconds(1f / _waveSetup[_waveIndex].rate); //how long to spawn an enemy during the wave
        }

        _waveIndex++;

        if(_waveIndex == _waveSetup.Length)
        {
            //TODO: LEVEL FINISHED! GOTO NEXT LEVEL
            Debug.Log("Level Finished!");
            this.enabled = false;
        }
    }

    void SpawnEnemy(GameObject enemyToSpawn)
    {
        Instantiate(enemyToSpawn, spawnPoint.position, spawnPoint.rotation);
    }
}

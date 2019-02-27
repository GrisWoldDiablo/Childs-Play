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

    private bool _startNewWave = false;
    

    // Update is called once per frame
    void Update()
    {
        //if (_countdownToNextWave <= 0f)
        //{
        //    StartCoroutine(WaveSpawner());
        //    _countdownToNextWave = timeBetweenWaves;
        //}

        //_countdownToNextWave -= Time.deltaTime;

        if (_startNewWave)//(_countdownToNextWave <= 0f)
        {
            StartCoroutine(WaveSpawner());
            //_countdownToNextWave = int.MaxValue;
            _startNewWave = false;
        }
        //else if(_countdownToNextWave == timeBetweenWaves)   
        //{
        //    _countdownToNextWave -= Time.deltaTime;
        //}
        

        //_countdownToNextWave = Mathf.Clamp(_countdownToNextWave, 0f, Mathf.Infinity);

    }

    void Start()
    {
        _startNewWave = true;
        _countdownToNextWave = 0f;
        //StartCoroutine(WaveSpawner());
    }

    IEnumerator WaveSpawner()
    {        
        for (int i = 0; i < _waveSetup[_waveIndex].count; i++)
        {
            SpawnEnemy(_waveSetup[_waveIndex].enemy);
            yield return new WaitForSeconds(_waveSetup[_waveIndex].rate); //how long to spawn an enemy during the wave
        }
        //yield return new WaitForSeconds(timeBetweenWaves);
        _waveIndex++;

        

        if(_waveIndex == _waveSetup.Length)
        {
            //TODO: LEVEL FINISHED! GOTO NEXT LEVEL
            Debug.Log("Level Finished!");
            this.enabled = false;
        }



        //StartCoroutine(WaveSpawner());
        //_countdownToNextWave = timeBetweenWaves;
        //_startNewWave = true;
        yield return new WaitForSeconds(timeBetweenWaves);
        _startNewWave = true;
    }

    void SpawnEnemy(GameObject enemyToSpawn)
    {
        Instantiate(enemyToSpawn, spawnPoint.position, spawnPoint.rotation);
    }

    IEnumerator TimeBetweenWavesCountdown()
    {
        _countdownToNextWave = 1 * Time.deltaTime;
        yield return new WaitForSeconds(1);
        if (_countdownToNextWave == timeBetweenWaves)
        {

        }
        

    }
}

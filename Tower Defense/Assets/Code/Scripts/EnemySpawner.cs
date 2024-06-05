using System;
using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using static System.Net.Mime.MediaTypeNames;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner main;

    [Header("References")]
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private UIManager UIManagerComponent;
    [SerializeField] public TextMeshProUGUI Text;

    [Header("Attributes")]
    [SerializeField] private int baseEnemies = 8;
    [SerializeField] private float enemiesPerSecond = 0.5f;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float difficultyScalingFactor = 0.75f;
    [SerializeField] private float enemiesPerSecondCap = 15f;
    [SerializeField] private float maxEnemiesAtOnce = 25f;

    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent();
    public delegate void RoundEndedHandler(int roundThatEnded);
    public event RoundEndedHandler OnRoundEnded;

    private int currentWave = 1;
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;
    private float eps; //enemies per second
    private bool isSpawning = false;

    public bool isBetweenWaves = true;

    private void Awake()
    {
        main = this;
        onEnemyDestroy.AddListener(EnemyDestroyed);
    }

    private void Start()
    {
        //StartCoroutine(StartWave());
        UIManagerComponent.UpdateWave(currentWave);
    }

    private void EnemyDestroyed()
    {
        if(Castle.main.GetHealth() <= 0)
        {
            //Game Over
            Debug.Log("Game Over");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        enemiesAlive--;
    }
  
    private IEnumerator StartWave()
    {
        isBetweenWaves = true;
        yield return new WaitForSeconds(timeBetweenWaves);

        isBetweenWaves = false;
        isSpawning = true;
        enemiesLeftToSpawn = EnemiesPerWave();

        eps = EnemiesPerSecond();
        
    }

    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficultyScalingFactor));
    }
    private float EnemiesPerSecond()
    {
        return Mathf.Clamp(enemiesPerSecond * Mathf.Pow(currentWave, difficultyScalingFactor), 0f, enemiesPerSecondCap);
    }

    private void SpawnEnemy()
    {
        int index = UnityEngine.Random.Range(0, enemyPrefabs.Length);
        GameObject prefabToSpawn = enemyPrefabs[index];
        Instantiate(prefabToSpawn, LevelManager.main.startPoint.position, Quaternion.identity); 
    }

    // Update is called once per frame
    void Update()
    {
        Text.text = enemiesAlive.ToString();
        if (!isSpawning)
        {
            return;
        }
        timeSinceLastSpawn += Time.deltaTime;

        if(timeSinceLastSpawn >= (1f/eps) && enemiesLeftToSpawn > 0)
        {
            if(enemiesAlive <= maxEnemiesAtOnce)
            {
                SpawnEnemy();
                enemiesLeftToSpawn--;
                enemiesAlive++;
                timeSinceLastSpawn = 0f;
            }
            
        }

        if(enemiesAlive == 0 && enemiesLeftToSpawn == 0)
        {
            EndWave();
        }
        
    }

    private void EndWave()
    {
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        ChangeWave();
        UIManagerComponent.UpdateWave(currentWave);
        OnRoundEnded?.Invoke(currentWave);
        
    }

    public void ChangeWave()
    {
        currentWave++;
    }
    public void IncrementAlive()
    {
        enemiesAlive++;
    }

    public void BeginNextWave()
    {
        StartCoroutine(StartWave());
    }
}

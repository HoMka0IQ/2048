
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] SpawnSystem spawnSystem;
    [Header("Start Spawn Area")]
    [SerializeField] private Vector3 spawnAreaSize = new Vector3(5f, 0f, 5f);
    [SerializeField] private Transform centerStartArea;


    [Header("Spawn Settings")]
    [SerializeField] private int cubesToSpawn = 3;

    public event System.Action OnGameOver;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        StartCubesSpawn();
    }
    public void StartCubesSpawn()
    {
        for (int i = 0; i < cubesToSpawn; i++)
        {
            Vector3 randomPosition = GetRandomPointInArea();
            spawnSystem.SpawnRandom(randomPosition);
        }
    }

    private Vector3 GetRandomPointInArea()
    {
        Vector3 center = centerStartArea.position;

        float randomX = Random.Range(-spawnAreaSize.x / 2f, spawnAreaSize.x / 2f);
        float randomY = Random.Range(-spawnAreaSize.y / 2f, spawnAreaSize.y / 2f);
        float randomZ = Random.Range(-spawnAreaSize.z / 2f, spawnAreaSize.z / 2f);

        return center + new Vector3(randomX, randomY, randomZ);
    }
    public void EndGame()
    {
        OnGameOver?.Invoke();
    }
    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void OnDrawGizmos()
    {
        if (centerStartArea == null)
            return;

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(centerStartArea.position, spawnAreaSize);
    }
}

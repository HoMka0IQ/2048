using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeExecutor : MonoBehaviour
{
    public static MergeExecutor Instance { get; private set; }

    [SerializeField] private SpawnSystem spawnSystem;
    [SerializeField] private CubePool cubePool;
    [SerializeField] private ScoreSystem scoreSystem;

    private void Awake()
    {
        Instance = this;
    }

    public void Execute(
        CubeMergeHandler a,
        CubeMergeHandler b,
        int newValue,
        Vector3 position)
    {
        Cube mergedCube = spawnSystem.Spawn(newValue, position);

        mergedCube.View.PlayPopupAnimation();

        Rigidbody rb = mergedCube.GetComponent<Rigidbody>();

        rb.AddForce(Vector3.up * 5f, ForceMode.Impulse);

        
        Vector3 randomTorque = Random.insideUnitSphere * 5f;
        rb.AddTorque(randomTorque, ForceMode.Impulse);

        cubePool.Release(a.GetComponent<Cube>());
        cubePool.Release(b.GetComponent<Cube>());

        scoreSystem.AddScore(newValue / 4);

        if (newValue >= 2048)
        {
            GameManager.Instance.EndGame();
        }
    }
}

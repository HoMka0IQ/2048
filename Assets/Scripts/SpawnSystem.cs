using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSystem : MonoBehaviour
{

    [SerializeField] private CubePool cubePool;
    [SerializeField] private CubeVisualDatabase visualDatabase;


    public Cube Spawn(int value, Vector3 position)
    {
        Cube cube = cubePool.Get();
        cube.transform.position = position;
        cube.transform.rotation = Quaternion.identity;

        cube.Initialize(value, visualDatabase);

        return cube;
    }
    public Cube SpawnRandom(Vector3 position)
    {
        int value = GetRandomValue();
        return Spawn(value, position);
    }

    private int GetRandomValue()
    {
        return Random.value <= 0.75f ? 2 : 4;
    }
}

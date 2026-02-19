using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CubePool : MonoBehaviour
{
    [SerializeField] private Cube cubePrefab;
    [SerializeField] private int initialSize = 6;

    private readonly Queue<Cube> _pool = new();

    private void Awake()
    {
        for (int i = 0; i < initialSize; i++)
        {
            Cube cube = Instantiate(cubePrefab, transform);
            cube.Deactivate();
            _pool.Enqueue(cube);
        }
    }

    public Cube Get()
    {
        if (_pool.Count > 0)
            return _pool.Dequeue();

        Cube cube = Instantiate(cubePrefab, transform);
        cube.Deactivate();
        return cube;
    }

    public void Release(Cube cube)
    {
        cube.Deactivate();
        _pool.Enqueue(cube);
    }
}

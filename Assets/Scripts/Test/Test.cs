using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] SpawnSystem _spawnSystem;
    [SerializeField] PlayerInputController _inputController;
    [SerializeField] Transform spawnPoint;


    private void Start()
    {
        //SpawnCudes();
    }
    public void SpawnCudes()
    {
        for (int i = 0; i < 32; i++)
        {
            _spawnSystem.Spawn(2, transform.position + Vector3.up * (i + 1) * 5);
        }
    }
}

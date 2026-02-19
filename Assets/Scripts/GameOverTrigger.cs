using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverTrigger : MonoBehaviour
{
    [SerializeField] private LayerMask cubeLayer;

    private void OnTriggerEnter(Collider other)
    {
        if (cubeLayer == (cubeLayer | (1 << other.gameObject.layer)))
        {
            GameManager.Instance.EndGame();
        }
    }
}

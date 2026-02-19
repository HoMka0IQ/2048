using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeLauncher : MonoBehaviour
{
    [SerializeField] private float maxHorizontalOffset = 3f;
    [SerializeField] private float maxLaunchForce = 15f;

    private Rigidbody _rigidbody;
    private Vector3 startPosition;
    private bool launched;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        startPosition = transform.position;
    }

    public void SetHorizontalOffset(Vector3 offset)
    {
        if (launched)
            return;
        _rigidbody.isKinematic = true;

        transform.position = offset;
    }

    public void Launch(float normalizedForce)
    {
        if (launched)
            return;
        _rigidbody.isKinematic = false;
        launched = true;

        float force = normalizedForce * maxLaunchForce;

        _rigidbody.AddForce(Vector3.forward * force, ForceMode.Impulse);
    }
    public void Reset()
    {
        launched = false;
    }
}

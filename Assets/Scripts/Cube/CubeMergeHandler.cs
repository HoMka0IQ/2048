using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMergeHandler : MonoBehaviour
{
    [SerializeField] private float minMergeImpulse = 1.5f;

    private CubeView cubeView;

    private void Awake()
    {
        cubeView = GetComponent<CubeView>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (cubeView.IsMerging)
            return;

        if (!collision.collider.TryGetComponent(out CubeMergeHandler otherHandler))
            return;

        CubeView otherView = otherHandler.cubeView;

        if (otherView.IsMerging)
            return;

        if (cubeView.Value != otherView.Value)
            return;

        if (!HasEnoughImpulse(collision))
            return;

        TryMerge(otherHandler);
    }

    private bool HasEnoughImpulse(Collision collision)
    {
        return collision.impulse.magnitude >= minMergeImpulse;
    }

    private void TryMerge(CubeMergeHandler other)
    {
        // merge only once (deterministic side)
        if (GetInstanceID() > other.GetInstanceID())
            return;

        Vector3 mergePosition =
            (transform.position + other.transform.position) * 0.5f;

        int newValue = cubeView.Value * 2;

        cubeView.MarkAsMerging();
        other.cubeView.MarkAsMerging();



        MergeExecutor.Instance.Execute(
            this,
            other,
            newValue,
            mergePosition
        );
    }
}

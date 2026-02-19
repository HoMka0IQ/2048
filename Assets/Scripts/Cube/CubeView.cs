using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CubeView : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private TextMeshPro[] valueText;
    [SerializeField] private Rigidbody _rigidbody;

    [Header("Animation")]
    [SerializeField] private float mergeScaleMultiplier = 0.5f;
    [SerializeField] private float popupAnimationDuration = 0.1f;
    [SerializeField] private ParticleSystem mergeVfx;

    [Header("Spawn Animation")]
    [SerializeField] private float spawnStartScale = 0.2f;
    [SerializeField] private float spawnAnimationDuration = 0.15f;

    private CubeVisualDatabase visualDatabase;
    private Coroutine animationRoutine;

    public int Value { get; private set; }
    public bool IsMerging { get; private set; }


    public void Initialize(int value, CubeVisualDatabase database)
    {
        IsMerging = false;
        Value = value;
        visualDatabase = database;

        ApplyVisual(value);
        ResetPhysicsState();
        transform.localScale = Vector3.one;
    }

    public void MarkAsMerging()
    {
        IsMerging = true;
        _rigidbody.isKinematic = true;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
    }

    public void PlayPopupAnimation()
    {
        if (animationRoutine != null)
            StopCoroutine(animationRoutine);

        animationRoutine = StartCoroutine(MergeAnimationRoutine());
        if (mergeVfx != null)
            mergeVfx.Play();
    }
    public void PlaySpawnAnimation()
    {
        if (animationRoutine != null)
            StopCoroutine(animationRoutine);

        animationRoutine = StartCoroutine(SpawnAnimationRoutine());
    }


    private void ApplyVisual(int value)
    {
        for (int i = 0; i < valueText.Length; i++)
        {
            valueText[i].text = value.ToString();

            CubeVisualData data = visualDatabase.GetVisual(value);
            if (data != null)
            {
                meshRenderer.material = data.Material;
                valueText[i].color = data.TextColor;
            }
        }
    }

    private void ResetPhysicsState()
    {
        _rigidbody.isKinematic = false;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
    }

    private IEnumerator MergeAnimationRoutine()
    {
        Vector3 baseScale = Vector3.one;
        Vector3 targetScale = baseScale * mergeScaleMultiplier;

        float time = 0f;

        while (time < popupAnimationDuration)
        {
            time += Time.deltaTime;
            float t = time / popupAnimationDuration;
            transform.localScale = Vector3.Lerp(baseScale, targetScale, t);
            yield return null;
        }

        time = 0f;

        while (time < popupAnimationDuration)
        {
            time += Time.deltaTime;
            float t = time / popupAnimationDuration;
            transform.localScale = Vector3.Lerp(targetScale, baseScale, t);
            yield return null;
        }

        transform.localScale = baseScale;
    }
    private IEnumerator SpawnAnimationRoutine()
    {
        Vector3 baseScale = Vector3.one;
        Vector3 startScale = Vector3.one * spawnStartScale;

        float time = 0f;
        transform.localScale = startScale;

        while (time < spawnAnimationDuration)
        {
            time += Time.deltaTime;
            float t = time / spawnAnimationDuration;
            transform.localScale = Vector3.Lerp(startScale, baseScale, t);
            yield return null;
        }

        transform.localScale = baseScale;
    }
}

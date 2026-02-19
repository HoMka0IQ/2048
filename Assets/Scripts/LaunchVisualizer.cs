using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchVisualizer : MonoBehaviour
{
    [SerializeField] private Transform arrowLine;
    [SerializeField] private float maxDistance = 20f;

    [SerializeField] private float lineHeightOffset = 1.5f;
    [SerializeField] private float lineOffsetAnimSpeed = 1f;

    [SerializeField] private float maxAarrowSize = 1.1f;
    [SerializeField] private float minAarrowSize = 0.9f;
    [SerializeField] private Material arrowLineMat;

    private Vector3 endPoint;
    private void Start()
    {
        Hide();
    }
    public void ShowArrowLine(Vector3 startPos, float launchForce)
    {
        // Кидаємо промінь
        if (Physics.Raycast(startPos, Vector3.forward, out RaycastHit hit, maxDistance))
        {
            endPoint = hit.point;
        }
        else
        {
            endPoint = startPos + Vector3.forward * maxDistance;
        }

        Vector3 direction = endPoint - startPos;
        float rayDistance = direction.magnitude;

        // Довжина стрілки = launchForce, але не більше rayDistance
        float arrowDistance = Mathf.Min(rayDistance, Mathf.Lerp(minAarrowSize,maxAarrowSize, launchForce) * 10);

        // Позиція — середина стрілки
        Vector3 midPoint = startPos + direction.normalized * (arrowDistance * 0.5f) - new Vector3(0, 0.49f, 0);
        arrowLine.position = midPoint;

        // Обертання (як у тебе було)
        if (direction != Vector3.zero)
            arrowLine.rotation = Quaternion.Euler(new Vector3(0, 90, 0));

        // Масштаб стрілки по X
        Vector3 scale = arrowLine.localScale;
        scale.x = arrowDistance / 10; // або твій коефіцієнт
        arrowLine.localScale = scale;

        // Масштаб текстури
        arrowLineMat.SetTextureScale("_MainTex", new Vector2(arrowDistance * lineHeightOffset, 1f));

        arrowLine.gameObject.SetActive(true);
    }
    private void Update()
    {
        if (arrowLine.gameObject.activeSelf == false)
            return;

        arrowLineMat.mainTextureOffset = new Vector2(Time.time * -lineOffsetAnimSpeed, 0); // animate texture
    }

    public void Hide()
    {
        arrowLine.gameObject.SetActive(false);
    }
}

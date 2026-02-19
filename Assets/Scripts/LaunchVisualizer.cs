using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchVisualizer : MonoBehaviour
{
    [SerializeField] private Transform arrowLine;
    [SerializeField] private float maxDistance = 20f;

    [SerializeField] private float lineHeightOffset = 1.5f;
    [SerializeField] private float lineOffsetAnimSpeed = 1f;

    [SerializeField] private Material arrowLineMat;

    private Vector3 endPoint;
    private void Start()
    {
        Hide();
    }
    public void ShowArrowLine(Vector3 startPos)
    {
        if (Physics.Raycast(startPos, Vector3.forward, out RaycastHit hit, maxDistance))
        {
            endPoint = hit.point;
        }
        else
        {
            endPoint = startPos + Vector3.forward * maxDistance;
        }

        Vector3 direction = endPoint - startPos;
        float distance = direction.magnitude;

        Vector3 midPoint = startPos + direction * 0.5f - new Vector3(0, 0.49f, 0);

        arrowLine.position = midPoint;

        if (direction != Vector3.zero)
            arrowLine.rotation = Quaternion.Euler(new Vector3(0, 90, 0));

        Vector3 scale = arrowLine.localScale;
        scale.x = distance / 10; // stretch along X
        arrowLineMat.SetTextureScale("_MainTex", new Vector2(distance * lineHeightOffset, 1f));
        
        arrowLine.localScale = scale;

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

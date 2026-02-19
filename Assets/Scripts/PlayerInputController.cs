using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera _camera;
    private Cube currentCube;
    [SerializeField] private Transform playerCubePoint;
    [SerializeField] private LaunchVisualizer launchVisualizer;

    [SerializeField] SpawnSystem spawnSystem;

    [Header("Settings")]
    [SerializeField] private float maxDragDistance = 3f;

    private Plane dragPlane;
    private Vector3 dragStartWorld;
    private bool isDragging;

    bool lockInput;

    private void Awake()
    {
        dragPlane = new Plane(Vector3.up, Vector3.zero);
    }
    private void Start()
    {
        InitCube();

        GameManager.Instance.OnGameOver += LockInput;
    }
    private void OnDisable()
    {
        GameManager.Instance.OnGameOver -= LockInput;
    }
    void LockInput()
    {
        lockInput = true;
    }
    private void Update()
    {
        if (Input.touchCount == 0)
            return;

        if (lockInput)
            return;

        Touch touch = Input.GetTouch(0);

        switch (touch.phase)
        {
            case TouchPhase.Began:
                BeginDrag(touch.position);
                break;

            case TouchPhase.Moved:
                UpdateDrag(touch.position);
                break;

            case TouchPhase.Ended:
                EndDrag(touch.position);
                break;
        }
    }
    public void InitCube()
    {
        currentCube = spawnSystem.Spawn(2, playerCubePoint.position);

        currentCube.Launcher.SetHorizontalOffset(playerCubePoint.position);
        currentCube.View.PlaySpawnAnimation();
    }

    private void BeginDrag(Vector2 screenPosition)
    {
        if (!TryGetWorldPoint(screenPosition, out dragStartWorld))
            return;

        isDragging = true;
    }

    private void UpdateDrag(Vector2 screenPosition)
    {
        if (!isDragging)
            return;

        if (!TryGetWorldPoint(screenPosition, out var worldPoint))
            return;

        if (currentCube == null)
            return;

        float deltaX = worldPoint.x - dragStartWorld.x;
        deltaX = Mathf.Clamp(deltaX, -maxDragDistance, maxDragDistance);

        Vector3 targetPosition = new Vector3(deltaX, playerCubePoint.position.y, playerCubePoint.position.z);

        currentCube.Launcher.SetHorizontalOffset(targetPosition);

        if (launchVisualizer != null)
            launchVisualizer.ShowArrowLine(currentCube.transform.position);
    }



    private void EndDrag(Vector2 screenPosition)
    {
        if (!isDragging)
            return;

        isDragging = false;

        if (!TryGetWorldPoint(screenPosition, out var worldPoint))
            return;

        if (currentCube == null)
            return;

        currentCube.Launcher.Launch(1);

        Invoke(nameof(InitCube), 1f);

        if (launchVisualizer != null)
            launchVisualizer.Hide();

        currentCube = null;
    }

    private bool TryGetWorldPoint(Vector2 screenPosition, out Vector3 worldPoint)
    {
        Ray ray = _camera.ScreenPointToRay(screenPosition);

        if (dragPlane.Raycast(ray, out float enter))
        {
            worldPoint = ray.GetPoint(enter);
            return true;
        }

        worldPoint = default;
        return false;
    }
}

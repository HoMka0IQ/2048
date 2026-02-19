using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    private CubeView view;
    private CubeMergeHandler mergeHandler;
    private CubeLauncher launcher;

    public CubeView View => view;
    public CubeMergeHandler MergeHandler => mergeHandler;

    public CubeLauncher Launcher => launcher;

    private void Awake()
    {
        view = GetComponent<CubeView>();
        mergeHandler = GetComponent<CubeMergeHandler>();
        launcher = GetComponent<CubeLauncher>();
    }

    public void Initialize(int value, CubeVisualDatabase database)
    {
        gameObject.SetActive(true);
        view.Initialize(value, database);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
        launcher.Reset();
    }
}

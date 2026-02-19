using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Configs/CubeVisualDatabase")]
public class CubeVisualDatabase : ScriptableObject
{
    [SerializeField] private List<CubeVisualData> _visuals;

    private Dictionary<int, CubeVisualData> _cache;

    private void OnEnable()
    {
        _cache = new Dictionary<int, CubeVisualData>();

        foreach (var visual in _visuals)
        {
            if (!_cache.ContainsKey(visual.Value))
                _cache.Add(visual.Value, visual);
        }
    }

    public CubeVisualData GetVisual(int value)
    {
        return _cache.TryGetValue(value, out var data) ? data : null;
    }
}
[Serializable]
public class CubeVisualData
{
    public int Value;
    public Material Material;
    public Color TextColor;
}

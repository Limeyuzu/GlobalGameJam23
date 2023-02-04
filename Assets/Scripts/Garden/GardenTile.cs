
using Assets.GenericTools.Event;
using System;
using UnityEngine;

public class GardenTile : MonoBehaviour
{
    [SerializeField] Material DirtMaterial;
    [SerializeField] Material GrassMaterial;

    private Vector2Int? _gridPosition;
    private double _growthValue = 0;

    public double GetGrowthValue() => _growthValue;

    public void InitGridPosition(Vector2Int initPosition)
    {
        if (!_gridPosition.HasValue)
        {
            _gridPosition = initPosition;
            EventManager.Subscribe(GameEvent.PlayerMoved, (newPosition) =>
            {
                if ((Vector2Int)newPosition == _gridPosition)
                {
                    OnPlayerEnter();
                }
            });
        }
    }

    public void OnPlayerEnter()
    {
        if (_growthValue <= 0)
        {
            GrowNewWeed();
        }
    }

    private void GrowNewWeed()
    {
        _growthValue = 1;
    }

    private void Update()
    {
        UpdateMaterial();
    }

    private void UpdateMaterial()
    {
        var materialToUse = _growthValue <= 0 ? DirtMaterial : GrassMaterial;
        GetComponent<MeshRenderer>().material = materialToUse;
    }
}

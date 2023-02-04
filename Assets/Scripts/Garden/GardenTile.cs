
using Assets.GenericTools.Event;
using System;
using UnityEngine;

public class GardenTile : MonoBehaviour
{
    [SerializeField] Material DirtMaterial;
    [SerializeField] Material GrassMaterial;
    [SerializeField] GameObject WeedPrefab;
    [SerializeField] float GrowRate = 0.1f;

    private Vector2Int? _gridPosition;
    [SerializeField] float _growthValue = 0; //private
    private GameObject _weed;

    public float GetGrowthValue() => _growthValue;

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

    private void Start()
    {
        var visualLocation = new Vector3(_gridPosition.Value.x, 0, _gridPosition.Value.y);
        _weed = Instantiate(WeedPrefab, visualLocation, Quaternion.identity);
        _weed.SetActive(false);
    }

    private void Update()
    {
        UpdateMaterial();

        _weed.SetActive(_growthValue > 0);

        if (_growthValue > 0)
        {
            Grow();
        }
    }

    private void Grow()
    {
        _growthValue += GrowRate * Time.deltaTime;
        var currentScale = _weed.transform.localScale;
        _weed.transform.localScale = new Vector3(currentScale.x, _growthValue, currentScale.z);
    }

    private void UpdateMaterial()
    {
        var materialToUse = _growthValue <= 0 ? DirtMaterial : GrassMaterial;
        GetComponent<MeshRenderer>().material = materialToUse;
    }
}

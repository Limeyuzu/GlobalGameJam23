
using Assets.GenericTools.Event;
using UnityEngine;
using Random = UnityEngine.Random;

public class GardenTile : MonoBehaviour
{
    [SerializeField] Material DirtMaterial;
    [SerializeField] Material GrassMaterial;
    [SerializeField] float GrowthRate = 0.1f;
    [SerializeField] float GrowthRandomness = 0.2f;
    [SerializeField] float MaxGrowth = 3f;

    private Vector2Int? _gridPosition;
    [SerializeField] float _growthValue = 0; //private

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
            EventManager.Subscribe(GameEvent.MowerMoved, (mower) =>
            {
                if (((Lawnmower)mower).gridPosition == _gridPosition)
                {
                    OnMowerEnter((Lawnmower)mower);
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

    public void OnMowerEnter(Lawnmower mower)
    {
        if (_growthValue > 0)
        {
            if (_growthValue > 3)
            {
                mower.ReSpawn();
            }
            else if (_growthValue > 2)
            {
                mower.SlowerMower();
            }
            CutDownWeed();
        }
    }

    private void GrowNewWeed()
    {
        _growthValue = 1;
    }


    private void CutDownWeed()
    {
        _growthValue = 0;
    }

    private void Start()
    {
        GrowthRate *= 1 + Random.Range(-GrowthRandomness, GrowthRandomness);
    }

    private void Update()
    {
        UpdateMaterial();

        Grow();
    }

    private void Grow()
    {
        if (_growthValue <= 0 || _growthValue >= MaxGrowth)
            return;

        _growthValue += GrowthRate * Time.deltaTime;
    }

    private void UpdateMaterial()
    {
        var materialToUse = _growthValue <= 0 ? DirtMaterial : GrassMaterial;
        GetComponent<MeshRenderer>().material = materialToUse;
    }
}


using Assets.GenericTools.Event;
using Assets.GenericTools.Grid;
using UnityEngine;
using Random = UnityEngine.Random;

public class GardenTile : MonoBehaviour, IGameGridTile
{
    [SerializeField] Material DirtMaterial;
    [SerializeField] Material GrassMaterial;
    [SerializeField] float GrowthRate = 0.1f;
    [SerializeField] float GrowthRandomness = 0.2f;
    [SerializeField] float MaxGrowth = 3f;

    private Vector2Int? _gridPosition;
    [SerializeField] float _growthValue = 0; //private
    private Material _dirtMaterialInstance;
    private Material _grassMaterialInstance;

    public float GetGrowthValue() => _growthValue;
    public bool IsGrowing() => _growthValue >= 1;
    public Vector2Int GetPosition() => _gridPosition.Value;

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
        GrowNewWeed();
    }

    public void OnMowerEnter(Lawnmower mower)
    {
        if (_growthValue > 0)
        {
            if (_growthValue > 3)
            {
                mower.SlowerMower(0.2f);
            }
            else if (_growthValue > 2)
            {
                mower.SlowerMower();
            }
            DestroyWeed();
        }
    }

    public void GrowNewWeed()
    {
        if (!IsGrowing())
        {
            _growthValue = 1;
            EventManager.Emit(GameEvent.WeedCreated, _gridPosition.Value);
        }
    }

    private void DestroyWeed()
    {
        _growthValue = 0;
        EventManager.Emit(GameEvent.WeedDestroyed, _gridPosition.Value);
    }

    private void Start()
    {
        GrowthRate *= 1 + Random.Range(-GrowthRandomness, GrowthRandomness);

        // randomise the texture offset to break up repeating patterns
        _dirtMaterialInstance = Instantiate(DirtMaterial);
        _dirtMaterialInstance.mainTextureOffset = new Vector2(Random.Range(-5f, 5f), Random.Range(-5f, 5f));
        _grassMaterialInstance = Instantiate(GrassMaterial);
        _grassMaterialInstance.mainTextureOffset = new Vector2(Random.Range(-5f, 5f), Random.Range(-5f, 5f));
    }

    private void Update()
    {
        UpdateMaterial();

        Grow();
    }

    private void Grow()
    {
        if (!IsGrowing() || _growthValue >= MaxGrowth)
            return;

        _growthValue += GrowthRate * Time.deltaTime;
    }

    private void UpdateMaterial()
    {
        var meshRenderer = GetComponent<MeshRenderer>();
        var materialToUse = _growthValue <= 0 ? _dirtMaterialInstance : _grassMaterialInstance;
        meshRenderer.material = materialToUse;
    }
}

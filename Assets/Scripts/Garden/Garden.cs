using Assets.GenericTools.Event;
using Assets.GenericTools.Grid;
using System.Linq;
using UnityEngine;

public class Garden : MonoBehaviour
{
    [SerializeField] GardenTile GardenTilePrefab;

    private IGameGrid<GardenTile> _gameGrid;

    private void Start()
    {
        SetupEventListeners();
        InitGardenTiles();
    }

    public double GetTotalGrowth() => _gameGrid.Select(g => g.GetGrowthValue()).Sum();

    public int Height() => _gameGrid.Height();
    public int Width() => _gameGrid.Width();

    private void InitGardenTiles()
    {
        _gameGrid = new GameGrid<GardenTile>(Globals.GridSizeX, Globals.GridSizeY);
        for (int x = 0; x < _gameGrid.Width(); x++)
        {
            for (int y = 0; y < _gameGrid.Height(); y++)
            {
                var coords = new Vector2Int(x, y);
                var visualLocation = new Vector3(x, 0, y);
                var gardenTile = Instantiate(GardenTilePrefab, visualLocation, Quaternion.identity);
                gardenTile.InitGridPosition(coords);
                gardenTile.transform.parent = this.transform;

                _gameGrid.Set(coords, gardenTile);
            }
        }
    }

    private void SetupEventListeners()
    {
        EventManager.Subscribe(GameEvent.PlayerMoved, (object text) =>
        {
            Debug.Log(text);
        });
    }

    private void Update()
    {
        GrowWeeds();
    }

    private void GrowWeeds()
    {
    }
}

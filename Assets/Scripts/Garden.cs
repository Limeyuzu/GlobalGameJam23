using Assets.GenericTools.Event;
using Assets.GenericTools.Grid;
using System.Linq;
using UnityEngine;

public class Garden : MonoBehaviour
{
    [SerializeField] Transform Weedy;

    private IGameGrid<GardenTile> _gameGrid;

    private void Start()
    {
        EventManager.Subscribe(GameEvent.PlayerMoved, (object text) =>
        {
            Debug.Log(text);
        });
        _gameGrid = new GameGrid<GardenTile>(10, 20);
    }

    public double GetTotalGrowth() => _gameGrid.Select(g => g.GetGrowthValue()).Sum();

    public int Height() => _gameGrid.Height();
    public int Width() => _gameGrid.Width();

    private void Update()
    {
        //GrowWeeds();
    }

    private void GrowWeeds()
    {
        throw new System.NotImplementedException();
    }
}

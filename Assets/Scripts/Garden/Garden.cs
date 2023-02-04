using Assets.GenericTools.Event;
using Assets.GenericTools.Grid;
using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class Garden : MonoBehaviour
{
    [SerializeField] GardenTile GardenTilePrefab;

    private IGameGrid<GardenTile> _gameGrid;

    private void Start()
    {
        InitGardenTiles();
        StartCoroutine(updateScore());
    }

    IEnumerator updateScore()
    {
        while (true)
        {
            EventManager.Emit(GameEvent.ScoreIncremented, getCurrentScoreToIncrement());

            // wait for seconds
            yield return new WaitForSeconds(1f);
        }
    }

    private int getCurrentScoreToIncrement()
    {
        return (int)Mathf.Floor(GetTotalGrowth()) * 10;
    }
    public float GetTotalGrowth() => _gameGrid.Select(g => g.GetGrowthValue()).Sum();

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
}

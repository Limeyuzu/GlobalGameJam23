using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.GenericTools.Grid
{
    public class GameGrid<T> : IGameGrid<T>, IEnumerable<T> where T : IGameGridTile
    {
        private readonly T[,] _data;

        public GameGrid(int width, int height)
        {
            _data = new T[width, height];
        }

        public T Get(Vector2Int pos)
        {
            if (IsWithinGrid(pos))
                return _data[pos.x, pos.y];
            throw new ArgumentException("Accessing grid out of bounds: ", pos.ToString());
        }

        public void Set(Vector2Int pos, T value) => _data[pos.x, pos.y] = value;

        public int Width() => _data.GetLength(0);

        public int Height() => _data.GetLength(1);

        public bool IsWithinGrid(Vector2Int pos) => pos.x >= 0 && pos.y >= 0 && pos.x < Width() && pos.y < Height();

        public bool IsEdgeTile(Vector2Int pos) => pos.x == 0 || pos.y == 0 || pos.x == Width() - 1 || pos.y == Height() - 1;

        public IEnumerable<T> GetFloodFilledTiles(Vector2Int fillAt, Predicate<T> isPositiveTileFn) => 
            GetFloodFilledPositions(fillAt, isPositiveTileFn, new List<Vector2Int>()).Select(Get);

        public List<Vector2Int> GetFloodFilledPositions(Vector2Int fillAt, Predicate<T> isPositiveTileFn, List<Vector2Int> visited)
        {
            var currentTile = Get(fillAt);
            if (isPositiveTileFn(currentTile) || visited.Contains(fillAt))
            {
                return new List<Vector2Int>();
            }

            visited.Add(fillAt);

            var region = new List<Vector2Int> { fillAt };

            var negativeAdjacents = GetAdjacentTilePositions(fillAt)
                .Select(Get)
                .Where(t => !isPositiveTileFn(t));

            foreach (var adjacent in negativeAdjacents)
            {
                region.AddRange(GetFloodFilledPositions(adjacent.GetPosition(), isPositiveTileFn, visited));
            }

            return region;
        }

        public IEnumerable<Vector2Int> GetAdjacentTilePositions(Vector2Int pos)
        {
            var tilesPositions = new List<Vector2Int>
            {
                new Vector2Int(pos.x - 1, pos.y),
                new Vector2Int(pos.x + 1, pos.y),
                new Vector2Int(pos.x, pos.y - 1),
                new Vector2Int(pos.x, pos.y + 1)
            };

            return tilesPositions.Where(IsWithinGrid);
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int x = 0; x < Width(); x++)
            {
                for (int y = 0; y < Height(); y++)
                {
                    yield return _data[x, y];
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}

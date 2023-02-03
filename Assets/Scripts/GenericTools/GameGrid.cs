using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.GenericTools.Grid
{
    public class GameGrid<T> : IGameGrid<T>, IEnumerable<T>
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

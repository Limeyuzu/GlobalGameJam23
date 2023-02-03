using System.Collections.Generic;
using UnityEngine;

namespace Assets.GenericTools.Grid
{
    public interface IGameGrid<T> : IEnumerable<T>
    {
        public T Get(Vector2Int pos);
        public void Set(Vector2Int pos, T value);
        public int Width();
        public int Height();
        public bool IsWithinGrid(Vector2Int pos);
    }
}

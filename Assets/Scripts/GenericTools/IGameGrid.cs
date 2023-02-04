using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.GenericTools.Grid
{
    public interface IGameGrid<T> : IEnumerable<T> where T : IGameGridTile
    {
        T Get(Vector2Int pos);
        void Set(Vector2Int pos, T value);
        int Width();
        int Height();
        bool IsWithinGrid(Vector2Int pos);
        bool IsEdgeTile(Vector2Int pos);
        IEnumerable<Vector2Int> GetAdjacentTilePositions(Vector2Int pos);

        /// <summary>
        /// Checks if the (negative) tile at fillAt can be flood filled with positive tiles, 
        /// without touching the edge of the grid. i.e. it has been encircled by positive tiles
        /// </summary>
        /// <param name="fillAt">position of the negative tile to try to flood fill</param>
        /// <param name="isPositiveTileFn">function to determine if the tile is considered positive or negative</param>
        /// <returns>A list of flood-fillable tiles if true, else an empty list</returns>
        IEnumerable<T> GetFloodFilledTiles(Vector2Int fillAt, Predicate<T> isPositiveTileFn);
    }
}

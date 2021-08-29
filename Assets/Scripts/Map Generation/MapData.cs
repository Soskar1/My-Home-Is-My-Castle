using UnityEngine;
using System.Collections.Generic;

namespace Map.Generation
{
    public struct MapData
    {
        public bool[] obstacleArr;
        public List<KnightPiece> knightPieces;
        public Vector2 startPos;
        public Vector2 exitPos;
        public List<Vector2> path;
    }
}

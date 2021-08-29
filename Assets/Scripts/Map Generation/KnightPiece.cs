using UnityEngine;
using System.Collections.Generic;

namespace Map.Generation
{
    public class KnightPiece
    {
        public static List<Vector2> possibleMoves = new List<Vector2>
        {
            new Vector2(-1, 2),
            new Vector2(1, 2),
            new Vector2(-1, -2),
            new Vector2(1, -2),
            new Vector2(-2, -1),
            new Vector2(-2, 1),
            new Vector2(2, -1),
            new Vector2(2, 1)
        };

        private Vector2 _position;
        public Vector2 Position { get => _position; set => _position = value; }

        public KnightPiece(Vector2 position) => Position = position;
    }
}

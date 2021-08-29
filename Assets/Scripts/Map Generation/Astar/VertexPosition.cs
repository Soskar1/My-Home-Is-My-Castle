using System;
using System.Collections.Generic;
using UnityEngine;

namespace Map.AI
{
    public class VertexPosition : IEquatable<VertexPosition>, IComparable<VertexPosition>
    {
        public static List<Vector2Int> possibleNeighbours = new List<Vector2Int>
        {
            new Vector2Int(0, -1),
            new Vector2Int(0, 1),
            new Vector2Int(1, 0),
            new Vector2Int(-1, 0),
        };

        public float totalCost;
        public float estimatedCost;
        public VertexPosition previousVertex = null;
        private Vector2 _position;
        private bool _isTaken;

        public int X { get => (int)_position.x; }
        public int Y { get => (int)_position.y; }
        public Vector2 Position { get => _position;}
        public bool IsTaken { get => _isTaken; }

        public VertexPosition(Vector2 position, bool isTaken = false)
        {
            _position = position;
            _isTaken = isTaken;
            estimatedCost = 0;
            totalCost = 1;
        }

        public int GetHashCode(VertexPosition obj)
        {
            return obj.GetHashCode();
        }

        public override int GetHashCode()
        {
            return _position.GetHashCode();
        }

        public int CompareTo(VertexPosition other)
        {
            if (estimatedCost < other.estimatedCost)
                return -1;
            if (estimatedCost > other.estimatedCost)
                return 1;

            return 0;
        }

        public bool Equals(VertexPosition other)
        {
            return Position == other.Position;
        }
    }
}

using Map.Grid;
using System.Collections.Generic;
using UnityEngine;

namespace Map.AI
{
    public static class Astar
    {
        public static List<Vector2> GetPath(Vector2 start, Vector2 exit, bool[] obstaclesArr, MapGrid grid)
        {
            VertexPosition startVertex = new VertexPosition(start);
            VertexPosition exitVertex = new VertexPosition(exit);

            List<Vector2> path = new List<Vector2>();

            List<VertexPosition> openedList = new List<VertexPosition>();
            HashSet<VertexPosition> closedList = new HashSet<VertexPosition>();

            startVertex.estimatedCost = ManhattanDistance(startVertex, exitVertex);

            openedList.Add(startVertex);

            VertexPosition currentVertex = null;

            while(openedList.Count > 0)
            {
                openedList.Sort();
                currentVertex = openedList[0];

                if (currentVertex.Equals(exitVertex))
                {
                    while(currentVertex != startVertex)
                    {
                        path.Add(currentVertex.Position);
                        currentVertex = currentVertex.previousVertex;
                    }

                    path.Reverse();
                    break;
                }

                var neighbours = FindNeighboursFor(currentVertex, grid, obstaclesArr);
                foreach (var neighbour in neighbours)
                {
                    if (neighbour == null || closedList.Contains(neighbour))
                        continue;
                    
                    if (!neighbour.IsTaken)
                    {
                        var totalCost = currentVertex.totalCost + 1;
                        var neighbourEstimatedCost = ManhattanDistance(neighbour, exitVertex);
                        neighbour.totalCost = totalCost;
                        neighbour.previousVertex = currentVertex;
                        neighbour.estimatedCost = totalCost + neighbourEstimatedCost;

                        if (!openedList.Contains(neighbour))
                            openedList.Add(neighbour);
                    }
                }
                closedList.Add(currentVertex);
                openedList.Remove(currentVertex);
            }

            return path;
        }

        private static VertexPosition[] FindNeighboursFor(VertexPosition currentVertex, MapGrid grid, bool[] obstaclesArr)
        {
            VertexPosition[] neighbours = new VertexPosition[4];
            int arrayIndex = 0;

            foreach (var possibleNeihgbour in VertexPosition.possibleNeighbours)
            {
                Vector2 position = new Vector2(currentVertex.X + possibleNeihgbour.x, currentVertex.Y + possibleNeihgbour.y);
                if (grid.IsCellValid(position))
                {
                    int index = grid.CalculateIndexFromCoordinates(position);
                    neighbours[arrayIndex] = new VertexPosition(position, obstaclesArr[index]);
                    arrayIndex++;
                }
            }

            return neighbours;
        }

        private static float ManhattanDistance(VertexPosition startVertex, VertexPosition exitVertex)
        {
            return Mathf.Abs(startVertex.X - exitVertex.X) + Mathf.Abs(startVertex.Y - exitVertex.Y);
        }
    }
}

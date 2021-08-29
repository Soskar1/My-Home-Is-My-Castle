using UnityEngine;
using Map.Grid;

namespace Map.Generation
{
    public static class Map
    {
        public static void RandomlyChooseAndSetStartAndExit(MapGrid grid, ref Vector2 startPos, ref Vector2 exitPos, bool randomPlacement, Direction startPositionEdge = Direction.Left, Direction exitPostionEdge = Direction.Right)
        {
            if (randomPlacement)
            {
                startPos = RandomlyChoosePositionOnTheEdgeOfTheGrid(grid, startPos, Direction.None, randomPlacement);
                exitPos = RandomlyChoosePositionOnTheEdgeOfTheGrid(grid, exitPos, Direction.None);
            }
            else
            {
                startPos = RandomlyChoosePositionOnTheEdgeOfTheGrid(grid, startPos, startPositionEdge);
                exitPos = RandomlyChoosePositionOnTheEdgeOfTheGrid(grid, exitPos, exitPostionEdge);
            }

            grid.SetCell(startPos, CellObjectType.Start);
            grid.SetCell(exitPos, CellObjectType.Exit);
        }

        private static Vector2 RandomlyChoosePositionOnTheEdgeOfTheGrid(MapGrid grid, Vector2 position, Direction direction, bool randomPlacement = false)
        {
            if (randomPlacement)
                direction = (Direction)Random.Range(1, 5);

            Vector2 directionPosition = Vector2.zero;

            switch (direction)
            {
                case Direction.None: //Ставим по центру
                {
                    directionPosition = new Vector2(grid.Width / 2, grid.Length/2);
                    break;
                }
                case Direction.Right:
                    do
                    {
                        directionPosition = new Vector2(grid.Width - 1, Random.Range(0, grid.Length));
                    } while (Vector2.Distance(directionPosition, position) <= 1);
                    break;
                case Direction.Left:
                    do
                    {
                        directionPosition = new Vector2(0, Random.Range(0, grid.Length));
                    } while (Vector2.Distance(directionPosition, position) <= 1);
                    break;
                case Direction.Up:
                    do
                    {
                        directionPosition = new Vector2(Random.Range(0, grid.Length), grid.Length - 1);
                    } while (Vector2.Distance(directionPosition, position) <= 1);
                    break;
                case Direction.Down:
                    do
                    {
                        directionPosition = new Vector2(Random.Range(0, grid.Length), 0);
                    } while (Vector2.Distance(directionPosition, position) <= 1);
                    break;
                default:
                    break;
            }

            return directionPosition;
        }
    }
}

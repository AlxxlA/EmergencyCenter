using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmergencyCenter.Units.Maps
{
    public static class MapUtils
    {
        private static readonly int[] RowNum = { -1, 0, 0, 1 };
        private static readonly int[] ColNum = { 0, -1, 1, 0 };

        private struct QueueNode
        {
            internal QueueNode(Position position, int distance, Stack<Position> path) : this()
            {
                this.Position = position;
                this.Distance = distance;
                this.Path = path;
            }

            internal Position Position { get; set; } // The cordinates of a cell
            internal int Distance { get; set; }// cell's distance of from the source
            internal Stack<Position> Path { get; set; } // current path
        };

        public static Route FindShortestRoute(Map map, Position start, Position destination)
        {
            var route = new Route();

            if (map[start] != 0)
            {
                throw new ArgumentException("Invalid start position.");
            }
            if (map[destination] != 0)
            {
                throw new ArgumentException("Invalid destination position.");
            }

            if (start == destination)
            {
                route.AddPosition(destination);
                return route;
            }

            bool[,] visited = new bool[map.Rows, map.Cols];

            visited[start.X, start.Y] = true;

            var path = new Stack<Position>();

            path.Push(start);

            var queue = new Queue<QueueNode>();

            var node = new QueueNode(start, 0, path);


            queue.Enqueue(node);

            while (queue.Count != 0)
            {
                var currentNode = queue.Dequeue();
                var currentPosition = currentNode.Position;
                path = currentNode.Path;

                if (currentPosition == destination)
                {
                    route.Positions = path.Reverse().ToList();
                    break;
                }

                for (int i = 0; i < 4; i++)
                {
                    int row = currentPosition.X + RowNum[i];
                    int col = currentPosition.Y + ColNum[i];


                    if (IsValidPosition(map, row, col) && map[row, col] == 0 && !visited[row, col])
                    {
                        path.Push(new Position(row, col));
                        visited[row, col] = true;
                        var nextPosition = new Position(row, col);
                        var nextNode = new QueueNode(nextPosition, currentNode.Distance + 1, new Stack<Position>(path.Reverse()));
                        queue.Enqueue(nextNode);
                        path.Pop();
                    }

                }
            }
            if (route.Positions.Count == 0)
            {
                throw new ArgumentException("Route not found. Position is unreachable.");
            }
            return route;
        }

        private static bool IsValidPosition(Map map, int row, int col)
        {
            if (row < 0 || row >= map.Rows)
            {
                return false;
            }
            if (col < 0 || col >= map.Cols)
            {
                return false;
            }

            return true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using EmergencyCenter.Units.Contracts.Navigation;
using EmergencyCenter.Units.Navigation.Enums;

namespace EmergencyCenter.Units.Navigation
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

            internal Position Position { get; } // The cordinates of a cell
            internal int Distance { get; }// cell's distance of from the source
            internal Stack<Position> Path { get; } // current path
        }

        public static IRoute FindShortestRoute(IMap map, Position start, Position destination)
        {
            var route = new Route();

            if (map[start] != (int)MapTileType.Street)
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

            var visited = new HashSet<Position>();

            visited.Add(start);

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
                    route = new Route(path.Reverse());
                    break;
                }

                foreach (var position in map.PositionNeighbours(currentPosition))
                {
                    if (map[position] == (int)MapTileType.Street && !visited.Contains(position))
                    {
                        path.Push(position);
                        visited.Add(position);
                        var nextNode = new QueueNode(position, currentNode.Distance + 1, new Stack<Position>(path.Reverse()));
                        queue.Enqueue(nextNode);
                        path.Pop();
                    }
                }
            }
            if (route.Length == 0)
            {
                throw new ArgumentException("Route not found. Position is unreachable.");
            }

            return route;
        }
    }
}

using System;
using System.Text;

namespace Day3
{
    class Program
    {
        // DEBUG ONLY
        const bool debug = true;

        const int START_X = 20000;
        const int START_Y = 20000;

        private struct Position
        {
            public int x, y;
            public Position(int p1, int p2)
            {
                x = p1;
                y = p2;
            }

            public Position(Position p)
            {
                x = p.x;
                y = p.y;
            }
        }

        static void Main(string[] args)
        {
            // Parse input into path directions for wire 1 & 2.
            var input = System.IO.File.ReadAllLines(@"./input.txt");
            string[] path1 = input[0].Split(",");
            string[] path2 = input[1].Split(",");

            // Initialize grid state and wire start positions.
            var grid = new int[START_X*2, START_Y*2];
            var position1 = new Position(START_X, START_Y);
            var position2 = new Position(START_X, START_Y);

            // Execute each move for both wires.
            foreach (var move in path1)
                MarkMove(ref grid, ref position1, move, 1);
            foreach (var move in path2)
                MarkMove(ref grid, ref position2, move, 2);

            // Find the closest intersections Manhatten Distance
            var distance = FindClosestIntersection(grid, new Position(START_X, START_Y));
            if (distance != int.MaxValue)
            {
                Console.WriteLine($"Manhatten distance of closest intersection: {distance}");
            } else {
                Console.WriteLine($"No intersections found!");
            }

            // if (debug) OutputGrid(grid);
        }

        static void MarkPosition(ref int[,] grid, ref Position pos, int wire)
        {
            if (grid[pos.x, pos.y] == 0)
            {
                grid[pos.x, pos.y] = wire;
            }
            else if (grid[pos.x, pos.y] != wire)
            {
                grid[pos.x, pos.y] = 3;
            }
        }

        static void MarkMove(ref int[,] grid, ref Position p, string move, int wire)
        {
            var direction = move[0];
            var length = int.Parse(move.Substring(1, move.Length-1));
            var newPos = new Position(p);
            if (direction == 'U')
            {
                for (int i = 0; i < length; i++)
                {
                    newPos.y--;
                    MarkPosition(ref grid, ref newPos, wire);
                }
            }
            else if (direction == 'D')
            {
                for (int i = 0; i < length; i++)
                {
                    newPos.y++;
                    MarkPosition(ref grid, ref newPos, wire);
                }
            }
            else if (direction == 'L')
            {
                for (int i = 0; i < length; i++)
                {
                    newPos.x--;
                    MarkPosition(ref grid, ref newPos, wire);
                }
            }
            else if (direction == 'R')
            {
                for (int i = 0; i < length; i++)
                {
                    newPos.x++;
                    MarkPosition(ref grid, ref newPos, wire);
                }
            }
            // Console.WriteLine($"P:({p.x},{p.y}), Move:{move}, newPos:({newPos.x},{newPos.y})");
            p.x = newPos.x;
            p.y = newPos.y;
            // if (debug) Console.WriteLine($"({newPos.x},{newPos.y}) = {grid[newPos.x, newPos.y]}");
        }

        private static int FindClosestIntersection(int[,] grid, Position p)
        {
            int distance = int.MaxValue;
            //Console.WriteLine($"{grid.GetLength(0)} x {grid.GetLength(1)}");
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    // Skip the origin point (default intersection).
                    if (y == START_Y && x == START_X) continue;
                    if (grid[x, y] == 3)
                    {
                        // if (debug) Console.WriteLine($"checking pos: ({x},{y}) = {grid[x,y]}");
                        int newDistance = Math.Abs(x - START_X) + Math.Abs(y - START_Y);
                        distance = (newDistance < distance) ? newDistance : distance;
                        // Console.WriteLine($"intersection distance: {Math.Abs(x - START_X)} + {Math.Abs(y - START_Y)} = {newDistance}");
                        // Console.WriteLine($"Found intersection at: ({x},{y})={grid[x,y]}, distance={distance}");
                    }
                }
            }
            return distance;
        }

        private static void OutputGrid(int[,] grid)
        {
            var sb = new StringBuilder(string.Empty);
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    if (y == START_Y && x == START_X)
                    {
                        sb.Append("O");
                    }
                    else
                    {
                        sb.Append(grid[x,y]);
                    }
                }
                sb.Append('\n');
            }
            System.IO.File.WriteAllText(@"./output.txt", sb.ToString());
        }
    }
}

using System;
using System.Linq;
using System.Collections.Generic;

namespace Day1
{
    class Program
    {
        /// <summary>
        /// Calculates the fuel requirements for a given set of module weights
        /// contained in the input filepath.
        /// </summary>
        /// <param name="args">
        /// Unused console args.
        /// </param>
        static void Main(string[] args)
        {
            var lines = System.IO.File.ReadAllLines(@"./input.txt");
            var weights = lines.Select(int.Parse).ToList();
            int total = 0;
            weights.ForEach(w => total += calcFuelRequirement(w));

            Console.WriteLine(total);
        }

        /// <summary>
        /// Recursively calculates the fuel requirements for the given weight.
        /// </summary>
        /// <param name="weight">Weight to calculate fuel for.</param>
        /// <returns>Integer weight of fuel requirements.</returns>
        static int calcFuelRequirement(int weight)
        {
            int fuel = weight / 3 - 2;
            
            if (fuel <= 0)
                return 0;

            return fuel + calcFuelRequirement(fuel);
        }
    }
}

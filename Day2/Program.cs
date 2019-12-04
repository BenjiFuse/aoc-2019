using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Day2
{
    class Program
    {
        const int DESIRED_RESULT = 19690720;
        static int[] ProgramSequence;

        static void Main(string[] args)
        {
            // Test all combinations of noun and verb, each less than 99.
            for (int noun = 0; noun < 100; noun++) {
                for (int verb = 0; verb < 100; verb++) {
                    var prog = GetProgram();
                    prog[1] = noun;
                    prog[2] = verb;
                    int result = Execute(prog, 0)[0];
                    if (result == DESIRED_RESULT) {
                        Console.WriteLine($"Noun: {noun}, Verb: {verb}, Result: {result}");
                        Console.WriteLine($"100 * noun + verb: {100*noun+verb}");
                        return;
                    }
                }
            }
            return;
        }

        /// <summary>
        /// Grab a fresh(tm) version of the program sequence from the input file.
        /// </summary>
        /// <returns>List<int> representing the program sequence.</returns>
        static List<int> GetProgram()
        {
            if (ProgramSequence is null) {
                var input = System.IO.File.ReadAllText(@"./input.txt");
                ProgramSequence = input.Split(",").Select(int.Parse).ToArray();
            }
            return ProgramSequence.ToList();
        }

        /// <summary>
        /// Perform the execute loop on a given program, starting execution at
        /// the provider pointer location.
        /// </summary>
        /// <param name="prog">Program sequence</param>
        /// <param name="pointer">Execution pointer</param>
        /// <returns>Finished program sequence state</returns>
        static List<int> Execute(List<int> prog, int pointer)
        {
            int ins = prog[pointer];
            int dest;
            switch (ins) {
                case 1:
                    // Add
                    var sum = prog[prog[pointer+1]] + prog[prog[pointer+2]];
                    dest = prog[pointer+3];
                    prog[dest] = sum;
                    break;
                case 2:
                    // Multiply
                    var product = prog[prog[pointer+1]] * prog[prog[pointer+2]];
                    dest = prog[pointer+3];
                    prog[dest] = product;
                    break;
                case 99:
                    // Terminate
                    return prog;
                default:
                    throw new InvalidOpcodeException($"Code {ins} at Position {pointer}");
            }
            Execute(prog, pointer+4);
            return prog;
        }

        /// <summary>
        /// Helper function to print a given program sequence.
        /// </summary>
        /// <param name="prog">Program sequence</param>
        static void PrintProg(List<int> prog)
        {
            Console.WriteLine(String.Join(",", prog));
        }
    }

    internal class InvalidOpcodeException : Exception
    {
        public InvalidOpcodeException(string message)
            : base(message) {}
    }
}

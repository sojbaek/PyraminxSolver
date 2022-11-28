using System;
using System.Collections.Generic;

namespace PyraminxSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            Pyraminx pyra = new Pyraminx("DDDDDDDDDFFFFFFFFFLLLLLLLLLRRRRRRRRR");

            Pyraminx testpyra = new Pyraminx("DLDFRFRRFFDRFLLLFRRLLRDLDDLDRDDFFFRL");

            Pyraminx shuffled = testpyra;
            Console.WriteLine(value: "shuffled cube=" + shuffled.to_fc_String());

            string info;
            List<Rotate> sol = Search.Solution(shuffled.to_fc_String(), out info);
            Pyraminx scube = Search.MultiRotPyra(shuffled, sol);
            Console.WriteLine(value: "solved cube=" + scube.to_fc_String());
            Console.WriteLine("Solution=" + info);
            Console.WriteLine("Solution=" + Search.SolutionToString(sol));
            Console.ReadKey();


        }
    }
}

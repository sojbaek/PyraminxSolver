using System;
using System.Collections.Generic;

namespace PyraminxSolver
{
    public class Search
    {

        public static readonly int[][] transform =
            new int[][]{ new int[]  {27,28,29,30,31,5,6,7,8,13,12,16,15,17,11,10,14,9,18,19,20,1,0,23,3,2,4,22,21,25,24,26,32,33,34,35} //F
            , new int[]  {22,21,25,24,26,5,6,7,8,17,15,14,10,9,16,12,11,13,18,19,20,28,27,23,30,29,31,0,1,2,3,4,32,33,34,35} //F'
            , new int[]  {0,1,2,15,17,5,10,14,9,27,28,11,12,13,32,33,16,35,22,21,25,24,26,20,19,23,18,8,6,29,30,31,7,3,34,4} //L
            , new int[]  {0,1,2,33,35,5,28,32,27,8,6,11,12,13,7,3,16,4,26,24,23,19,18,25,21,20,22,9,10,29,30,31,14,15,34,17} //L'
            , new int[]  {18,19,2,3,4,23,24,7,26,9,10,11,6,8,14,1,5,0,17,15,20,21,22,16,12,25,13,31,30,34,33,35,29,28,32,27} //R
            , new int[]  {17,15,2,3,4,16,12,7,13,9,10,11,24,26,14,19,23,18,0,1,20,21,22,5,6,25,8,35,33,32,28,27,34,30,29,31} //R'
            , new int[]  {4,3,7,6,8,2,1,5,0,18,19,20,21,22,14,15,16,17,31,30,34,33,35,23,24,25,26,27,28,29,10,9,32,12,11,13} //D
            , new int[]  {8,6,5,1,0,7,3,2,4,31,30,34,33,35,14,15,16,17,9,10,11,12,13,23,24,25,26,27,28,29,19,18,32,21,20,22} //D'
            , new int[]  {0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,27,18,19,20,21,22,23,24,25,17,26,28,29,30,31,32,33,34,35} //u
            , new int[]  {0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,26,18,19,20,21,22,23,24,25,27,17,28,29,30,31,32,33,34,35} //u'
            , new int[]  {0,1,2,3,4,5,6,7,18,9,10,11,12,13,14,15,16,17,35,19,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34,8} //b
            , new int[]  {0,1,2,3,4,5,6,7,35,9,10,11,12,13,14,15,16,17,8,19,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34,18} //b'
            , new int[]  {0,1,2,3,9,5,6,7,8,22,10,11,12,13,14,15,16,17,18,19,20,21,4,23,24,25,26,27,28,29,30,31,32,33,34,35} //l
            , new int[]  {0,1,2,3,22,5,6,7,8,4,10,11,12,13,14,15,16,17,18,19,20,21,9,23,24,25,26,27,28,29,30,31,32,33,34,35} //l'
            , new int[]  {31,1,2,3,4,5,6,7,8,9,10,11,12,0,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,13,32,33,34,35} //r
            , new int[]  {13,1,2,3,4,5,6,7,8,9,10,11,12,31,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,0,32,33,34,35} //r'
            };
                
        
        static Pyraminx Goal = new Pyraminx("DDDDDDDDDFFFFFFFFFLLLLLLLLLRRRRRRRRR");
               
        static Pyraminx MatchSmallTip(Pyraminx pyra,  List<Rotate> sol)
        {
            Pyraminx outPyra = pyra;
            Facelet[] tips = new Facelet[] { Facelet.L1, Facelet.F1, Facelet.D1, Facelet.R1 };  // b,l,r,u
            Facelet[] centers = new Facelet[] { Facelet.L2, Facelet.F2, Facelet.D2, Facelet.R2 };  //list("b"=19, "r"=1,"u"=28,"l"=10);
            Rotate[] rots = new Rotate[] { Rotate.b, Rotate.l, Rotate.r, Rotate.u };
            Rotate[] invrots = new Rotate[] { Rotate.bprime, Rotate.lprime, Rotate.rprime, Rotate.uprime };

            for (int ii=0; ii<4; ii++)
            {
                Rotate rot = rots[ii];
                Rotate invrot = invrots[ii];
                int tip = (int) tips[ii];
                int tipturned = transform[(int) rot][tip];
                int invtipturned = transform[(int)invrot][tip];
                if (pyra.f[tipturned] == pyra.f[(int) centers[ii]])
                {
                    outPyra = RotatePyra(outPyra, rot);
                    sol.Add(rot);
                } else if (pyra.f[invtipturned] == pyra.f[(int) centers[ii]])
                {
                    outPyra = RotatePyra(outPyra, invrot);
                    sol.Add(invrot);
                }
            }
            return outPyra;
        }
       
        public static void BFS(Pyraminx start, Pyraminx end, List<Rotate> sol)
        {
            List<Pyraminx> pyras = new List<Pyraminx>();
            List<int> parentArray = new List<int>();
            List<Rotate> rotHistory = new List<Rotate>();
            HashSet<Pyraminx> visited = new HashSet<Pyraminx>();
            // Create a queue for BFS
            int queueindex;
            // Stack<Rotate> rotstack = new Stack<Rotate>();

            Rotate[] rots = new Rotate[] {
                Rotate.R, Rotate.Rprime,Rotate.F, Rotate.Fprime, Rotate.D, Rotate.Dprime,
                Rotate.L, Rotate.Lprime
            };

            pyras.Add(start);
            parentArray.Add(0);
            rotHistory.Add(Rotate.R);  // 0
            visited.Add(start);
            queueindex = 0;
            int numRejected = 0;
            int total = 0;
            Console.WriteLine(queueindex + ":" + pyras[queueindex].to_fc_String());
            while (queueindex < pyras.Count)
            {
                Pyraminx s = pyras[queueindex];
                foreach (Rotate rot in rots)
                {
                    Pyraminx newPyra = RotatePyra(s, rot);
                    total++;
                    if (!visited.Contains(newPyra))
                    {
                        pyras.Add(newPyra);
                        parentArray.Add(queueindex);
                        rotHistory.Add(rot);
                        visited.Add(newPyra);
                    }
                    else
                    {
                        numRejected++;
                    }
                    if (newPyra.Equals(end))
                    {
                        Console.WriteLine("Problem solved!");
                        int parent = pyras.Count - 1;
                        while (parent > 0)
                        {
                            Console.WriteLine(parent + ":" + pyras[parent].to_fc_String());
                            Console.WriteLine("<---" + rotHistory[parent].ToString() + "---");
                            sol.Insert(0, rotHistory[parent]);
                            parent = parentArray[parent];
                        }
                        Console.WriteLine(parent + ":" + pyras[parent].to_fc_String());
                        Console.WriteLine("Total Trial=" + total + " numRejected=" + numRejected);
                        return;
                    }
                }
                queueindex++;
            }

        }


        public static Pyraminx RotatePyra(Pyraminx pyra, Rotate rot)
        {
            FaceColor[] outPyra = new FaceColor[36];

            if (rot == Rotate.None)
            {
                return new Pyraminx(pyra.f);
            }

            int[] tr = transform[(int)rot];
            for (int ii = 0; ii < 36; ii++)
            {
                outPyra[ii] = pyra.f[tr[ii]];
            }
            return new Pyraminx(outPyra);
        }

        public static Pyraminx MultiRotPyra(Pyraminx pyra, List<Rotate> rots)
        {
            Pyraminx ipyra = new Pyraminx();
            Pyraminx outPyra = new Pyraminx();
            Array.Copy(pyra.f, ipyra.f, 36);
            foreach (Rotate rot in rots)
            {
                outPyra = RotatePyra(ipyra, rot);
                ipyra.f = outPyra.f;
            }
            return outPyra;
        }

        public static string SolutionToString(List<Rotate> rots)
        {
            List<string> sol = new List<string>();
            foreach (Rotate rot in rots)
            {
                switch (rot)
                {
                    case Rotate.Rprime:
                        sol.Add("R'"); break;
                    case Rotate.Fprime:
                        sol.Add("F'"); break;
                    case Rotate.Dprime:
                        sol.Add("D'"); break;
                    case Rotate.Lprime:
                        sol.Add("L'"); break;
                    case Rotate.bprime:
                        sol.Add("b'"); break;
                    case Rotate.uprime:
                        sol.Add("u'"); break;
                    case Rotate.rprime:
                        sol.Add("r'"); break;
                    case Rotate.lprime:
                        sol.Add("l'"); break;
                    case Rotate.None:
                        break;
                    default:
                        sol.Add(rot.ToString());
                        break;
                }
            }
            return string.Join(" ", sol);
        }

        public static List<Rotate> Solution(string moveString, out string info)
        {
            List<Rotate> sol = new List<Rotate>();
            List<Rotate> solBFS = new List<Rotate>();
            Pyraminx ipyra = new Pyraminx(moveString);
            Pyraminx goal = new Pyraminx("DDDDDDDDDFFFFFFFFFLLLLLLLLLRRRRRRRRR");
            Pyraminx pyra1 = MatchSmallTip(ipyra, sol);
            BFS(pyra1, goal, solBFS);
            sol.AddRange(solBFS);
            Pyraminx spyra = MultiRotPyra(ipyra, sol);
            info = SolutionToString(sol);
            return sol;
        }
    }

}



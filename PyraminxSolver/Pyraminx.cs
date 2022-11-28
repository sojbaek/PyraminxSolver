using System;
using System.Collections;

//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
//Cube on the facelet level

namespace PyraminxSolver
{
    public class Pyraminx
    {
        public FaceColor[] f = new FaceColor[36];

        
        // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public Pyraminx()
        {
            string s = "DDDDDDDDDFFFFFFFFFLLLLLLLLLRRRRRRRRR";
            for (int i = 0; i < 36; i++)
            {
                FaceColor col = (FaceColor)Enum.Parse(typeof(FaceColor), s[i].ToString());
                f[i] = col;
            }
        }

        // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        // Construct a facelet cube from a string
        public Pyraminx(string cubeString)
        {
            for (int i = 0; i < cubeString.Length; i++)
            {
                FaceColor col = (FaceColor)Enum.Parse(typeof(FaceColor), cubeString[i].ToString());
                f[i] = col;
            }

        }

        public Pyraminx(FaceColor[] f)
        {
            this.f = f;
        }
        // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        // Gives string representation of a facelet cube
        public string to_fc_String()
        {
            string s = "";
            for (int i = 0; i < 36; i++)
                s += f[i].ToString();
            return s;
        }

        public override int GetHashCode()
        {
            int result = 0;
            int shift = 0;
            for (int i = 0; i < f.Length; i++)
            {
                shift = (shift + 11) % 21;
                result ^= ((int) f[i] + 1024) << shift;
            }
            return result;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Pyraminx);
        }

        public bool Equals(Pyraminx other)
        {
            for (int i = 0; i <36; i++)
            {
                if (other.f[i] == FaceColor.W || this.f[i] == FaceColor.W) continue;
                if (other.f[i] != this.f[i]) return false;
            }
            return true;
        }
    }
}
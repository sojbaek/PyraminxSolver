using System;

namespace PyraminxSolver
{
    /**
      * <pre>
      * The names of the facelet positions of the cube
            L1   L6   L9    R1   R6   R9
               L2   L7   F9   R2   R7  
               L3   L8   F7   R3   R8
                  L4   F6  F8   R4 
                  L5   F2  F4   R5
                     F1  F3  F5
                     D5  D3  D1
                       D4  D2
                       D8  D6
                         D7
                         D9

      * </pre>
      * 
      * A pyraminx definition string "DFLR..." means for example: In position D1 we have the D-color, in position F2 we have the
      * F-color, in position L3 we have the L color etc. according to the order D1, D2, D3, D4, D5, D6, D7, D8, D9,F1, F2, F3, F4, F5, F6, F7, F8, F9, 
      * L1, L2, L3, L4, L5, L6, L7, L8, L9, R1, R2, R3, R4, R5, R6, R7, R8, R9 of the enum constants.
      */
    public enum Facelet
    {
        D1, D2, D3, D4, D5, D6, D7, D8, D9, // 0-8
        F1, F2, F3, F4, F5, F6, F7, F8, F9, // 9-17   19 10 12 15  
        L1, L2, L3, L4, L5, L6, L7, L8, L9, // 18-26 
        R1, R2, R3, R4, R5, R6, R7, R8, R9  // 27-35
    }

    

    //++++++++++++++++++++++++++++++ Names the colors of the cube facelets ++++++++++++++++++++++++++++++++++++++++++++++++
    public enum FaceColor
    {
        D,F,L,R,W
    }

    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    //The names of the corner positions of the cube. Corner URF e.g., has an U(p), a R(ight) and a F(ront) facelet

    public enum BigRotate
    {
        F, Fprime, L, Lprime, R, Rprime, D, Dprime
    }

    public enum TipRotate
    {
        u, uprime, b, bprime, l, lprime,r, rprime
    }

    public enum Rotate
    {
        F, Fprime, L, Lprime, R, Rprime, D, Dprime, u, uprime, b, bprime, l, lprime, r, rprime, None
    }
}
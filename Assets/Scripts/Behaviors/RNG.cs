using System;
using System.Linq;
using System.Collections.Generic;

public static class RNG {
    public static int seed {get; private set;} = Environment.TickCount;
    public static System.Random rand {get; private set;} = new System.Random(RNG.seed);

    public static int Next(){
        return rand.Next();
    }

    public static int Next(int max){
        return rand.Next(max);
    }

    public static int Next(int min, int max){
        return rand.Next(min, max);
    }
}
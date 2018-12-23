using UnityEngine;
using System.Collections.Generic;

public class PseudoRandomNumberGenerator
{
    private System.Random randomiser;

    public PseudoRandomNumberGenerator(int seed)
    {
        randomiser = new System.Random(seed);
    }

    public int GetNextNumber(int min,int max)
    {
        return randomiser.Next(min, max);
    }
}

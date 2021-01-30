using UnityEngine;

public enum Compass { N, S, E, W }

public static class CompassExtensions
{
    public static Compass Opposite(this Compass dir)
    {
        switch(dir)
        {
            case Compass.N: return Compass.S;
            case Compass.S: return Compass.N;
            case Compass.E: return Compass.W;
            case Compass.W: return Compass.E;
            default:
                Debug.LogWarning("Invalid direction! Returning north...");
                return Compass.N;
        }
    }
}
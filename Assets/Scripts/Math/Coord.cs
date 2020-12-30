using UnityEngine;

public class Coord
{
    public float X { get; set; }
    public float Y { get; set; }
    public float Z { get; set; }

    public Coord(float x, float y, float z)
    {
        X = x;
        Y = y;
        Z = z;
    }


    public Vector3 GetVector3()
    {
        return new Vector3(X, Y, Z);
    }



}

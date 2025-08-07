using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// A serializable representation of a spline knot, since Unity's own
/// spline classes may not be serializable by default.
/// </summary>
[System.Serializable]
public struct SerializableKnot
{
    public Vector3 position;
    public Vector3 tangentIn;
    public Vector3 tangentOut;
}

[System.Serializable]
public class SplineData
{
    public List<SerializableKnot> knots = new List<SerializableKnot>();
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePart
{

    public string Name { get; private set; }
    public Vector3 Target { get; private set; }
    public MoveAs DoMovePartAs { get; private set; }
    public bool IsBackingUp { get; private set; }
    public bool IsPositionChange { get; private set; }
    public bool IsChangeRotationInPlace { get; private set; }
    

    public MovePart(string name, Vector3 target, MoveAs doMovePartAs,bool isBackingUp, bool isPositionChange, bool isChangeRotationInPlace)
    {
        Name = name;
        Target = target;
        DoMovePartAs = doMovePartAs;
        IsBackingUp = isBackingUp;
        IsPositionChange = isPositionChange;
        IsChangeRotationInPlace = isChangeRotationInPlace;
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HandPosition
{
    None,
    PalmUp,
    PalmDown,
    Wave,
    ForearmGrip,
    Handshake,
    UpPalmUp,
    UpPalmDown,
    ArmAround
}

public class MovePart
{

    public string Name { get; private set; }
    public Vector3 Target { get; private set; }
    public MoveAs DoMovePartAs { get; private set; }
    public bool IsBackingUp { get; private set; }
    public bool IsPositionChange { get; private set; }
    public bool IsChangeRotationInPlace { get; private set; }
    public HandPosition LeftHandPosition { get; private set; }
    public HandPosition RightHandPosition { get; private set; }
    public Vector3 LeftHandTarget { get; private set; }
    public Vector3 RightHandTarget { get; private set; }


    public MovePart(string name, Vector3 target, MoveAs doMovePartAs,bool isBackingUp, bool isPositionChange, bool isChangeRotationInPlace)
    {
        Name = name;
        Target = target;
        DoMovePartAs = doMovePartAs;
        IsBackingUp = isBackingUp;
        IsPositionChange = isPositionChange;
        IsChangeRotationInPlace = isChangeRotationInPlace;
    }

    public MovePart(string name, Vector3 target, MoveAs doMovePartAs, bool isBackingUp, bool isPositionChange, bool isChangeRotationInPlace, HandPosition leftHandPosition, Vector3 leftHandTarget)
    {
        Name = name;
        Target = target;
        DoMovePartAs = doMovePartAs;
        IsBackingUp = isBackingUp;
        IsPositionChange = isPositionChange;
        IsChangeRotationInPlace = isChangeRotationInPlace;
        LeftHandPosition = leftHandPosition;
        LeftHandTarget = leftHandTarget;
    }
}

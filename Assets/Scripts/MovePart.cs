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

public enum CompleteConditionType
{
    SeePartner,
    TargetMet
}

public class MovePart
{

    public string Name { get; private set; }
    public Vector3 Target { get; private set; }
    public GameObject TargetGameObject { get; private set; }
    public MoveAs DoMovePartAs { get; private set; }
    public bool IsBackingUp { get; private set; }
    public bool IsPositionChange { get; private set; }
    public bool IsChangeRotationInPlace { get; private set; }
    public HandPosition LeftHandPosition { get; private set; }
    public HandPosition RightHandPosition { get; private set; }
    public Vector3 LeftHandTarget { get; private set; }
    public Vector3 RightHandTarget { get; private set; }
    public bool DoUpdateTargetPosition { get; private set; }

    public CompleteConditionType CompleteCondition { get; private set; }

    public MovePart(string name, Vector3 target, MoveAs doMovePartAs,bool isBackingUp, bool isPositionChange, bool isChangeRotationInPlace)
    {
        Name = name;
        Target = target;
        DoMovePartAs = doMovePartAs;
        IsBackingUp = isBackingUp;
        IsPositionChange = isPositionChange;
        IsChangeRotationInPlace = isChangeRotationInPlace;
        LeftHandPosition = HandPosition.None;
        TargetGameObject = null;
        DoUpdateTargetPosition = false;
    }

    public MovePart(string name, Vector3 target, MoveAs doMovePartAs, bool isBackingUp, bool isPositionChange, bool isChangeRotationInPlace, HandPosition leftHandPosition, Vector3 leftHandTarget, GameObject targetGameObject, bool doUpdateTargetPosition, CompleteConditionType completeCondition)
    {
        Name = name;
        Target = target;
        DoMovePartAs = doMovePartAs;
        IsBackingUp = isBackingUp;
        IsPositionChange = isPositionChange;
        IsChangeRotationInPlace = isChangeRotationInPlace;
        LeftHandPosition = leftHandPosition;
        LeftHandTarget = leftHandTarget;
        TargetGameObject = targetGameObject;
        DoUpdateTargetPosition = doUpdateTargetPosition;
        CompleteCondition = CompleteCondition;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doozy.Engine;

public class MovePartsPopper : MonoBehaviour
{
    private List<MovePart> MyMovePartsQueue = new List<MovePart>();
    private int nextPart = -1;
    private int currentPart = -1;
    private bool isMoving;
    private SimpleSampleCharacterControl MyMover;

    public string lastCall;

// Start is called before the first frame update
    void Start()
    {
        MyMover = gameObject.GetComponent<SimpleSampleCharacterControl>();

    }

    // Update is called once per frame
    void Update()
    {
        int LastPartIndex = MyMovePartsQueue.Count - 1;
        //if (gameObject.name == "Dancer1Right")
        //{
        //    Debug.Log($"lastpartindex:{LastPartIndex}");
        //}
        if (!(LastPartIndex < 0))
        {
            //if (gameObject.name == "Dancer1Right")
            //{
            //    Debug.Log($"nextpart:{nextPart} currentpart:{currentPart}");
            //}
            if (LastPartIndex > nextPart && currentPart == nextPart)
            {
                nextPart++;
            }

            isMoving = nextPart > currentPart;

            if (isMoving)                                                               
            {
                if (MyMover.IsReadyForNextMove())
                {
                    if (nextPart > currentPart)
                    {
                        currentPart++;
                        MyMover.setFacingTarget(MyMovePartsQueue.ToArray()[currentPart].Target);
                        MyMover.setTargetPosition(MyMovePartsQueue.ToArray()[currentPart].Target);
                        MyMover.isFacing = MyMovePartsQueue.ToArray()[currentPart].IsChangeRotationInPlace;
                        MyMover.isMoving = (!MyMovePartsQueue.ToArray()[currentPart].IsBackingUp && MyMovePartsQueue.ToArray()[currentPart].IsPositionChange);
                        MyMover.isMovingBackwards = MyMovePartsQueue.ToArray()[currentPart].IsBackingUp;
                        MyMover.setMovingAs( MyMovePartsQueue.ToArray()[currentPart].DoMovePartAs);
                        MyMover.leftHandPosition = MyMovePartsQueue.ToArray()[currentPart].LeftHandPosition;
                        MyMover.leftHandTarget = MyMovePartsQueue.ToArray()[currentPart].LeftHandTarget;
                        MyMover.doUpdateTargetPosition = MyMovePartsQueue.ToArray()[currentPart].DoUpdateTargetPosition;
                        MyMover.targetGameObject = MyMovePartsQueue.ToArray()[currentPart].TargetGameObject;
                    }
                    else
                        isMoving = false;
                }
            }
        }
    }

    private void OnEnable()
    {
        //Start listening for game events
        Message.AddListener<GameEventMessage>(OnMessage);
    }

    private void OnDisable()
    {
        //Stop listening for game events
        Message.RemoveListener<GameEventMessage>(OnMessage);
    }

    private void OnMessage(GameEventMessage message)
    {
        if (message == null) return;
        lastCall = message.EventName;
        switch (message.EventName)
        {
            case "AllemandeLeft":
                //face corner
                MyMovePartsQueue.Add(new MovePart(message.EventName, gameObject.GetComponent<Dancer>().Corner.transform.position, MoveAs.Dancer, false, false, true));
                // Motion Pinwheel around left forearm grip
                MyMovePartsQueue.Add(new MovePart(message.EventName, gameObject.GetComponent<Dancer>().Corner.GetComponent<DancerTargets>().ForwardSpaceTarget.transform.position, MoveAs.Dancer, false, true, false,HandPosition.ForearmGrip, gameObject.GetComponent<Dancer>().Corner.GetComponent<Dancer>().LeftHandTarget.transform.position, gameObject.GetComponent<Dancer>().Corner,true));

                // IsComplete: facepartner
                // MyMovePartsQueue.Add(new MovePart(message.EventName, gameObject.GetComponent<Dancer>().Partner.transform.position, MoveAs.Dancer, false, false, true));

                break;
            case "FaceCorner":
                MyMovePartsQueue.Add(new MovePart(message.EventName,gameObject.GetComponent<Dancer>().Corner.transform.position,MoveAs.Dancer,false,false,true));
                break;
            case "FacePartner":
                MyMovePartsQueue.Add(new MovePart(message.EventName,gameObject.GetComponent<Dancer>().Partner.transform.position, MoveAs.Dancer, false, false, true));
                break;
            case "FaceLeft":
                MyMovePartsQueue.Add(new MovePart(message.EventName, gameObject.GetComponent<DancerTargets>().LeftSpaceTarget.transform.position, MoveAs.Dancer, false, false, true));
                break;
            case "FaceRight":
                MyMovePartsQueue.Add(new MovePart(message.EventName, gameObject.GetComponent<DancerTargets>().RightSpaceTarget.transform.position, MoveAs.Dancer, false, false, true));
                break;
            case "FaceIn":
                MyMovePartsQueue.Add(new MovePart(message.EventName, gameObject.GetComponent<Dancer>().FacingInTarget.transform.position, MoveAs.Couple, false, false, true));
                break;
            case "SquareTheSet":
                MyMovePartsQueue.Add(new MovePart(message.EventName, gameObject.GetComponent<Dancer>().HomePosition.transform.position, MoveAs.Couple, false, true, false));
                break;
            case "MoveForward":
                MyMovePartsQueue.Add(new MovePart(message.EventName, gameObject.GetComponent<DancerTargets>().ForwardSpaceTarget.transform.position, MoveAs.Couple, false, true, false));
                break;
            case "MoveBackward":
                MyMovePartsQueue.Add(new MovePart(message.EventName, gameObject.GetComponent<DancerTargets>().BackwardSpaceTarget.transform.position, MoveAs.Couple, true,true, false));
                break;
            default:
                Debug.Log($"No association for {message.EventName}");
                break;
        }
    }
}

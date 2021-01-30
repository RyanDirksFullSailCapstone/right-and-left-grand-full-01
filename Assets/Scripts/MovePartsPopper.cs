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
                if (gameObject.name == "Dancer1Right")
                {
                    Debug.Log($"incremented nextpart to:{nextPart}");
                }
            }

            isMoving = nextPart > currentPart;
            //if (nextPart > currentPart)
            //{
            //    if (!isMoving)
            //    {
            //        currentPart++;
            //    }
            //    isMoving = true;
            //}

            if (isMoving)                                                               
            {
                if (MyMover.IsReadyForNextMove())
                {
                    if (nextPart > currentPart)
                    {
                        currentPart++;
                        if (gameObject.name == "Dancer1Right")
                        {
                            Debug.Log($"incremented currentpart to:{currentPart}");
                        }
                        MyMover.setFacingTarget(MyMovePartsQueue.ToArray()[currentPart].Target);
                        MyMover.setTargetPosition(MyMovePartsQueue.ToArray()[currentPart].Target);
                        MyMover.isFacing = MyMovePartsQueue.ToArray()[currentPart].IsChangeRotationInPlace;
                        MyMover.isMoving = MyMovePartsQueue.ToArray()[currentPart].IsPositionChange;
                        MyMover.isMovingBackwards = MyMovePartsQueue.ToArray()[currentPart].IsBackingUp;
                        MyMover.setMovingAs( MyMovePartsQueue.ToArray()[currentPart].DoMovePartAs);
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
        if (gameObject.name == "Dancer1Right")
        {
            Debug.Log($"adding {message.EventName}");
        }
        if (message == null) return;
        switch (message.EventName)
        {
            case "FaceCorner":
                MyMovePartsQueue.Add(new MovePart(message.EventName,gameObject.GetComponent<Dancer>().Corner.transform.position,MoveAs.Dancer,false,false,true));
                break;
            case "FacePartner":
                MyMovePartsQueue.Add(new MovePart(message.EventName,gameObject.GetComponent<Dancer>().Corner.transform.position, MoveAs.Dancer, false, false, true));
                break;
            case "FaceLeft":
                MyMovePartsQueue.Add(new MovePart(message.EventName, gameObject.GetComponent<DancerTargets>().LeftSpaceTarget.transform.position, MoveAs.Dancer, false, false, true));
                break;
            case "FaceRight":
                MyMovePartsQueue.Add(new MovePart(message.EventName, gameObject.GetComponent<DancerTargets>().RightSpaceTarget.transform.position, MoveAs.Dancer, false, false, true));
                break;
            case "FaceIn":
                MyMovePartsQueue.Add(new MovePart(message.EventName, gameObject.GetComponent<Dancer>().FacingInTarget.transform.position, MoveAs.Dancer, false, false, true));
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

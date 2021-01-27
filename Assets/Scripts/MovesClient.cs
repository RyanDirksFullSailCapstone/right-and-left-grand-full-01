using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doozy.Engine;
using UnityEditorInternal;


public class MovesClient : MonoBehaviour
{
    public GameObject DoMoveWithDancer;
    public List<MovePart> thisMovePartsList = new List<MovePart>();
    public GameObject squareDanceMove;

    public List<string> SquareDanceMovesList = new List<string>();
    public void AddCallToArray(string callId)
    {
        SquareDanceMovesList.Add(callId);
        //squareDanceMoves = new int[SquareDanceMovesList.Count];
        //int i = 0;
        //foreach (int move in SquareDanceMovesList)
        //{
        //    squareDanceMoves[i++] = move;
        //}
    }

    private int nextPart = 0;
    private int currentPart = 0;

    public bool isMoving;

    // Start is called before the first frame update
    void Start()
    {
        
    }



    // Update is called once per frame
    void Update()
    {
        int LastPartIndex = thisMovePartsList.Count - 1;
        if (!(LastPartIndex < 0))
        {
            if (LastPartIndex > nextPart && currentPart == nextPart)
            {
                nextPart++;
            }

            if (nextPart > currentPart)
            {
                if (!isMoving)
                    currentPart++;
                isMoving = true;
            }

            if (isMoving)
            {
                // if dancer is situated StateReadyTo.Move rather than Relate - always true at this point


                GameEventMessage.SendEvent(thisMovePartsList[currentPart].Name);
                // check conditions for complete
                bool isComplete = CheckComplete(thisMovePartsList[currentPart].Name,thisMovePartsList[currentPart].Target);
                if (isComplete)
                {
                    if (nextPart > currentPart)
                        currentPart++;
                    else
                        isMoving = false;
                }
            }
        }
    }

    bool CheckComplete(string CompleteCondition, Vector3 targetCompare)
    {
        // Doing a left allemande
        // Face Target: Corner
        // Contact arm: left
        // Contact type: forearm grip
        // Motion Pinwheel around left forearm grip
        // IsComplete: see partner
        switch (CompleteCondition)
        {
            case "FaceCorner":
                float angle = 5f;
                return ((Vector3.Angle(transform.forward, targetCompare - transform.position) >
                                 angle)) ;
                break;
            default: return false;
        }
    }


}
public class MovePart:MonoBehaviour
{
    public string Name { get; set; }
    public Vector3 Target { get; set; }

    public MovePart(string name, Vector3 target)
    {
        Name = name;
        Target = target;
    }
}

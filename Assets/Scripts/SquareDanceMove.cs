using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doozy.Engine;

public enum MoveAs
{
    Dancer,
    Couple
}
public class SquareDanceMove : MonoBehaviour
{
    public MoveAs moveAs;
    // Start is called before the first frame update
    void Start()
    {   

        //GameEventMessage.SendEvent("SquareTheSet");
    }

    //// Update is called once per frame
    //void Update()
    //{
        
    //}
}

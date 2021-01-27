using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager instance;
    //public int[] squareDanceMoves;

    void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = this;
        //squareDanceMoves = new int[SquareDanceMovesList.Count];
        //int i = 0;
        //foreach (int move in SquareDanceMovesList)
        //{
        //    squareDanceMoves[i++] = move;
        //}
    }
}

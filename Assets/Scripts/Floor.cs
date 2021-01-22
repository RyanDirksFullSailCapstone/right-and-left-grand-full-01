using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doozy.Engine;

public class Floor : MonoBehaviour
{
    public bool tapMakesDancerOn = false;
    public int squareCount = 1;
    private GameObject squareDanceMove;

    // Start is called before the first frame update
    void Start()
    {
        //squareDanceMove =
        //    Instantiate(Resources.Load("Grid")) as GameObject;
        //squareDanceMove =
        //    Instantiate(Resources.Load("Square Dance Move")) as GameObject;
        //for (int i = 0; i < squareCount; i++)
        //{
        //    GameObject square = Instantiate(Resources.Load("Grid"),
        //        new Vector3(0,0,0),Quaternion.identity) as GameObject;
        //    for (int dancerCount = 0; dancerCount < 8; dancerCount++)
        //    {
        //        GameObject newDancerLeft =
        //            Instantiate(Resources.Load("Dancer"),
        //                new Vector3(Random.Range(-1.5f, 1.5f), 1.3f, Random.Range(-1.5f, 1.5f)),
        //                Quaternion.identity) as GameObject;
        //    }
        //}
    }

    void OnMouseDown()
    {
        if (tapMakesDancerOn)
        {
            GameObject newDancer =
                Instantiate(Resources.Load("Dancer"),
                    new Vector3(Random.Range(-1.5f, 1.5f), 1.3f, Random.Range(-1.5f, 1.5f)),
                    Quaternion.identity) as GameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

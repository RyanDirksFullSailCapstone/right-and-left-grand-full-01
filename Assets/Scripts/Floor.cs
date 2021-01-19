using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    public bool tapMakesDancerOn = false;
    public int squareCount = 1;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < squareCount; i++)
        {
            GameObject square = Instantiate(Resources.Load("Grid"),
                new Vector3(0,0,0),Quaternion.identity) as GameObject;
            for (int dancerCount = 0; dancerCount < 8; dancerCount++)
            {
                GameObject newDancerLeft =
                    Instantiate(Resources.Load("Dancer"),
                        new Vector3(Random.Range(-1.5f, 1.5f), 1.3f, Random.Range(-1.5f, 1.5f)),
                        Quaternion.identity) as GameObject;
            }
        }
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

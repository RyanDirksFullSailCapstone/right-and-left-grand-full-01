using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    public bool tapMakesDancerOn = false;

    // Start is called before the first frame update
    void Start()
    {
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

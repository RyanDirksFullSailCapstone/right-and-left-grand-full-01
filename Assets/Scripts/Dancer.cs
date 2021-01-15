using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dancer : MonoBehaviour
{
    public GameObject Selected;

    // https://www.youtube.com/watch?v=c69oZprM1oc&feature=emb_logo
    void OnMouseDown()
    {
        Debug.Log("Clicked");
        Selected.active = !Selected.active;
    }

    void Update()
    {
        //// https://gamedevacademy.org/unity-touch-input-tutorial/
        //{
        //    if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        //    {
        //        Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
        //        RaycastHit hit;

        //        if (Physics.Raycast(ray, out hit))
        //        {
        //            if (hit.collider != null)
        //            {
        //                Debug.Log("Touched");
        //            }
        //        }
        //    }
        //}
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit hit;

        //    if (Physics.Raycast(ray, out hit))
        //    {
        //        if (hit.collider != null)
        //        {
        //            Debug.Log("Clicked");
        //        }
        //    }
        //}
    }
}

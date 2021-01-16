using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DancerStartedOnThe
{
    RightSide,
    LeftSide
}
public class Dancer : MonoBehaviour
{
    public GameObject Selected;
    public DancerStartedOnThe SideIStartedOn;
    public GameObject DancerRightToken;
    public GameObject DancerLeftToken;
    public GameObject DancerRightSelected;
    public GameObject DancerLeftSelected;
    public GameObject RightHandTarget;
    public GameObject LeftHandTarget;
    public GameObject DancerToMyRightBounds;
    public GameObject DancerToMyLeftBounds;
    public GameObject Partner;
    public GameObject Corner;

    // https://www.youtube.com/watch?v=c69oZprM1oc&feature=emb_logo
    void OnMouseDown()
    {
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

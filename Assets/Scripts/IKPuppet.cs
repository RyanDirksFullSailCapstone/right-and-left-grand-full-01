using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PartnerHand
{
    Left,
    Right
};

public class IKPuppet : MonoBehaviour
{
    public Transform target;
    public AvatarIKGoal avatarIKPartnerHand;
    public MoveAs movingAs = MoveAs.Couple;

    private Animator anim;

    private float weight = 1f;

    public Vector3 LeftHandTarget { get; set; }
    public HandPosition LeftHandPosition { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void OnAnimatorIK(int layerIndex)
    {
        if (LeftHandPosition == HandPosition.ForearmGrip)
        {
            anim.SetIKPosition(AvatarIKGoal.LeftHand, LeftHandTarget);
            anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
        }
        if (movingAs == MoveAs.Couple)
        {
            anim.SetIKPosition(avatarIKPartnerHand, target.position);
            anim.SetIKPositionWeight(avatarIKPartnerHand, weight);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.UpArrow))
        //{
        //    target.transform.Translate(0f,.1f,0f);
        //}
        //if (Input.GetKeyDown(KeyCode.DownArrow))
        //{
        //    target.transform.Translate(0f, -.1f, 0f);
        //}
        //if (Input.GetKeyDown(KeyCode.LeftArrow))
        //{
        //    target.transform.Translate(0.1f, 0f, 0f);
        //}
        //if (Input.GetKeyDown(KeyCode.RightArrow))
        //{
        //    target.transform.Translate(-0.1f, 0f, 0f);
        //}
    }
}

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
    public PartnerHand partnerHand;
    private AvatarIKGoal avatarIKPartnerHand;
    public GameObject squareDanceMove;

    private Animator anim;

    private float weight = 1f;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        avatarIKPartnerHand = (partnerHand == PartnerHand.Left ? AvatarIKGoal.LeftHand : AvatarIKGoal.RightHand);
    }

    void OnAnimatorIK(int layerIndex)
    {
        if (squareDanceMove.GetComponent<SquareDanceMove>().moveAs == MoveAs.Couple)
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

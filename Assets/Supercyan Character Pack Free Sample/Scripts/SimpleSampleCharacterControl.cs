using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Doozy.Engine;
using Unity.Transforms;

public class SimpleSampleCharacterControl : MonoBehaviour
{
    private enum ControlMode
    {
        /// <summary>
        /// Up moves the character forward, left and right turn the character gradually and down moves the character backwards
        /// </summary>
        Tank,
        /// <summary>
        /// Character freely moves in the chosen direction from the perspective of the camera
        /// </summary>
        Direct,
        /// <summary>
        /// Character freely moves in the chosen direction from the perspective of the camera
        /// </summary>
        SquareDanceMoves
    }

    public bool isReadyForNextMove;
    public bool isFacing = false;
    public float positionRange = 0.5f;
    public GameObject squareDanceMove;
    public GameObject forwardSpaceTarget;
    private Vector3 targetPosition;
    private Vector3 facingTarget;
    public GameObject targetGameObject { get; set; }
    public bool isMoving;
    public bool isMovingBackwards;
    [SerializeField] MoveAs dancerStatus = MoveAs.Dancer;
    [SerializeField] private float m_moveSpeed = 2;
    [SerializeField] private float m_turnSpeed = 200;
    [SerializeField] private float m_jumpForce = 4;

    [SerializeField] private Animator m_animator = null;
    [SerializeField] private Rigidbody m_rigidBody = null;

    [SerializeField] private ControlMode m_controlMode = ControlMode.Direct;
    
    private MoveAs m_movingAs = MoveAs.Dancer;
    private float m_currentV = 0;
    private float m_currentH = 0;

    private readonly float m_interpolation = 10;
    private readonly float m_walkScale = 0.33f;
    private readonly float m_backwardsWalkScale = 0.16f;
    private readonly float m_backwardRunScale = 0.66f;

    private bool m_wasGrounded;
    private Vector3 m_currentDirection = Vector3.zero;

    private float m_jumpTimeStamp = 0;
    private float m_minJumpInterval = 0.25f;
    private bool m_jumpInput = false;

    private bool m_isGrounded;

    private List<Collider> m_collisions = new List<Collider>();
    private IKPuppet myIkPuppet;
    public bool doUpdateTargetPosition;
    public HandPosition leftHandPosition { get; set; }
    public Vector3 leftHandTarget { get; set; }


    private void Awake()
    {
        if (!m_animator) { gameObject.GetComponent<Animator>(); }
        if (!m_rigidBody) { gameObject.GetComponent<Animator>(); }

        if (!myIkPuppet)
        {
            myIkPuppet = gameObject.GetComponent<IKPuppet>();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint[] contactPoints = collision.contacts;
        for (int i = 0; i < contactPoints.Length; i++)
        {
            if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
            {
                if (!m_collisions.Contains(collision.collider))
                {
                    m_collisions.Add(collision.collider);
                }
                m_isGrounded = true;
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        ContactPoint[] contactPoints = collision.contacts;
        bool validSurfaceNormal = false;
        for (int i = 0; i < contactPoints.Length; i++)
        {
            if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
            {
                validSurfaceNormal = true; break;
            }
        }

        if (validSurfaceNormal)
        {
            m_isGrounded = true;
            if (!m_collisions.Contains(collision.collider))
            {
                m_collisions.Add(collision.collider);
            }
        }
        else
        {
            if (m_collisions.Contains(collision.collider))
            {
                m_collisions.Remove(collision.collider);
            }
            if (m_collisions.Count == 0) { m_isGrounded = false; }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (m_collisions.Contains(collision.collider))
        {
            m_collisions.Remove(collision.collider);
        }
        if (m_collisions.Count == 0) { m_isGrounded = false; }
    }

    private void Update()
    {
        if (!m_jumpInput && Input.GetKey(KeyCode.Space))
        {
            m_jumpInput = true;
        }
    }

    private void FixedUpdate()
    {
        m_animator.SetBool("Grounded", m_isGrounded);

        switch (m_controlMode)
        {
            case ControlMode.Direct:
                DirectUpdate();
                break;

            case ControlMode.Tank:
                TankUpdate();
                break;

            case ControlMode.SquareDanceMoves:
                DanceUpdate();
                break;

            default:
                Debug.LogError("Unsupported state");
                break;
        }

        m_wasGrounded = m_isGrounded;
        m_jumpInput = false;
    }

    public CompleteCondition CompleteCondition { get; set; }
    RaycastHit hit;
    private bool HasMetCompleteCondition(CompleteCondition completeCondition)
    {
        if (gameObject.name == "Dancer1Right")
        {
            Debug.Log($"CompleteCondition {CompleteCondition} {completeCondition}");
        }
        switch (completeCondition)
        {
            case CompleteCondition.SeePartner:
                Debug.DrawRay(transform.position + Vector3.up * .5f, transform.forward*2f + (gameObject.GetComponent<Dancer>().DancerLeftToken.activeSelf ? -transform.right : transform.right) * 2f, Color.green);
                Ray ray = new Ray(transform.position + Vector3.up * .5f, transform.forward * 2f + (gameObject.GetComponent<Dancer>().DancerLeftToken.activeSelf?-transform.right:transform.right) * 2f);
                if (Physics.Raycast(ray, out hit, 1f))
                {
                    Debug.Log($"{gameObject.name} sees {hit.collider.gameObject.name}, looking for {gameObject.GetComponent<Dancer>().Partner.name} found={hit.collider.gameObject.name == gameObject.GetComponent<Dancer>().Partner.name}");
                    if (hit.collider.gameObject.name == gameObject.GetComponent<Dancer>().Partner.name)
                    {
                        Debug.Log($"{gameObject.name} CompleteCondition true");
                        return true;
                    }
                }
                return false;
            case CompleteCondition.TargetMet:
                if (gameObject.name == "Dancer1Right")
                {
                    Debug.Log($"{Vector3.Distance(targetGameObject.transform.position, transform.position)}");
                }
                return Vector3.Distance(targetGameObject.transform.position,transform.position) < positionRange;
        }
        return false;
    }

    public void DanceUpdate()
    {
        isReadyForNextMove = IsReadyForNextMove();
        myIkPuppet.movingAs = m_movingAs;
        myIkPuppet.LeftHandTarget = leftHandTarget;
        myIkPuppet.LeftHandPosition = leftHandPosition;
        if (isFacing)
        {
            float AngleDifference = Vector3.Angle(transform.forward,
                new Vector3(facingTarget.x - transform.position.x, 0, facingTarget.z - transform.position.z) -
                transform.forward);

            if(!(AngleDifference % 180 < 2 || (180 - AngleDifference) < 2))
            {
                Vector3 targetDirection = new Vector3(facingTarget.x - transform.position.x, 0,
                    facingTarget.z - transform.position.z);

                // The step size is equal to speed times frame time.
                float singleStep = m_moveSpeed * Time.deltaTime;

                // Rotate the forward vector towards the target direction by one step
                Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

                // Draw a ray pointing at our target in
                Debug.DrawRay(transform.position, newDirection, Color.red);

                // Calculate a rotation a step closer to the target and applies rotation to this object
                transform.rotation = Quaternion.LookRotation(newDirection);

                m_animator.SetFloat("MoveSpeed", m_walkScale);
            }
            else
            {
                isFacing = false;
                m_animator.SetFloat("MoveSpeed", 0);
            }
        }

        // while isMoving && not at target
        if ( 
            (isMoving || isMovingBackwards) && 
            (
                ( !doUpdateTargetPosition && (Math.Abs(targetPosition.z - gameObject.transform.position.z) > positionRange || Math.Abs(targetPosition.x - gameObject.transform.position.x) > positionRange)) ||
                (  doUpdateTargetPosition && !HasMetCompleteCondition(CompleteCondition))
            )
        )
        {
            //if (gameObject.name == "Dancer1Right")
            //{
            //    Debug.Log($"{Math.Abs(targetGameObject.transform.position.z - gameObject.transform.position.z)} {Math.Abs(targetGameObject.transform.position.x - gameObject.transform.position.x)} {doUpdateTargetPosition} {(Math.Abs(targetPosition.z - gameObject.transform.position.z) > positionRange || Math.Abs(targetPosition.x - gameObject.transform.position.x) > positionRange)} {(Math.Abs(targetGameObject.transform.position.z - gameObject.transform.position.z) > positionRange || Math.Abs(targetGameObject.transform.position.x - gameObject.transform.position.x) > positionRange)}");
            //}
            //   move towards target
            // set isMoving false or update target

            float v = isMoving ? 1 : -1;
            float h = 0;

            bool walk = true;

            //if (v < 0)
            //{
            //    if (walk)
            //    {
            //        v *= m_backwardsWalkScale;
            //    }
            //    else
            //    {
            //        v *= m_backwardRunScale;
            //    }
            //}
            //else if (walk)
            //{
                v *= m_walkScale;
            //}

            m_currentV = Mathf.Lerp(m_currentV, v, Time.deltaTime * m_interpolation);
            m_currentH = Mathf.Lerp(m_currentH, h, Time.deltaTime * m_interpolation);

            transform.position += transform.forward * m_currentV * m_moveSpeed * Time.deltaTime;
            transform.Rotate(0, m_currentH * m_turnSpeed * Time.deltaTime, 0);

            m_animator.SetFloat("MoveSpeed", m_currentV);

            Vector3 targetDirection;
            // Determine which direction to rotate towards
            if (!doUpdateTargetPosition)
            {
                targetDirection = new Vector3(targetPosition.x - transform.position.x, 0,
                    targetPosition.z - transform.position.z);
            }
            else
            {
                targetDirection = new Vector3(targetGameObject.transform.position.x - transform.position.x, 0,
                    targetGameObject.transform.position.z - transform.position.z);
            }

            // The step size is equal to speed times frame time.
            float singleStep = m_moveSpeed * Time.deltaTime;

            // Rotate the forward vector towards the target direction by one step
            if (!isMovingBackwards)
            {
                Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

                // Draw a ray pointing at our target in
                //Debug.DrawRay(transform.position, newDirection, Color.red);

                // Calculate a rotation a step closer to the target and applies rotation to this object
                transform.rotation = Quaternion.LookRotation(newDirection);
            }
        }
        else
        {
            m_animator.SetFloat("MoveSpeed", 0);
            isMoving = false;
            isMovingBackwards = false;
        }
    }

    private void TankUpdate()
    {
        if (m_movingAs == dancerStatus)
        {
            float v = Input.GetAxis("Vertical");
            float h = Input.GetAxis("Horizontal");

            bool walk = Input.GetKey(KeyCode.LeftShift);

            if (v < 0)
            {
                if (walk) { v *= m_backwardsWalkScale; }
                else { v *= m_backwardRunScale; }
            }
            else if (walk)
            {
                v *= m_walkScale;
            }

            m_currentV = Mathf.Lerp(m_currentV, v, Time.deltaTime * m_interpolation);
            m_currentH = Mathf.Lerp(m_currentH, h, Time.deltaTime * m_interpolation);

            transform.position += transform.forward * m_currentV * m_moveSpeed * Time.deltaTime;
            transform.Rotate(0, m_currentH * m_turnSpeed * Time.deltaTime, 0);

            m_animator.SetFloat("MoveSpeed", m_currentV);

            JumpingAndLanding();
        }
    }

    private void DirectUpdate()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        Transform camera = Camera.main.transform;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            v *= m_walkScale;
            h *= m_walkScale;
        }

        m_currentV = Mathf.Lerp(m_currentV, v, Time.deltaTime * m_interpolation);
        m_currentH = Mathf.Lerp(m_currentH, h, Time.deltaTime * m_interpolation);

        Vector3 direction = camera.forward * m_currentV + camera.right * m_currentH;

        float directionLength = direction.magnitude;
        direction.y = 0;
        direction = direction.normalized * directionLength;

        if (direction != Vector3.zero)
        {
            m_currentDirection = Vector3.Slerp(m_currentDirection, direction, Time.deltaTime * m_interpolation);

            transform.rotation = Quaternion.LookRotation(m_currentDirection);
            transform.position += m_currentDirection * m_moveSpeed * Time.deltaTime;

            m_animator.SetFloat("MoveSpeed", direction.magnitude);
        }

        JumpingAndLanding();
    }

    private void JumpingAndLanding()
    {
        bool jumpCooldownOver = (Time.time - m_jumpTimeStamp) >= m_minJumpInterval;

        if (jumpCooldownOver && m_isGrounded && m_jumpInput)
        {
            m_jumpTimeStamp = Time.time;
            m_rigidBody.AddForce(Vector3.up * m_jumpForce, ForceMode.Impulse);
        }

        if (!m_wasGrounded && m_isGrounded)
        {
            m_animator.SetTrigger("Land");
        }

        if (!m_isGrounded && m_wasGrounded)
        {
            m_animator.SetTrigger("Jump");
        }
    }

    public bool IsReadyForNextMove()
    {
        return m_isGrounded && !isMoving && !isMovingBackwards && !isFacing;
    }

    public void setFacingTarget(Vector3 transformPosition)
    {
        facingTarget = transformPosition;
    }

    public void setTargetPosition(Vector3 target)
    {
        targetPosition = target;
    }

    public void setMovingAs(MoveAs movingAs)
    {
        m_movingAs = movingAs;
    }
}

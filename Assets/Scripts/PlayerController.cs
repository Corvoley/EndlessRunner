using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private ProcessInputs processInputs;
    
    [SerializeField] private float horizontalSpeed;
    [SerializeField] private float forwardSpeed;
    [SerializeField] private float laneDistance;

    [Header("Jump")]
    [SerializeField] private float jumpDistanceZ;
    [SerializeField] private float jumpHeightY;
    private float jumpStartZ;
    [SerializeField] private float jumpLerpSpeed;
    [Header("Roll")]
    [SerializeField] float rollDistanceZ;
    [SerializeField] private Collider normalCollider;
    [SerializeField] private Collider rollCollider;
    private float rollStartZ;
    public bool IsJumping { get; private set;}
    public bool IsIdle { get; private set;}
    public bool IsRolling { get; private set; }
    public float JumpDuration => jumpDistanceZ / forwardSpeed;
    public float RollDuration => rollDistanceZ / forwardSpeed;

    private float targetPositionX;
    private Vector3 initialPosition;
    

    private float LeftLaneX => initialPosition.x - laneDistance;
    private float RightLaneX => initialPosition.x + laneDistance;
    private bool CanJump => !IsJumping;
    private bool CanRoll => !IsRolling;

    private void Awake()
    {
        IsIdle = true;
        initialPosition = transform.position;
        processInputs = GetComponent<ProcessInputs>();
        
    }

    private void Update()
    {
        IsIdle = false;
        ProcessInputs();
        ProcessRoll();
        Vector3 position = transform.position;

        position.x = ProcessLaneMovement();
        position.y = ProcessJump();
        position.z = ProcessForwardMovement();


        transform.position = position;
    }

    private void ProcessInputs()
    {                  

        if (processInputs.IsLeftKeyDown())
        {
            targetPositionX -= laneDistance;
        }
        if (processInputs.IsRightKeyDown())
        {
            targetPositionX += laneDistance;
        }
        if (processInputs.IsJumpKeyDown() && CanJump )
        {
            StartJump();
        }
        if (processInputs.IsRollKeyDown() && CanRoll)
        {
            StartRoll();            
        }


        targetPositionX = Mathf.Clamp(targetPositionX, LeftLaneX, RightLaneX);
    }
    private float ProcessLaneMovement()
    {
        return Mathf.Lerp(transform.position.x, targetPositionX, horizontalSpeed * Time.deltaTime);
    }
    private float ProcessForwardMovement()
    {
        return transform.position.z + forwardSpeed * Time.deltaTime;
    }
    private float ProcessJump()
    {
        float deltaY = 0;
        if (IsJumping)
        {
            float jumpCurrentProgress = transform.position.z - jumpStartZ;
            float jumpPercent = jumpCurrentProgress / jumpDistanceZ;
            if (jumpPercent >= 1)
            {
                StopJump();
            }
            else
            {
                deltaY = Mathf.Sin(Mathf.PI * jumpPercent) * jumpHeightY;
            }          
        }
         float targetPositionY = initialPosition.y + deltaY;
        return Mathf.Lerp(transform.position.y, targetPositionY, Time.deltaTime * jumpLerpSpeed);
        
    }

    private void StartJump()
    {
        IsJumping = true;
        jumpStartZ = transform.position.z;
        StopRoll();
    }
    private void StopJump()
    {
        IsJumping = false;
    }
    private void StartRoll()
    {
        rollStartZ = transform.position.z;
        IsRolling = true;
        normalCollider.enabled = false;
        rollCollider.enabled = true;
       
        StopJump();
    }
    private void StopRoll()
    {
        IsRolling = false;       
        normalCollider.enabled = true;
        rollCollider.enabled = false;

    }
    private void ProcessRoll()
    {
        if (IsRolling)
        {
            float percent = (transform.position.z - rollStartZ) / rollDistanceZ;
            if (percent >=1)
            {
                StopRoll();
            }
        }
    }


}

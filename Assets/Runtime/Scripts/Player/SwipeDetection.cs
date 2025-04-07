using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwipeDetection : MonoBehaviour
{
    public Action OnSwipeUp;
    public Action OnSwipeDown;
    public Action OnSwipeLeft;
    public Action OnSwipeRight;

    [SerializeField] private PlayerInputActions playerInputAction;

    [SerializeField] private float maxSwipeTime = 0.5f;
    [SerializeField] private float minSwipeDistance = 100;

    [SerializeField] private bool swiping = false;
    [SerializeField] private float swipeAngle = 0f;

    private Vector2 swipeStartPosition;
    private Vector2 swipeEndPosition;

    private float swipeStartTime;
    private float swipeEndTime;


    private void Awake()
    {
        playerInputAction = new PlayerInputActions();

    }

    private void OnEnable()
    {
        playerInputAction.Player.Enable();
    }
    private void OnDisable()
    {
        playerInputAction.Player.Enable();
    }

    private void Start()
    {
        playerInputAction.Player.Touch.started += Touch_started;
        playerInputAction.Player.Touch.canceled += Touch_canceled;
    }
    private void Touch_started(InputAction.CallbackContext context)
    {
        swiping = true;
        swipeStartPosition = playerInputAction.Player.TouchPosition.ReadValue<Vector2>();
        swipeStartTime = Time.time;

    }
    private void Touch_canceled(InputAction.CallbackContext context)
    {
        if (swiping)
        {
            swipeEndPosition = playerInputAction.Player.TouchPosition.ReadValue<Vector2>();
            Vector2 swipeDelta = swipeEndPosition - swipeStartPosition;

            if (swipeDelta.magnitude >= minSwipeDistance)
            {
                swipeAngle = Mathf.Atan2(swipeDelta.y, swipeDelta.x) * Mathf.Rad2Deg;

                if (swipeAngle < 0f)
                {
                    swipeAngle += 360f;
                }

                if(swipeAngle > 45f && swipeAngle <= 135f)
                {
                    OnSwipeUp?.Invoke();

                   // Debug.Log("Up");
                }
                else if (swipeAngle > 225f && swipeAngle <= 315f)
                {
                    OnSwipeDown?.Invoke();
                    //Debug.Log("Down");
                }
                else if (swipeAngle > 135f && swipeAngle <= 225f)
                {
                    OnSwipeLeft?.Invoke();

                   // Debug.Log("Left");
                }
                else if ((swipeAngle > 315f && swipeAngle <= 360f) || (swipeAngle >= 0f && swipeAngle <= 45f))
                {
                    OnSwipeRight?.Invoke();

                   // Debug.Log("Right");
                }


            }
        }



    }





}

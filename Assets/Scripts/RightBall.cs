using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;

public class RightBall : MonoBehaviour
{
    [Header("Tween Config")]
    [SerializeField] private Vector3 desiredPosRight;
    [SerializeField] private Vector3 desiredPosLeft;
    [SerializeField] private float jumpPower;
    [SerializeField] private float durationOfTween;
    private bool snapping = false;
    private bool performTweenRight;
    private bool performTweenLeft;
    private int numOfJumps = 0;
    public bool finishedMoving = false;
    private BallHolder ballHolder;
    // Tween Condition
    private float xValue;
    private float yValue;
    //Touch Cache
    private Touch touch;
    // Losing/Winning Condition
    public bool isLoseRightBall = false;
    public bool isWinRightBall = false;
    public bool canMove;
    [SerializeField] private GameObject leftBallObj;
    [SerializeField] private LeftBall leftBallScript;


    //new floats
    [SerializeField] private float minYValue = 0.2491584f;
    [SerializeField] private float maxYValue = 1.148f;
    private float ogJumpPower;
    private float startedJumpTime;
    private bool inMidTrigger = false;
    private void Awake()
    {
        leftBallScript = FindObjectOfType<LeftBall>();
        ballHolder = GetComponentInParent<BallHolder>();
        finishedMoving = true;
    }
    private void Start()
    {
        ogJumpPower = jumpPower;
    }
    //Tween Action
    private void Update()
    {
        if (canMove)
        {
            Movement();  
        }
        MoveCheck();
    }

    private void Movement()
    {
        xValue = transform.position.x;
        /*
         if (Input.touchCount > 0)
         {
             touch = Input.GetTouch(0);
             if (touch.phase == TouchPhase.Began && xValue >= 2.4f && leftBallScript.finishedMoving)
             {
                 transform.DOLocalJump(desiredPosLeft, jumpPower, numOfJumps, durationOfTween, snapping);
             }
             else if (touch.phase == TouchPhase.Began && xValue <= 0.2f && leftBallScript.finishedMoving)
             {
                 transform.DOLocalJump(desiredPosRight, jumpPower, numOfJumps, durationOfTween, snapping);
             }
         }
         */
        if (Input.GetKeyDown(KeyCode.Space) && !inMidTrigger)
        {
            startedJumpTime += Time.deltaTime;
            if (leftBallObj.transform.position.x < transform.position.x)
            {
                transform.DOLocalJump(desiredPosLeft, jumpPower, numOfJumps, durationOfTween, snapping);
            }

            else
            {
                transform.DOLocalJump(desiredPosRight, jumpPower, numOfJumps, durationOfTween, snapping);
            }
        }
     
        /*
        if (Input.GetKeyDown(KeyCode.Space) && xValue >= 2.411 && ballHolder.bothFinished)
        {
            transform.DOLocalJump(desiredPosLeft, jumpPower, numOfJumps, durationOfTween, snapping);
        }

        else if (Input.GetKeyDown(KeyCode.Space) && xValue <= 0.112 && ballHolder.bothFinished)
        {
            transform.DOLocalJump(desiredPosRight, jumpPower, numOfJumps, durationOfTween, snapping);
        }
        */

    }
    private void MoveCheck()
    {
        yValue = transform.position.y;
        if(yValue >= 1.148)
        {
            finishedMoving = true;
            //jumpPower = ogJumpPower;
            startedJumpTime = 0;
        }
        else
        {
            finishedMoving = false;
            //jumpPower = 0;
        }
    }
    
    private void PerformTweenRight()
    {
        if(performTweenRight)
        {
            transform.DOLocalJump(desiredPosRight, jumpPower, numOfJumps, durationOfTween, snapping);
        }
    }

    private void PerformTweenLeft()
    {
        if (performTweenLeft)
        {
            transform.DOLocalJump(desiredPosLeft, jumpPower, numOfJumps, durationOfTween, snapping);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            isLoseRightBall = true;
            Destroy(gameObject);
            Destroy(leftBallObj);
        }

        if (other.gameObject.CompareTag("WinTrigger"))
        {
            isWinRightBall = true;

        }
        if (other.gameObject.CompareTag("MidTrigger"))
        {
            inMidTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("MidTrigger"))
        {
            inMidTrigger = false;
        }
    }
}


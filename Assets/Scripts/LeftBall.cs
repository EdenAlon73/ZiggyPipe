using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;

public class LeftBall : MonoBehaviour
{
    [Header("Tween Config")]
    [SerializeField] private Vector3 desiredPosRight;
    [SerializeField] private Vector3 desiredPosLeft;
    [SerializeField] private float jumpPower;
    [SerializeField] private float durationOfTween;
    private bool snapping = false;
    private int numOfJumps = 0;
    [SerializeField] private GameObject rightBallObj;
    [SerializeField] private RightBall rightBallScript;
    public bool finishedMoving = false;
    private BallHolder ballHolder;

    private bool performTweenRight;
    private bool performTweenLeft;
    // Tween Condition
    private float xValue;
    private float yValue;
    //Touch Cache
    private Touch touch;
    
    // Losing/Winning Condition
    public bool isLoseLeftBall = false;
    public bool isWinLeftBall = false;
    public bool canMove = true;
    [SerializeField] private float minYValue = 0.25f;
    [SerializeField] private float maxYValue = 1.148f;
    private float ogJumpPower;
    private float startedJumpTime;
    private bool inMidTrigger = false;
    private void Awake()
    {
        rightBallScript = FindObjectOfType<RightBall>();
        ballHolder = GetComponentInParent<BallHolder>();
        finishedMoving = true;
    }
    private void Start()
    {
        ogJumpPower = jumpPower;
    }

    // Tween Action
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
            if (touch.phase == TouchPhase.Began && xValue >= 2.4f && rightBallScript.finishedMoving)
            {
                transform.DOLocalJump(desiredPosLeft, jumpPower, numOfJumps, durationOfTween, snapping);
            }
            else if (touch.phase == TouchPhase.Began && xValue <= 0.2f && rightBallScript.finishedMoving)
            {
                transform.DOLocalJump(desiredPosRight, jumpPower, numOfJumps, durationOfTween, snapping);
            }
        }
        */
        if (Input.GetKeyDown(KeyCode.Space) && !inMidTrigger)
        {
            startedJumpTime += Time.deltaTime;
            if (rightBallObj.transform.position.x < transform.position.x)
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
        if (yValue >= 1.148)
        {
            finishedMoving = true;
            //jumpPower = ogJumpPower;
        }
        else
        {
            finishedMoving = false;
           //jumpPower = 0f;
        }
    }

    private void PerformTweenRight()
    {
        if (performTweenRight)
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
            isLoseLeftBall = true;
            Destroy(gameObject);
            Destroy(rightBallObj);
        }

        if (other.gameObject.CompareTag("WinTrigger"))
        {
            isWinLeftBall = true;
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



using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;

public class RightBall : MonoBehaviour
{
    //Cache Referance:
    private Touch touch;
    private LeftBall leftBallScript;
    private BallHolder ballHolder;
    [SerializeField] private GameObject leftBallObj;
    
    //Tween Movement Conditions
    public bool finishedMoving = false;
    private bool inMidTrigger = false;
    private float xValue;
    private float yValue;
    
    //Win\Lose Conditions
    public bool isLoseRightBall = false;
    public bool isWinRightBall = false;
    public bool canMove = true;
    
    private void Awake()
    {
        leftBallScript = FindObjectOfType<LeftBall>();
        ballHolder = GetComponentInParent<BallHolder>();
        finishedMoving = true;
        inMidTrigger = false;
    }
   
    private void Update()
    {
        if (canMove)
        {
            Movement();  
        }
        //  MoveCheck();
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
            if (leftBallObj.transform.position.x < transform.position.x)
            {
                transform.DOLocalJump(ballHolder.desiredPosLeft, -ballHolder.jumpPower, ballHolder.numOfJumps,
                    ballHolder.durationOfTween, ballHolder.snapping);
            }

            else
            {
                transform.DOLocalJump(ballHolder.desiredPosRight, -ballHolder.jumpPower, ballHolder.numOfJumps,
                    ballHolder.durationOfTween, ballHolder.snapping);
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
    /*
    private void MoveCheck()
    {
        yValue = transform.position.y;
        if(yValue >= 1.192)
        {
            finishedMoving = true;
        }
        else
        {
            finishedMoving = false;
        }
    }
    */
    
   
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
            //inMidTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("MidTrigger"))
        {
            //inMidTrigger = false;
        }
    }
}


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;

public class LeftBall : MonoBehaviour
{
    
    //Cache Referance:
    private Touch touch;
    private RightBall rightBallScript;
    private BallHolder ballHolder;
    [SerializeField] private GameObject rightBallObj;
    
    //Tween Movement Conditions
    public bool finishedMoving = false;
    private bool inMidTrigger = false;
    private float xValue;
    private float yValue;
    
    //Win\Lose Conditions
    public bool isLoseLeftBall = false;
    public bool isWinLeftBall = false;
    public bool canMove = true;
   
    
    
    
    private void Awake()
    {
        rightBallScript = FindObjectOfType<RightBall>();
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
    //        MoveCheck();
       
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
            if (rightBallObj.transform.position.x < transform.position.x)
            {
                //Move Left
                transform.DOLocalJump(ballHolder.desiredPosLeft, -ballHolder.jumpPower, ballHolder.numOfJumps,
                    ballHolder.durationOfTween, ballHolder.snapping);
            }

            else
            {
                //Move Right
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
        if (yValue >= 1.148)
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
            isLoseLeftBall = true;
            Destroy(gameObject);
            Destroy(rightBallObj);
        }

        if (other.gameObject.CompareTag("WinTrigger"))
        {
            isWinLeftBall = true;
        }
        
    }

   

}



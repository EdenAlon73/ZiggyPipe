﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallHolder : MonoBehaviour
{
    public bool bothFinished = false;
    private LeftBall leftBallScript;
    private RightBall rightBallScript;
    
    [Header("Tween Move Config")]
    public Vector3 desiredPosRight;
    public Vector3 desiredPosLeft;
    public float jumpPower;
    public float ogJumpPower;
    public float durationOfTween;
    public bool snapping = false;
    public int numOfJumps = 0;
    private float ballsYValue;
    
    private void Awake()
    {
        leftBallScript = GetComponentInChildren<LeftBall>();
        rightBallScript = GetComponentInChildren<RightBall>();
        ogJumpPower = jumpPower;
    }
    private void Update()
    {
        if(rightBallScript.finishedMoving && leftBallScript.finishedMoving)
        {
            bothFinished = true;
        }
        else
        {
            bothFinished = false;
        }
        UpdateJumpPower();
    }

    private void UpdateJumpPower()
    {
        ballsYValue = leftBallScript.transform.localPosition.y;
        jumpPower = ballsYValue - 1.95f;
    }
}

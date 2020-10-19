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
    private int numOfJumps = 0;
    // Tween Condition
    private float xValue;
    //Touch Cache
    private Touch touch;
    // Losing/Winning Condition
    public bool isLoseRightBall = false;
    public bool isWinRightBall = false;
    public bool canMove;
    
    
    //Tween Action
    private void Update()
    {
        if (canMove)
        {
            Movement();  
        }
        
    }

    private void Movement()
    {
        xValue = transform.position.x;
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began && xValue >= 2.4f)
            {
                transform.DOLocalJump(desiredPosLeft, jumpPower, numOfJumps, durationOfTween, snapping);
            }
            else if (touch.phase == TouchPhase.Began && xValue <= 0.2f)
            {
                transform.DOLocalJump(desiredPosRight, jumpPower, numOfJumps, durationOfTween, snapping);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && xValue >= 2.4f)
        {
            transform.DOLocalJump(desiredPosLeft, jumpPower, numOfJumps, durationOfTween, snapping);
        }

        else if (Input.GetKeyDown(KeyCode.Space) && xValue <= 0.2f)
        {
            transform.DOLocalJump(desiredPosRight, jumpPower, numOfJumps, durationOfTween, snapping);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            isLoseRightBall = true;
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("WinTrigger"))
        {
            isWinRightBall = true;
        }
    }
    
    
}


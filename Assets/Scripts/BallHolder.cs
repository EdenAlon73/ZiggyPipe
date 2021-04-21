using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallHolder : MonoBehaviour
{
    public bool bothFinished = false;
    private LeftBall leftBallScript;
    private RightBall rightBallScript;
    private void Awake()
    {
        leftBallScript = GetComponentInChildren<LeftBall>();
        rightBallScript = GetComponentInChildren<RightBall>();
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            print("Both Finished" + bothFinished);
        }

    }
}

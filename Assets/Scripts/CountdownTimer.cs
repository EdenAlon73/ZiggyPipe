using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    private float currentTime;
    private float startingTime = 3f;
    
    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] private GameObject scoreText;
    [SerializeField] private GameObject ballHolder;
    [SerializeField] private GameObject cameraTracker;
    private LeftBall leftBallScrip;
    private RightBall rightBallScript;
    
    
    

    private void Start()
    {
        currentTime = startingTime;
        leftBallScrip = FindObjectOfType<LeftBall>();
        rightBallScript = FindObjectOfType<RightBall>();
    }

    private void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        countdownText.text = currentTime.ToString("0");
        if (currentTime <= 0)
        {
            currentTime = 0f;
            ballHolder.GetComponent<ConstantMovingForward>().enabled = true;
            cameraTracker.GetComponent<ConstantMovingForward>().enabled = true;
            leftBallScrip.canMove = true;
            rightBallScript.canMove = true;
            scoreText.SetActive(true);
            countdownText.enabled = false;
            


        }
        else
        {
            ballHolder.GetComponent<ConstantMovingForward>().enabled = false;
            cameraTracker.GetComponent<ConstantMovingForward>().enabled = false;
            leftBallScrip.canMove = false;
            rightBallScript.canMove = false;
            scoreText.SetActive(false);
        }
    }
}

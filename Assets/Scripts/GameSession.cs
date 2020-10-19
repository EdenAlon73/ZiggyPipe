using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    [Header("Game Session Config")]
    [SerializeField] public int currentScore = 0;
    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private GameObject youWinCanvas;
    public LeftBall leftBall;
    public RightBall rightBall;

    [SerializeField] private float delayInSeconds = .5f;
    

    private void Awake()
    {
        
        currentScore = 0;
        gameOverCanvas.SetActive(false);
        youWinCanvas.SetActive(false);
        rightBall = FindObjectOfType<RightBall>();
        leftBall = FindObjectOfType<LeftBall>();

    }

    private void Update()
    {
        PlayerLost();
        PlayerWin();
        SetUpSingleton();
    }


    private void SetUpSingleton()
    {
        int numberGameSession = FindObjectsOfType<GameSession>().Length;
        
        if (numberGameSession > 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);   
        }
    }
    

    public void AddToScore(int pointsPerZigzag)
    {
        currentScore += pointsPerZigzag;
       
    }

    public int GetScore()
    {
        return currentScore;
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }


    private void PlayerLost()
   {
       
           if (leftBall.isLoseLeftBall || rightBall.isLoseRightBall)
           {
               
               StartCoroutine(LoadGameOverCanvas());
           }
       
       
   }

    private void PlayerWin()
    {

        if (leftBall.isWinLeftBall || rightBall.isWinRightBall)
        {
            PlayerPrefs.SetInt("levelReached", SceneManager.GetActiveScene().buildIndex + 1);
            StartCoroutine(LoadYouWinCanvas());
        }

    }
 
  IEnumerator LoadGameOverCanvas()
   {
       gameOverCanvas.SetActive(true);
           yield return new WaitForSeconds(delayInSeconds);
          
   }
  
  IEnumerator LoadYouWinCanvas()
  {
      youWinCanvas.SetActive(true);
      yield return new WaitForSeconds(delayInSeconds);
    
  }
   
   



}



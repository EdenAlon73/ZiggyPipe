﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinLogic : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        print("ball In");
        if (other.CompareTag("RightBall"))
        {
            Destroy(this.gameObject);
        }
        if (other.CompareTag("LeftBall"))
        {
            Destroy(this.gameObject);
        }
    }
}

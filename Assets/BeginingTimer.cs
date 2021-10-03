using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Water2D;

public class BeginingTimer : MonoBehaviour
{
    public GameObject waterSpawner;
    public float targetTime = 10.0f;
    void Update(){
        
        targetTime -= Time.deltaTime;
        
        if (targetTime <= 0.0f)
        {
            timerEnded();
        }
        
    }
    void timerEnded()
    {
        
        transform.gameObject.SetActive(false);
        
    }
    
}


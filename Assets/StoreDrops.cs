using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using Water2D;

public class StoreDrops : MonoBehaviour
{
    private int dropCount;
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(dropCount);
        } 
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag=="Metaball_liquid")
        {
            dropCount += 1;
            
            
        }
    }
}

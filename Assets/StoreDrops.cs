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
    private Water2D_Spawner script;
    public GameObject waterSpawner;

    void Start()
    {
        script = waterSpawner.GetComponent<Water2D_Spawner>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            waterSpawner.SetActive(true);
            transform.Rotate(transform.rotation.x,transform.rotation.y,-145f);
            
            Debug.Log(dropCount);
        } 
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag=="Metaball_liquid")
        {
            dropCount += 1;
            if (other.gameObject!=null)
            {
                // GELEN OBJELERİ CHILD OBJEYE EKLEMEK script.WaterDropsObjects.Append(other.gameObject);
                Destroy(other.gameObject);    
            }
            
        }
    }
}

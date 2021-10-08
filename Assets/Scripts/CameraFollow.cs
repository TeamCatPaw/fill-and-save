using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using Debug = UnityEngine.Debug;

public class CameraFollow : MonoBehaviour
{

    public static CameraFollow instance;
    [SerializeField]
    public Dictionary<GameObject,float> drops = new Dictionary<GameObject,float>();

    public bool canCameraMove = true;
    [Range(0,1)]
    public float cameraSpeed;
    
    private float lowestY;
    private Vector3 targetPos;

    [SerializeField] private Vector3 _offset;
    void Start()
    {
        instance = this;
    }

    void Update()
    {
        CalculateTargetPos();
        MoveCamera();
    }
    void CalculateTargetPos()
    {
        if (drops.Any())
        {
            lowestY = drops.Values.Min();
            lowestY = (int) Math.Round(lowestY);
        }
        targetPos = new Vector3(transform.position.x, lowestY, transform.position.z);
    }
    void MoveCamera()
    {
        if (transform.position.y- (targetPos.y + _offset.y )> 0.1f  && canCameraMove )
        {
            transform.position = Vector3.Lerp(transform.position, targetPos + _offset, cameraSpeed/50f);

        }
    }
    
}

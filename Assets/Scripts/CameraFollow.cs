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

    private bool _moveToDown = false;
    void Start()
    {
        instance = this;
        EventManager.GetInstance().OnCupPassed += MoveToDown;
    }

    void Update()
    {
        CalculateTargetPos();
        MoveCamera();
        EventManager.GetInstance().OnDropPlaced += StopCamera;

        if (_moveToDown) {
            Vector3 _camPosition = transform.position;

            _camPosition.y = Mathf.Lerp(_camPosition.y, -11f, 0.05f);
            transform.position = _camPosition;
        }
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
    private void StopCamera() {
        StartCoroutine(StopTimer());
    }
    private IEnumerator StopTimer() {
        yield return new WaitForSeconds(1f);
        canCameraMove = false;
    }
    private void MoveToDown() {
        StartCoroutine(MoveToDownTimer());
    }
    private IEnumerator MoveToDownTimer() {
        _moveToDown = true;
        yield return new WaitForSeconds(1.5f);
        _moveToDown = false;
    }
}

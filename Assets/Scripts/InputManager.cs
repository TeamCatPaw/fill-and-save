using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private int _currentCupId;
    [SerializeField] private GameObject[] _cups;

    [SerializeField] private float _pouringSec;
    private bool _isStageStarted = false;

    private bool _isRotating = false;

    private bool _isReady = true;

    private bool _moveToDown = false;

    private void Start() {
        EventManager.GetInstance().OnCupPassed += NextCup;

        _cups = GameObject.FindGameObjectsWithTag("Collect");
        if (_cups.Length > 1) {
            if (_cups[0].transform.position.y < _cups[1].transform.position.y) {
                GameObject cup = _cups[0];
                _cups[0] = _cups[1];
                _cups[1] = cup;
            }
            _cups[1].GetComponent<BoxCollider2D>().enabled = true;
        }
        
        
        SpawnManager.GetInstance()._currentCup = _cups[0].transform;
    }

    private void Update() {

        if (Input.GetKeyDown("a")) {
            NextCup();
        }
        if (_isReady) {
            if (Input.GetMouseButtonDown(0) && !_isStageStarted) {
                StartCoroutine(FirstPouringTimer());
            }

            if (Input.GetMouseButton(0)) {
                Vector3 _cupPosition = _cups[_currentCupId].transform.position;
                _cupPosition.x = Mathf.Clamp(Input.mousePosition.x / Screen.width * 8f, 1.7f, 5.5f);

                _cups[_currentCupId].transform.position = _cupPosition;
            }

            if (_isRotating) {

                Vector3 cupEuler = _cups[_currentCupId].transform.localEulerAngles;
                cupEuler.z = Mathf.LerpAngle(cupEuler.z, -90, 0.1f);
                _cups[_currentCupId].transform.localEulerAngles = cupEuler;

                if (Mathf.Abs(_cups[_currentCupId].transform.localEulerAngles.z - 90) < 181) {
                    _isRotating = false;
                }
            }
        }

        if (_moveToDown) {
            Vector3 _cupPosition = _cups[_currentCupId].transform.position;
            
            _cupPosition.y = Mathf.Lerp(_cupPosition.y, -8.6f, 0.05f);
            _cups[_currentCupId].transform.position = _cupPosition;
            if (Mathf.Abs(_cupPosition.y - -8.6f) < 1 ) {
                _moveToDown = false;
                _isReady = true;
            }
        }
        
        
    }

    private void NextCup() {
        Debug.Log("NextCup");
        _currentCupId++;
        StartCoroutine(ReadyTimer());
        StartCoroutine(MoveToDown());
        _cups[_currentCupId].GetComponent<BoxCollider2D>().enabled = false;
        SpawnManager.GetInstance()._currentCup = _cups[_currentCupId].transform;
    }
    private IEnumerator FirstPouringTimer() {
        _isStageStarted = true;
        _isRotating = true;
        yield return new WaitForSeconds(_pouringSec);
        _isStageStarted = false;
        EventManager.GetInstance().DoStartPouring();
    }

    private IEnumerator ReadyTimer() {
        _isReady = false;
        yield return new WaitForSeconds(1.5f);
        _isReady = true;
    }

    private IEnumerator MoveToDown() {
        _moveToDown = true;
        yield return new WaitForSeconds(1.5f);
        _moveToDown = false;
    }
}

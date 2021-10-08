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

    private void Start() {
        EventManager.GetInstance().OnCupPassed += NextCup;
        SpawnManager.GetInstance()._currentCup = _cups[0].transform;
    }

    private void Update() {

        if (Input.GetKeyDown("a")) {
            NextCup();
        }

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

            if (Mathf.Abs(_cups[_currentCupId].transform.localEulerAngles.z - 90) < 1) {
                _isRotating = false;
            }
        }
    }

    private void NextCup() {
        Debug.Log("NextCup");
        _currentCupId++;
        SpawnManager.GetInstance()._currentCup = _cups[_currentCupId].transform;
    }
    private IEnumerator FirstPouringTimer() {
        _isStageStarted = true;
        _isRotating = true;
        yield return new WaitForSeconds(_pouringSec);
        EventManager.GetInstance().DoStartPouring();
    }

}

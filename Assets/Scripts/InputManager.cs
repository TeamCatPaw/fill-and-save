using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private int _currentCupId;
    [SerializeField] private GameObject[] _cups;

    private void Start() {
        EventManager.GetInstance().OnCupPassed += NextCup;
        SpawnManager.GetInstance()._currentCup = _cups[0].transform;
    }

    private void Update() {

        if (Input.GetKeyDown("a")) {
            NextCup();
        }

        if (Input.GetMouseButton(0)) {
            Vector3 _cupPosition = _cups[_currentCupId].transform.position;
            _cupPosition.x = Mathf.Clamp(Input.mousePosition.x / Screen.width * 8f, 0f, 100f);//Min ve Max level design'a göre düzenlenecek
            //Debug.Log(Input.mousePosition.x / Screen.width);
            _cups[_currentCupId].transform.position = _cupPosition;
        }
    }

    private void NextCup() {
        _currentCupId++;
        SpawnManager.GetInstance()._currentCup = _cups[_currentCupId].transform;
    }

}

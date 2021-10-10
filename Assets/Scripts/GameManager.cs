using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour//
{
    public int _dropCounter;
    private int _cupCounter = 1;
    private int _totalCupCount;

    public int _nextStageDropCount = 0;

    private void Start() {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 30;

        EventManager.GetInstance().OnDropCreated += IncreaseDropCounter;

        EventManager.GetInstance().OnDropBurned += DecreaseDropCounter;
        EventManager.GetInstance().OnDropBurned += LoseCheck;

        EventManager.GetInstance().OnDropCollected += DecreaseDropCounter;
        EventManager.GetInstance().OnDropCollected += IncreaseNextStageDropCounter;
        EventManager.GetInstance().OnDropCollected += WinCheck;

        EventManager.GetInstance().OnDropPlaced += DecreaseDropCounter;
        EventManager.GetInstance().OnDropPlaced += WinCheck;

        _totalCupCount = GameObject.FindGameObjectsWithTag("Collect").Length;
        //_totalCupCount = 1;//SİL!
    }

    private void IncreaseDropCounter() {
        _dropCounter++;
    }
    private void DecreaseDropCounter() {
        _dropCounter--;
    }
    private void IncreaseNextStageDropCounter() {
        _nextStageDropCount++;
    }
    private void WinCheck() {
        if (_dropCounter == 0) {
            if (_totalCupCount == _cupCounter) {
                EventManager.GetInstance().DoWin();
                return;
            } else {
                _cupCounter++;
                EventManager.GetInstance().DoCupPassed();
                Debug.Log("Cup Passed");
            }
            
        }
    }
    private void LoseCheck() {
        if (_dropCounter <= 0) {
            EventManager.GetInstance().DoLose();
        }
    }
    private void Update() {
        if (Input.GetKeyDown("d")) {
            Debug.Log(_dropCounter);
        }
        if (Input.GetKeyDown("c")) {
            Debug.Log(_cupCounter);
        }
    }
}

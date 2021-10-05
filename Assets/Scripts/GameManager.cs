using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour//
{
    private int _dropCounter;
    private int _cupCounter;

    private void Start() {
        EventManager.GetInstance().OnDropCreated += IncreaseDropCounter;

        EventManager.GetInstance().OnDropBurned += DecreaseDropCounter;
        EventManager.GetInstance().OnDropBurned += LoseCheck;

        EventManager.GetInstance().OnDropCollected += DecreaseDropCounter;
        EventManager.GetInstance().OnDropCollected += WinCheck;

        EventManager.GetInstance().OnDropPlaced += DecreaseDropCounter;
        EventManager.GetInstance().OnDropPlaced += WinCheck;

    }

    private void IncreaseDropCounter() {
        _dropCounter++;
    }
    private void DecreaseDropCounter() {
        _dropCounter--;
    }
    
    private void WinCheck() {
        if (_dropCounter <= 0) {
            if (_cupCounter == 1) {
                Debug.Log("Win");
                return;
            }
            _cupCounter++;
            EventManager.GetInstance().DoCupPassed();
            Debug.Log("Cup Passed");
        }
    }
    private void LoseCheck() {
        if (_dropCounter <= 0) {
            Debug.Log("Lose");
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

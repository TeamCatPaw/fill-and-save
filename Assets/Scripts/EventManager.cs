using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    #region Singleton
    private static EventManager _instance;
    public static EventManager GetInstance() {
        return _instance;
    }

    public void Awake() {
        SingletonPattern();
    }
    private void SingletonPattern() {
        if (_instance == null) {
            _instance = this;
        } else if (_instance != this) {
            Destroy(gameObject);
        }
    }
    #endregion

    public event Action OnStartPouring;

    public event Action OnDropCreated;
    public event Action OnDropCollected;
    public event Action OnDropBurned;
    public event Action OnDropPlaced;

    public event Action OnWin;
    public event Action OnLose;

    public event Action OnCupPassed;

    public void DoStartPouring() {
        OnStartPouring?.Invoke();
    }
    public void DoDropCreated() {
        OnDropCreated?.Invoke();
    }
    public void DoDropCollected() {
        OnDropCollected?.Invoke();
    }
    public void DoDropBurned() {
        OnDropBurned?.Invoke();
    }
    public void DoDropPlaced() {
        OnDropPlaced?.Invoke();
    }
    public void DoWin() {
        OnWin?.Invoke();
    }
    public void DoLose() {
        OnLose?.Invoke();
    }
    public void DoCupPassed() {
        OnCupPassed?.Invoke();
    }
}

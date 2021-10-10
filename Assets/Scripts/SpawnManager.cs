using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    #region Singleton
    private static SpawnManager _instance;
    public static SpawnManager GetInstance() {
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

    //public int _dropCounter = 0;//object pooling olunca private olacak.
    private bool _isAvailableForSpawn = true;

    private float _delayBetweenDrops = 0.01f;

    [SerializeField] private GameObject _waterDrop;

    Queue<Vector3> _spawnPositions = new Queue<Vector3>();
    Queue<int> _spawnPositionIds = new Queue<int>();

    [HideInInspector] public Transform _currentCup;

    private int _firstDropCount;

    private bool _isFirstPouringStarted;

    private GameManager _gameManager;

    private void Start() {
        _gameManager = GetComponent<GameManager>();
        _firstDropCount = 10;
        EventManager.GetInstance().OnStartPouring += CreateforCup;
    }

    private void Update() {
        if (_firstDropCount > 0 && _isFirstPouringStarted && _isAvailableForSpawn) {
            Spawn(_currentCup.position, 0, true);//0 hata almamak için bir anlamı yok.
            _firstDropCount--;
        }

        if (_spawnPositions.Count > 0 && _isAvailableForSpawn) {
            Spawn(_spawnPositions.Dequeue(), _spawnPositionIds.Dequeue());

        }

    }

    public void AddToQueue(Vector3 _spawnPoint, int _multiplier, int _areaId) {

        for (int i = 1; i < _multiplier; i++) {
            _spawnPositions.Enqueue(_spawnPoint);
            _spawnPositionIds.Enqueue(_areaId);
        }
    }

    private void Spawn(Vector3 _spawnPoint, int areaId, bool isFirstDrops = false) {
        GameObject drop = Instantiate(_waterDrop, _spawnPoint, Quaternion.identity, transform);
        drop.SetActive(true);
        if (!isFirstDrops) {
            drop.GetComponent<Drop>().ignoredAreas.Add(areaId);
        }
        EventManager.GetInstance().DoDropCreated();
        StartCoroutine(SpawnTimer());
    }

    private IEnumerator SpawnTimer() {
        _isAvailableForSpawn = false;
        yield return new WaitForSeconds(_delayBetweenDrops);
        _isAvailableForSpawn = true;
    }
    private void CreateforCup() {
        _isFirstPouringStarted = true;
        if (_gameManager._nextStageDropCount > _firstDropCount) {
            _firstDropCount = _gameManager._nextStageDropCount;
            _gameManager._nextStageDropCount = 0;
        }
    }
}

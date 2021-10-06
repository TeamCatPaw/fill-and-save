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
    [SerializeField]
    [Range(0f, .3f)] private float _delayBetweenDrops = 0.05f;

    [SerializeField] private GameObject _waterDrop;

    Queue<Vector3> _spawnPositions = new Queue<Vector3>();

    [HideInInspector] public Transform _currentCup;

    [SerializeField] private int _firstDropCount;

    private bool _isFirstPouringStarted;

    private void Start() {
        EventManager.GetInstance().OnStartPouring += CreateforCup;
    }

    private void Update() {

        if (Input.GetKeyDown("space")) {

        }

        if (_firstDropCount > 0 && _isFirstPouringStarted && _isAvailableForSpawn) {
            Spawn(_currentCup.position);
            _firstDropCount--;
        }

        if (_spawnPositions.Count > 0 && _isAvailableForSpawn) {
            Spawn(_spawnPositions.Dequeue());

        }

    }

    private int areaId;
    public void AddToQueue(Vector3 _spawnPoint, int _multiplier, int _areaId) {
        areaId = _areaId;
        for (int i = 1; i < _multiplier; i++) {
            _spawnPositions.Enqueue(_spawnPoint);
        }
    }

    private void Spawn(Vector3 _spawnPoint) {
        GameObject drop = Instantiate(_waterDrop, _spawnPoint, Quaternion.identity, transform);
        drop.SetActive(true);
        drop.GetComponent<Drop>().ignoredAreas.Add(areaId);
        EventManager.GetInstance().DoDropCreated();
        StartCoroutine(SpawnTimer());
    }

    private IEnumerator SpawnTimer() {
        _isAvailableForSpawn = false;
        yield return new WaitForSeconds(_delayBetweenDrops);
        _isAvailableForSpawn = true;
    }
    private void CreateforCup() {
        //for (int i = 0; i < _firstDropCount; i++) {
        //    _spawnPositions.Enqueue(_currentCup.position);
        //}
        _isFirstPouringStarted = true;
    }
}

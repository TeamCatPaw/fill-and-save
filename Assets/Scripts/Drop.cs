using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{

    public List<int> ignoredAreas = new List<int>();
    private MultiplyArea currentArea;

    private void Start() {
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        currentArea = collision.GetComponent<MultiplyArea>();

        if (collision.CompareTag("Multiply") && !ignoredAreas.Contains(currentArea.areaId)) {

            Multiply(currentArea.multiplier, currentArea.areaId);
            currentArea = null;

        }else if (collision.CompareTag("Lava")) {
            SpawnManager.GetInstance()._dropCounter--;
            Debug.Log(SpawnManager.GetInstance()._dropCounter);
            Destroy(gameObject);
        }
    }


    private void Multiply(int _multiplier, int _areaId) {
        SpawnManager.GetInstance().AddToQueue(transform.position, _multiplier, _areaId);
        ignoredAreas.Add(_areaId);
    }
}

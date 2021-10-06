using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    [SerializeField] private GameObject steamParticle;
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

            EventManager.GetInstance().DoDropBurned(); 
            Instantiate(steamParticle, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }else if (collision.CompareTag("Collect")) {

            EventManager.GetInstance().DoDropCollected();
            Destroy(gameObject);
        }else if (collision.CompareTag("Aquarium")) {

            EventManager.GetInstance().DoDropPlaced();
        }
    }


    private void Multiply(int _multiplier, int _areaId) {
        SpawnManager.GetInstance().AddToQueue(transform.position, _multiplier, _areaId);
        ignoredAreas.Add(_areaId);
    }
}

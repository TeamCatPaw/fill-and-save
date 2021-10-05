using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MultiplyArea : MonoBehaviour {

    [Tooltip("Must be unique in level")]
    public int areaId;

    public int multiplier;

    private void Start() {
        transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = "x" + multiplier.ToString();
    }

}

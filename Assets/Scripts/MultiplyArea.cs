using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MultiplyArea : MonoBehaviour {

    [Tooltip("Must be unique in level")]
    public int areaId;

    public int multiplier;

    private Transform _myText;
    private bool _isLarging, _isSmalling;

    private void Start() {
        _myText = transform.GetChild(0).GetChild(1);
        _myText.GetComponent<TextMeshProUGUI>().text = "x" + multiplier.ToString();
    }
    private void Update() {

        float targetValue = 1.7f;
        if (_isLarging) {
            _myText.localScale = Vector3.Lerp(_myText.localScale, new Vector3(targetValue, targetValue, 1), 0.2f);

            if (targetValue - _myText.localScale.x < 0.1f) {
                _isLarging = false; _isSmalling = true;
            }
        }else if (_isSmalling) {
            _myText.localScale = Vector3.Lerp(_myText.localScale, Vector3.one, 0.25f);

            if (_myText.localScale.x - 1f < 0.1f) {
                _isSmalling = false;
            }
        }
    }
    public void ScaleText() {
        _myText.localScale = Vector3.one;
        _isLarging = true;
        
    }
}

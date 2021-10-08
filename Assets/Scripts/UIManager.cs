using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private GameObject WinPanel, LosePanel;
    [SerializeField] private float _panelTime;

    private void Start() {
        WinPanel = GameObject.Find("UI_Canvas").transform.GetChild(1).gameObject;
        LosePanel = GameObject.Find("UI_Canvas").transform.GetChild(0).gameObject;

        EventManager.GetInstance().OnWin += OpenWinPanel;
        EventManager.GetInstance().OnLose += OpenLosePanel;
    }

    private void OpenWinPanel() {
        StartCoroutine(WinPanelTimer(WinPanel));
    }
    private void OpenLosePanel() {
        StartCoroutine(WinPanelTimer(LosePanel));
    }
    private IEnumerator WinPanelTimer(GameObject panel) {
        yield return new WaitForSeconds(_panelTime);
        panel.SetActive(true);
    }
}

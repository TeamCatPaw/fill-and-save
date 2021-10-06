using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject WinPanel, LosePanel;
    [SerializeField] private float _panelTime;

    private void Start() {
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

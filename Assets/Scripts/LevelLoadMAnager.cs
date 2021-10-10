using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LevelLoadMAnager : MonoBehaviour
{

    [SerializeField] private int toLevel;

    private void Start() {
#if UNITY_EDITOR

        PlayerPrefs.SetInt("SavedLevel", toLevel);
#endif

        if (PlayerPrefs.GetInt("SavedLevel") == 0) {
            PlayerPrefs.SetInt("SavedLevel", 1);
        }
        if (SceneManager.GetActiveScene().buildIndex != PlayerPrefs.GetInt("SavedLevel")) {
            NextLevel();
        }
        FindButtons();
        EventManager.GetInstance().OnWin += SaveLevel;

    }


    private void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void NextLevel() {
        int levelCount = PlayerPrefs.GetInt("SavedLevel");
        //Debug.Log(levelCount);
        if (levelCount < 21) {
            SceneManager.LoadScene(levelCount);
        } else {
            SceneManager.LoadScene((levelCount % 20) + 10);
        }
    }
    public void SaveLevel() {
        PlayerPrefs.SetInt("SavedLevel", SceneManager.GetActiveScene().buildIndex + 1);
        //Debug.Log("SavedLevel" + " " + PlayerPrefs.GetInt("SavedLevel"));
    }
    private void FindButtons() {
        GameObject.Find("UI_Canvas").transform.GetChild(1).GetChild(2).gameObject.GetComponent<Button>().onClick.AddListener(Restart);
        GameObject.Find("UI_Canvas").transform.GetChild(2).GetChild(5).gameObject.GetComponent<Button>().onClick.AddListener(NextLevel);
        GameObject.Find("UI_Canvas").transform.GetChild(0).GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(Restart);

        
        
        //Debug.Log(GameObject.Find("UI_Canvas").transform.GetChild(0).GetChild(2).name);
        //Debug.Log(GameObject.Find("UI_Canvas").transform.GetChild(1).GetChild(5).name);
    }
}

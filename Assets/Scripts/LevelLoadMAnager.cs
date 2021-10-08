using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoadMAnager : MonoBehaviour
{

    private void Start() {
        PlayerPrefs.SetInt("SavedLevel", 11);
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
        SceneManager.LoadScene(levelCount % 20);
    }
    public void SaveLevel() {
        PlayerPrefs.SetInt("SavedLevel", SceneManager.GetActiveScene().buildIndex + 1);
        //Debug.Log("SavedLevel" + " " + PlayerPrefs.GetInt("SavedLevel"));
    }
    private void FindButtons() {
        GameObject.Find("UI_Canvas").transform.GetChild(0).GetChild(2).gameObject.GetComponent<Button>().onClick.AddListener(Restart);
        GameObject.Find("UI_Canvas").transform.GetChild(1).GetChild(5).gameObject.GetComponent<Button>().onClick.AddListener(NextLevel);
        //Debug.Log(GameObject.Find("UI_Canvas").transform.GetChild(0).GetChild(2).name);
        //Debug.Log(GameObject.Find("UI_Canvas").transform.GetChild(1).GetChild(5).name);
    }
}

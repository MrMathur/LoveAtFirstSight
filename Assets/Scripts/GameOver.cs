using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    private GameObject endModal;
    private MusicSettings musicSettings;

    void Start() {
        endModal = GameObject.FindGameObjectsWithTag("EndModal")[0];
        endModal.SetActive(false);
        musicSettings = GameObject.FindGameObjectsWithTag("Environment")[0].GetComponent<MusicSettings>();
    }

    public void GameOverFunction() {
        endModal.SetActive(true);
        Time.timeScale = 0;
        musicSettings.StartHeartBeat();
    }
}

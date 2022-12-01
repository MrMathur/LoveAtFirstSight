using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    private GameObject endModal;

    void Start() {
        endModal = GameObject.FindGameObjectsWithTag("EndModal")[0];
        endModal.SetActive(false);
    }

    public void GameOverFunction() {
        endModal.SetActive(true);
        Time.timeScale = 0;
    }
}

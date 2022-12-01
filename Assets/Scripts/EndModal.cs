using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndModal : MonoBehaviour
{

    private GameObject endModal;
    private MusicSettings musicSettings;

    // Start is called before the first frame update
    void Start()
    {
        endModal = GameObject.FindGameObjectsWithTag("EndModal")[0];

        endModal.SetActive(false);

        musicSettings = GameObject.FindGameObjectsWithTag("Environment")[0].GetComponent<MusicSettings>();
    }

    public void GameOver() {
        endModal.SetActive(true);
        Time.timeScale = 0;

        musicSettings.StartHeartBeat();
    }
}

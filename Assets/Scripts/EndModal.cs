using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndModal : MonoBehaviour
{

    private GameObject endModal;
    private MusicSettings musicSettings;
    private bool heartBeating;

    // Start is called before the first frame update
    void Start()
    {
        endModal = GameObject.FindGameObjectsWithTag("EndModal")[0];

        endModal.SetActive(false);

        musicSettings = GameObject.FindGameObjectsWithTag("Environment")[0].GetComponent<MusicSettings>();

        heartBeating = false;
    }

    public void GameOver() {
        endModal.SetActive(true);
        Time.timeScale = 0;

        if (!heartBeating) {
            musicSettings.StartHeartBeat();
            heartBeating = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndModal : MonoBehaviour
{

    private GameObject endModal;

    // Start is called before the first frame update
    void Start()
    {
        endModal = GameObject.FindGameObjectsWithTag("EndModal")[0];

        endModal.SetActive(false);
    }

    public void GameOver() {
        endModal.SetActive(true);
        Time.timeScale = 1;
    }
}

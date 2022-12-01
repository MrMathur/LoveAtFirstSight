using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ModalSetting : MonoBehaviour
{

    [SerializeField] private bool startWithInstruction;

    private GameObject modal;


    // Start is called before the first frame update
    void Start()
    {
        modal = GameObject.FindGameObjectsWithTag("Modal")[0];

        if (startWithInstruction) {
            OpenModal();
        } else {
            CloseModal();
        }
    }

    public void OpenModal() {
        modal.SetActive(true);
        Time.timeScale = 0;
    }

    public void CloseModal() {
        modal.SetActive(false);
        Time.timeScale = 1;
    }

    public void RestartScene() {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        Time.timeScale=1;
    }
}

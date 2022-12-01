using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuFunctions : MonoBehaviour
{

    private int menuIndex = 0;
    private int levelSelectIndex = 1;
    private int startIndex = 2;
    private int creditsIndex;

    [SerializeField] private bool startPaused;
    
    private GameObject env;
    private GameObject modal;

    private void Start() {
        env = GameObject.FindGameObjectsWithTag("Environment")[0];
        creditsIndex = SceneManager.sceneCountInBuildSettings-1;

        modal = GameObject.FindGameObjectsWithTag("Modal")[0];

        if (startPaused) {
            Time.timeScale = 0;
        }
    }

    public void StartGame() {       
        SceneManager.LoadScene(startIndex, LoadSceneMode.Single);
    }

    public void LoadMainMenu() {
        SceneManager.LoadScene(menuIndex, LoadSceneMode.Single);
    }

    public void LoadCredits() {
        SceneManager.LoadScene(creditsIndex, LoadSceneMode.Single);
    }

    public void RestartScene() {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void LoadLevelSelectMenu() {
        SceneManager.LoadScene(levelSelectIndex, LoadSceneMode.Single);
    }

    public void ToggleMute() {
        env.GetComponent<AudioSource>().mute = !env.GetComponent<AudioSource>().mute;
    }

    public void closeModal() {
        this.modal.SetActive(false);
        Time.timeScale = 1;
    }

    public void openModal() {
        modal.SetActive(true);
        Time.timeScale = 0;
    }
}

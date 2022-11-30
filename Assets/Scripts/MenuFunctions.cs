using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuFunctions : MonoBehaviour
{

    private int menuIndex = 0;
    private int levelSelectIndex = 1;
    private int creditsIndex = 2;
    private int startIndex = 3;

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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuFunctions : MonoBehaviour
{

    private int menuIndex = 0;
    private int creditsIndex = 1;
    private int startIndex = 2;

    public void StartGame() {       
        SceneManager.LoadScene(startIndex, LoadSceneMode.Single);
    }

    public void LoadMainMenu() {
        SceneManager.LoadScene(menuIndex, LoadSceneMode.Single);
    }

    public void LoadCredits() {
        SceneManager.LoadScene(creditsIndex, LoadSceneMode.Single);
    }
}

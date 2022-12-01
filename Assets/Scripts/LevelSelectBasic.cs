using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using UnityEngine.UI;
using TMPro;

public class LevelSelectBasic : MonoBehaviour
{
    // Start is called before the first frame update
    public Color green;
    public Color beige;
    private int test;
    [SerializeField] GameObject[] playButtons;

    void Start()
    {
        test = 0;
        foreach (var level in PlayerStats.Levels) {
            if (level.cleared) {
                playButtons[test].GetComponent<Image>().color = green;
            } else {
                playButtons[test].GetComponent<Image>().color = beige;

            }
            test+=1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
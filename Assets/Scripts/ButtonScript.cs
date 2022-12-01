using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private string buttonId;
    private GameObject[] doors;
    private MusicSettings musicSettings;

    // private AudioSource keySound;
    void Start()
    {
        doors = GameObject.FindGameObjectsWithTag("BlockDoor");
        musicSettings = GameObject.FindGameObjectsWithTag("Environment")[0].GetComponent<MusicSettings>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        // keySound.Play(0);

        foreach (GameObject door in doors) {
            if (buttonId == door.GetComponent<DoorScript>().getDoorId()){
                door.GetComponent<DoorScript>().destroyDoor();
            }
        }

        musicSettings.PlayKeySound();

        Destroy(gameObject);

    }
}

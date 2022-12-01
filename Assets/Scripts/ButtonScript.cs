using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private string buttonId;
    private GameObject[] doors;
    // private AudioSource keySound;
    void Start()
    {
        doors = GameObject.FindGameObjectsWithTag("BlockDoor");
        // keySound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        // keySound.Play(0);

        foreach (GameObject door in doors) {
            if (buttonId == door.GetComponent<DoorScript>().getDoorId()){
                door.GetComponent<DoorScript>().destroyDoor();
            }
        }

        Destroy(gameObject);

    }
}

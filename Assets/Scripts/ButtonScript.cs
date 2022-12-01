using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private string buttonId;
    [SerializeField] private GameObject button;
    private GameObject[] doors;
    void Start()
    {
        doors = GameObject.FindGameObjectsWithTag("BlockDoor");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        button.GetComponent<AudioSource>().Play(0);

        foreach (GameObject door in doors) {
            if (buttonId == door.GetComponent<DoorScript>().getDoorId()){
                door.GetComponent<DoorScript>().destroyDoor();
            }
        }
        col.enabled = false;

    }
}

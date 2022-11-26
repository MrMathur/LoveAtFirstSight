using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    // Start is called before the first frame 
    
    [SerializeField] private string doorId;
    [SerializeField] private GameObject door;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void destroyDoor(){
        Destroy(door);
    }

    public string getDoorId(){
        return doorId;
    }
}

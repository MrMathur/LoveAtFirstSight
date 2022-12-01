using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    // Start is called before the first frame 
    
    [SerializeField] private string doorId;
    
    public void destroyDoor(){
        Destroy(gameObject);
    }

    public string getDoorId(){
        return doorId;
    }
}

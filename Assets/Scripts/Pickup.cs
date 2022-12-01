using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private GameObject fieldOfView_go;
    private GameObject player;

    [SerializeField] float editedViewAngle;
    [SerializeField] float editedViewDistance;

    private FieldOfView fieldOfView_script;

    private AudioSource keySound;
    private Animator player_animator;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        fieldOfView_go = GameObject.FindGameObjectsWithTag("FieldOfView")[0];
        fieldOfView_script = fieldOfView_go.GetComponent<FieldOfView>();    

        keySound = GetComponent<AudioSource>();
        player_animator = player.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        keySound.Play(0);

        if (editedViewAngle != 0) {
            fieldOfView_script.setViewAngle(editedViewAngle);
        }
        if (editedViewDistance != 0) {
            fieldOfView_script.setViewDistance(editedViewDistance);
        }

        player_animator.SetBool("hasShades", true);
        
        Destroy(gameObject);
    }
}

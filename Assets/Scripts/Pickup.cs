using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private GameObject fieldOfView_go;
    private GameObject player;
    private GameObject[] enemies;
    [SerializeField] float editedPlayerSpeed;
    [SerializeField] float editedEnemySpeed;
    [SerializeField] float editedViewAngle;
    [SerializeField] float editedViewDistance;
    [SerializeField] GameObject current;
    private FieldOfView fieldOfView_script;


    // Start is called before the first frame update
    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        player = GameObject.FindGameObjectsWithTag("Player")[0];
        fieldOfView_go = GameObject.FindGameObjectsWithTag("FieldOfView")[0];
        fieldOfView_script = fieldOfView_go.GetComponent<FieldOfView>();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        current.GetComponent<AudioSource>().Play(0);

        if (editedViewAngle != 0) {
            fieldOfView_script.setViewAngle(editedViewAngle);
        }
        if (editedViewDistance != 0) {
            fieldOfView_script.setViewDistance(editedViewDistance);
        }
        if (editedEnemySpeed !=0) {
            foreach (GameObject enemy in enemies) {
                enemy.GetComponent<EnemyMovement>().setSpeed(editedEnemySpeed);
            }
        }
        if (editedPlayerSpeed!=0) {
            player.GetComponent<PlayerMovement>().setSpeed(editedPlayerSpeed);
        }
        Destroy(current);
    }
}

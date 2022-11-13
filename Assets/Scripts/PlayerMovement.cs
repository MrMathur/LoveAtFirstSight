using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float speed;
    private Rigidbody2D player_rb;

    private float move_x, move_y;

    private void Start() {
        player_rb = GetComponent<Rigidbody2D>();

        move_x = 0f;
        move_y = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        move_x = Input.GetAxis("Horizontal") * speed;
        move_y = Input.GetAxis("Vertical") * speed; 
    }

    void FixedUpdate() {
        player_rb.velocity = new Vector2(move_x, move_y);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float speed;
    private Rigidbody2D player_rb;
    private float initSpeed;

    private float move_x, move_y;

    private GameObject fieldOfView_go;
    private FieldOfView fieldOfView_script;

    private Animator player_animator;

    private bool isMoving;
    private bool playerCanMove;
    private bool facingRight;

    private void Start() {
        player_rb = GetComponent<Rigidbody2D>();
        initSpeed = speed;
        move_x = 0f;
        move_y = 0f;

        fieldOfView_go = GameObject.FindGameObjectsWithTag("FieldOfView")[0];
        fieldOfView_script = fieldOfView_go.GetComponent<FieldOfView>();
        player_animator = GetComponent<Animator>();

        facingRight = true;

        isMoving = false;

        playerCanMove = true;
    }

    public void setSpeedBack(){
        speed = initSpeed;
    }

    public void setSpeed(float y){
        speed = y;
    }

    void FixedUpdate() {

        if (Input.anyKey && playerCanMove) {
            
            if (Input.GetAxis("Vertical") > 0) {
                move_y = speed;
            } else if (Input.GetAxis("Vertical") < 0) {
                move_y = -speed;
            } else {
                move_y = 0;
            }

            if (Input.GetAxis("Horizontal") > 0) {
                move_x = speed;
            } else if (Input.GetAxis("Horizontal") < 0) {
                move_x = -speed;
            } else {
                move_x = 0;
            }

            isMoving = (move_x != 0 || move_y != 0);
        } else {
            isMoving = false;
        }

        // Move (if keydown)
        if (isMoving) {
            player_rb.velocity = new Vector2(move_x, move_y);
        } else {
            player_rb.velocity = new Vector2(0, 0);
        }

        player_animator.SetBool("isMoving", isMoving);

        // Update FieldOfView (Vision Cone)
        fieldOfView_script.SetOrigin(new Vector2(transform.position.x, transform.position.y));
    }

    public void FlipCharacter(bool isDirRight) {
        if (facingRight != isDirRight) {
            Vector3 currentScale = gameObject.transform.localScale;
            currentScale.x *= -1;
            gameObject.transform.localScale = currentScale;

            facingRight = !facingRight;
        }
    }
}

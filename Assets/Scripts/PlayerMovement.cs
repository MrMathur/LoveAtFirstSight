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

    private bool facingRight;

    private void Start() {
        player_rb = GetComponent<Rigidbody2D>();
        initSpeed = speed;
        move_x = 0f;
        move_y = 0f;

        fieldOfView_go = GameObject.FindGameObjectsWithTag("FieldOfView")[0];
        fieldOfView_script = fieldOfView_go.GetComponent<FieldOfView>();

        facingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        move_x = Input.GetAxis("Horizontal") * speed;
        move_y = Input.GetAxis("Vertical") * speed; 
    }

    public void setSpeedBack(){
        speed = initSpeed;
    }

    public void setSpeed(float y){
        speed = y;
    }

    void FixedUpdate() {
        // Move (if keydown)
        if (Input.anyKey) {
            player_rb.velocity = new Vector2(move_x, move_y);
        } else {
            player_rb.velocity = new Vector2(0, 0);
        }

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

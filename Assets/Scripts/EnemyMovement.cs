using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    [SerializeField] private GameObject waypoints;
    [SerializeField] private bool followWaypoints;
    [SerializeField] public float speed;
    private float initSpeed;
    [SerializeField] private float timeDelay;
    [SerializeField] private float distanceThreshold;

    private Transform currentTarget;
    private Transform[] targets;
    private int targetIndex;
    private bool delayCondition;

    private Animator enemy_animator;
    private bool isMoving;

    // Start is called before the first frame update
    void Start()
    {
        targets = new Transform[waypoints.GetComponentsInChildren<Transform>().GetLength(0) - 1];
        initSpeed = speed;
        int i = 0;
        foreach (Transform t in waypoints.GetComponentsInChildren<Transform>())
        {
            if (i != 0) {
                targets[i - 1] = t;
            }

            i++;
        }

        targetIndex = 0;
        currentTarget = targets[targetIndex];

        delayCondition = true;

        enemy_animator = GetComponent<Animator>();
        isMoving = false;
    }

    public void setSpeedZero(){
        speed = 0;
        isMoving = false;
        enemy_animator.SetBool("isMoving", isMoving && followWaypoints);
    }

    public void setSpeed(float y) {
        speed = y;
        isMoving = true;
        enemy_animator.SetBool("isMoving", isMoving && followWaypoints);
    }

    public void setSpeedBack() {
        speed = initSpeed;
        isMoving = true;
        enemy_animator.SetBool("isMoving", isMoving && followWaypoints);
    }

    // Update is called once per frame
    void Update()
    {
        if (followWaypoints) {

            float distance = Vector2.Distance(transform.position, currentTarget.position);

            if (distance < distanceThreshold) {
                if (delayCondition) {
                    isMoving = false;
                    enemy_animator.SetBool("isMoving", isMoving && followWaypoints);
                    StartCoroutine(SetDelay());
                }
            } else {
                float step = speed * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, currentTarget.position, step);
                if (!isMoving) { 
                    isMoving = true;
                    enemy_animator.SetBool("isMoving", isMoving && followWaypoints);
                }
            }
            
        }
    }

    IEnumerator SetDelay() {
        delayCondition = false;
        yield return new WaitForSeconds(timeDelay);
        targetIndex = (targetIndex + 1) % targets.Length;
        currentTarget = targets[targetIndex];
        delayCondition = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FieldOfView : MonoBehaviour
{
    private Mesh mesh;

    [SerializeField] private float viewDistance;
    [SerializeField] private float viewAngle;
    [SerializeField] private int rayCount;
    [SerializeField] private float zOffset;
    
    private bool testEnd;

    private GameObject[] enemies;
    
    private LayerMask obstacleMask;

    private Vector3 origin;
    private float startingAngle;

    private float initViewDistance;
    private float initViewAngle;

    private GameObject player;
    private Animator player_animator;
    private Animator enemy_animator;
    private PlayerMovement player_movementScript;

    private List<GameObject> enemiesHit;

    private Camera cam;
    private MusicSettings musicSettings;

    private bool bubbleSoundPlaying;

    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        origin = new Vector3(0,0,zOffset);
        startingAngle = viewAngle / 2;

        obstacleMask = LayerMask.GetMask("Obstacle");
        // endGame = 0;
        cam = Camera.main;

        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        player = GameObject.FindGameObjectsWithTag("Player")[0];
        player_animator = player.GetComponent<Animator>();
        player_movementScript = player.GetComponent<PlayerMovement>();

        enemiesHit = new List<GameObject>();

        musicSettings = GameObject.FindGameObjectsWithTag("Environment")[0].GetComponent<MusicSettings>();
    }

     public void setViewAngleBack(){
        viewAngle = initViewAngle;
        startingAngle = viewAngle / 2;
    }

    public void setViewAngle(float y){
        viewAngle = y;
        startingAngle = viewAngle / 2;
    }

     public void setViewDistanceBack(){
        viewDistance = initViewDistance;
    }

    public void setViewDistance(float y){
        viewDistance = y;
    }

    void Update() {
        // Change angle
        Vector3 mousePos = Input.mousePosition;
        Vector3 point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, zOffset));
        Vector3 playerToMouse = point - origin;
        float turnToMouseAngle = Mathf.Atan((playerToMouse.y) / (playerToMouse.x));
        if (playerToMouse.x < 0) {
            SetStartingAngle(turnToMouseAngle + (180) * Mathf.Deg2Rad);  
            player_movementScript.FlipCharacter(false);
        } else {
            SetStartingAngle(turnToMouseAngle);     
            player_movementScript.FlipCharacter(true);   
        }

        player_animator.SetBool("isLookingDown", (playerToMouse.y < 0));
    }

    void LateUpdate()
    {
        // Draw FieldOfView
        float angle = startingAngle;
        float deltaAngle = viewAngle / (rayCount - 1);

        testEnd = false;

        int[] triangles = new int[(rayCount - 1) * 3];
        Vector3[] vertices = new Vector3[rayCount + 1];
        Vector2[] uv = new Vector2[rayCount + 1];

        vertices[0] = origin;
        for (int i = 1; i < rayCount; i++) {

            Vector2 direction = new Vector2(viewDistance * Mathf.Cos(angle), viewDistance * Mathf.Sin(angle));

            RaycastHit2D hit = Physics2D.Raycast(origin, direction, viewDistance, obstacleMask);

            if (hit.collider == null) {
                vertices[i] = new Vector3(direction.x + origin.x, direction.y + origin.y, origin.z);
            } else {
                vertices[i] = new Vector3(hit.point.x, hit.point.y, origin.z);

                if (hit.collider.tag == "Enemy") {
                    testEnd = true;

                    enemiesHit.Add(hit.collider.gameObject);
                }
            }

            

            angle -= deltaAngle;

            if (i > 1) {
                triangles[(i - 2) * 3 + 0] = 0;
                triangles[(i - 2) * 3 + 1] = i - 1;
                triangles[(i - 2) * 3 + 2] = i;
            }
        }        

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;

        // Drawing Complete

        if (testEnd) {
            player_movementScript.setSpeed(0);

            foreach(GameObject enemy in enemiesHit) {
                enemy.GetComponent<EnemyMovement>().setSpeedZero();
                enemy.GetComponent<Animator>().SetBool("isInCone", true);
            }

            player_animator.SetBool("EnemyWithinCone", true);

            if (!bubbleSoundPlaying) {
                bubbleSoundPlaying = true;
                musicSettings.StartBubbleSound();
            }            

        } else {
            player_movementScript.setSpeedBack();

            foreach(GameObject enemy in enemiesHit) {
                enemy.GetComponent<EnemyMovement>().setSpeedBack();
                enemy.GetComponent<Animator>().SetBool("isInCone", false);
            }

            player_animator.SetBool("EnemyWithinCone", false);

            enemiesHit.Clear();

            bubbleSoundPlaying = false;
            musicSettings.StopBubbleSound();
        }
    }

    public void SetOrigin(Vector2 origin) {
        this.origin = new Vector3(origin.x, origin.y, zOffset);
    }

    private void SetStartingAngle(float anglePlayer) {
        this.startingAngle = anglePlayer + viewAngle / 2;
    }
}
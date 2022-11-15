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

    private GameObject[] enemies;

    private LayerMask obstacleMask;

    private Vector3 origin;
    private float startingAngle;

    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        origin = new Vector3(0,0,zOffset);
        startingAngle = viewAngle / 2;

        obstacleMask = LayerMask.GetMask("Obstacle");

        cam = Camera.main;

        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    void Update() {
        // Change angle
        Vector3 mousePos = Input.mousePosition;
        Vector3 point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 0));
        Vector3 playerToMouse = point - origin;
        float turnToMouseAngle = Mathf.Atan((playerToMouse.y) / (playerToMouse.x));
        if (playerToMouse.x < 0) {
            SetStartingAngle(turnToMouseAngle + (180) * Mathf.Deg2Rad);  
        } else {
            SetStartingAngle(turnToMouseAngle);        
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // Draw FieldOfView
        float angle = startingAngle;
        float deltaAngle = viewAngle / rayCount;

        int[] triangles = new int[(rayCount - 1) * 3];
        Vector3[] vertices = new Vector3[rayCount + 1];

        vertices[0] = origin;
        for (int i = 1; i < rayCount; i++) {

            Vector2 direction = new Vector2(viewDistance * Mathf.Cos(angle), viewDistance * Mathf.Sin(angle));

            RaycastHit2D hit = Physics2D.Raycast(origin, direction, viewDistance, obstacleMask);

            if (hit.collider == null) {
                vertices[i] = new Vector3(direction.x + origin.x, direction.y + origin.y, origin.z);
            } else {
                vertices[i] = new Vector3(hit.point.x, hit.point.y, origin.z);
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

        // Look for Enemies
        foreach (GameObject enemy in enemies) {
            float enemyDistance = Vector3.Distance(origin, enemy.transform.position);
            if (enemyDistance < viewDistance) {
                Vector3 visionConeDirection = new Vector3(Mathf.Cos(startingAngle - viewAngle/2), Mathf.Sin(startingAngle - viewAngle/2), 0);
                Vector3 playerToEnemyDirection = new Vector3(enemy.transform.position.x - origin.x, enemy.transform.position.y - origin.y, 0);
                float enemyAngle = Vector3.Angle(playerToEnemyDirection, visionConeDirection) * Mathf.Deg2Rad;
                if (enemyAngle < (viewAngle / 2)) {
                    RaycastHit2D hit = Physics2D.Raycast(origin, new Vector2(enemy.transform.position.x - origin.x, enemy.transform.position.y - origin.y), viewDistance, obstacleMask);        
                    if (hit.collider == null) {
                        Scene scene = SceneManager.GetActiveScene();
                        SceneManager.LoadScene(scene.name);
                    } 
                }
            }
        }
    }

    public void SetOrigin(Vector2 origin) {
        this.origin = new Vector3(origin.x, origin.y, zOffset);
    }

    private void SetStartingAngle(float anglePlayer) {
        this.startingAngle = anglePlayer + viewAngle / 2;
    }
}

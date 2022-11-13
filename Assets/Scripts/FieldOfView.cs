using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{

    private Mesh mesh;

    [SerializeField] private float viewDistance;
    [SerializeField] private float viewAngle;
    [SerializeField] private int rayCount;
    [SerializeField] private float zOffset;

    private LayerMask layerMask;

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

        layerMask = LayerMask.GetMask("Obstacle");

        cam = Camera.main;
    }

    void Update() {
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
        float angle = startingAngle;
        float deltaAngle = viewAngle / rayCount;

        int[] triangles = new int[(rayCount - 1) * 3];
        Vector3[] vertices = new Vector3[rayCount + 1];

        vertices[0] = origin;
        for (int i = 1; i < rayCount; i++) {

            Vector2 direction = new Vector2(viewDistance * Mathf.Cos(angle), viewDistance * Mathf.Sin(angle));

            RaycastHit2D hit = Physics2D.Raycast(origin, direction, viewDistance, layerMask);

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
    }

    public void SetOrigin(Vector2 origin) {
        this.origin = new Vector3(origin.x, origin.y, zOffset);
    }

    private void SetStartingAngle(float anglePlayer) {
        this.startingAngle = anglePlayer + viewAngle / 2;
    }
}

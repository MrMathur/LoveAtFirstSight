using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{

    private Mesh mesh;

    [SerializeField] private float viewDistance;
    [SerializeField] private float viewAngle;
    [SerializeField] private int rayCount;

    private Vector3 origin;
    private float startingAngle;
    private float deltaAngle;

    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        origin = Vector3.zero;
        startingAngle = viewAngle / 2;

        deltaAngle = viewAngle / rayCount;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float angle = startingAngle;

        int[] triangles = new int[(rayCount - 1) * 3];
        Vector3[] vertices = new Vector3[rayCount + 1];

        vertices[0] = origin;
        for (int i = 1; i < rayCount; i++) {

            Vector2 direction = new Vector2(viewDistance * Mathf.Cos(angle), viewDistance * Mathf.Sin(angle));

            vertices[i] = new Vector3(direction.x, direction.y, 0);

            angle -= deltaAngle;

            if (i < rayCount - 1) {
                triangles[(i - 1) * 3 + 0] = 0;
                triangles[(i - 1) * 3 + 1] = i + 1;
                triangles[(i - 1) * 3 + 2] = i;
            }
        }        

        mesh.vertices = vertices;
        mesh.triangles = triangles;
    }
}

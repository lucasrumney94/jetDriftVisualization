using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
public class DeformSphere : MonoBehaviour {

    private Mesh mesh;
    private Vector3[] verts;

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        verts = mesh.vertices;
    }

    void Update()
    {

    }

    void OnDrawGizmos()
    {
        mesh = GetComponent<MeshFilter>().sharedMesh;
        verts = mesh.vertices;

        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(verts[0], 0.02f);
        for (int i = 1; i < verts.Length; i++)
        {
            Gizmos.color = new Color(1f, (float)i / verts.Length, (float)i / verts.Length);
            Gizmos.DrawSphere(verts[i], 0.02f);
            Gizmos.DrawLine(verts[i], verts[i - 1]);
        }
    }
}

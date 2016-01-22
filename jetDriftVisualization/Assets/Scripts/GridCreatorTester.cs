using UnityEngine;
using System.Collections;

public class GridCreatorTester : MonoBehaviour {

    public int width = 8;
    public bool twoSided = false;

    void Start()
    {
        GetComponent<MeshFilter>().mesh = GridCreator.CreateGrid(width, twoSided);
    }

    //void OnDrawGizmos()
    //{
    //    Vector3[] verts = GetComponent<MeshFilter>().sharedMesh.vertices;

    //    if (verts.Length > 0)
    //    {
    //        for (int i = 1; i < verts.Length; i++)
    //        {
    //            Gizmos.color = new Color(1f, (float)i / verts.Length, (float)i / verts.Length);
    //            Gizmos.DrawCube(verts[i], Vector3.one * 0.02f);
    //            Gizmos.DrawLine(verts[i], verts[i - 1]);
    //        }
    //        //for (int i = 0; i < verts.Length - width; i++)
    //        //{
    //        //    Gizmos.color = new Color((float)i / verts.Length, (float)i / verts.Length, 1f);
    //        //    Gizmos.DrawLine(verts[i], verts[i + width]);
    //        //}
    //        Gizmos.color = Color.blue;
    //        Gizmos.DrawSphere(verts[0], 0.05f);
    //    }
    //}
}

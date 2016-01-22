using UnityEngine;
using System.Collections;
//[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public static class SphereCreator {

    //[Range(1, 64)]
    //public int width;
    //public float radius;
    //private Mesh mesh;
    //private Vector3[] verts = new Vector3[0];

    public static Mesh CreateSpiralSphere(int width, float radius)
    {
        Mesh mesh = new Mesh();
        mesh.name = "Spiral Sphere";
        FillVerticies(mesh, width, radius);
        FillTriangles(mesh, width);
        return mesh;
    }

    private static void FillVerticies(Mesh mesh, int width, float radius)
    {
        Vector3[] verts = new Vector3[width * width];

        //Create rings
        for(int i = 0; i < verts.Length; i += width)
        {
            for(int j = 0; j < width; j++)
            {
                float yPos = Mathf.Cos(((float)(i + j) / verts.Length) * Mathf.PI) * radius;
                float xPos = Mathf.Sin(((float)j / width) * 2f * Mathf.PI) * Mathf.Sqrt((radius * radius) - (yPos * yPos)); //* (Mathf.Cos(yPos * Mathf.PI / 2f));
                float zPos = Mathf.Cos(((float)j / width) * 2f * Mathf.PI) * Mathf.Sqrt((radius * radius) - (yPos * yPos)); //* (Mathf.Cos(yPos * Mathf.PI / 2f));
                Vector3 vertPos = new Vector3(xPos, yPos, zPos);
                //Debug.Log(vertPos);
                verts[i + j] = vertPos;
            }
        }
        mesh.vertices = verts;
    }

    private static void FillTriangles(Mesh mesh, int width)
    {
        int[] triangles = new int[((width - 1) * (width * 2) + (width * 2)) * 3];
        int triIndex = 0;

        //Fill first loop
        for(int i = 0; i < width - 1; i++)
        {
            triangles[triIndex] = 0;
            triangles[triIndex + 1] = i + 1;
            triangles[triIndex + 2] = i + 2;
            triIndex += 3;
        }

        //Fill rest of sphere
        for(int i = width; i < mesh.vertices.Length - 1; i++)
        {
            triangles[triIndex] = i;
            triangles[triIndex + 1] = (i - width) + 1;
            triangles[triIndex + 2] = i - width;
            triIndex += 3;

            triangles[triIndex] = i;
            triangles[triIndex + 1] = i + 1;
            triangles[triIndex + 2] = (i - width) + 1;
            triIndex += 3;
        }

        //Fill last loop
        for (int i = mesh.vertices.Length; i > mesh.vertices.Length - width; i--)
        {
            triangles[triIndex] = mesh.vertices.Length - 1;
            triangles[triIndex + 1] = i - 1;
            triangles[triIndex + 2] = i - 2;
            triIndex += 3;
        }

        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }

    //void OnDrawGizmos()
    //{
    //    if (verts.Length > 0)
    //    {
    //        for (int i = 1; i < verts.Length; i++)
    //        {
    //            Gizmos.color = new Color(1f, (float)i / verts.Length, (float)i / verts.Length);
    //            Gizmos.DrawCube(verts[i], Vector3.one * 0.01f);
    //            Gizmos.DrawLine(verts[i], verts[i - 1]);
    //        }
    //        for (int i = 0; i < verts.Length - width; i++)
    //        {
    //            Gizmos.color = new Color((float)i / verts.Length, (float)i / verts.Length, 1f);
    //            Gizmos.DrawLine(verts[i], verts[i + width]);
    //        }
    //        Gizmos.color = Color.blue;
    //        Gizmos.DrawSphere(verts[0], 0.05f);
    //    }
    //}
}

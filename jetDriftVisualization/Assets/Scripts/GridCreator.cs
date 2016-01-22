using UnityEngine;
using System.Collections;

public static class GridCreator {

    public static Mesh CreateGrid(int width, bool twoSided = false)
    {
        Mesh mesh = new Mesh();
        mesh.name = "Grid";
        mesh.vertices = CreateVerts(width);
        mesh.normals = CreateNormals(width);
        mesh.triangles = CreateTriangles(width, twoSided);
        return mesh;
    }

    private static Vector3[] CreateVerts(int width)
    {
        Vector3[] verts = new Vector3[width * width];

        for(int i = 0; i < width; i++)
        {
            float yPos = (((float)i / (width - 1)) * 2f) - 1f;
            for(int j = 0; j < width; j++)
            {
                float xPos = (((float)j / (width - 1)) * 2f) - 1f;
                verts[(i * width) + j] = new Vector3(xPos, 0f, yPos);
            }
        }

        return verts;
    }

    private static Vector3[] CreateNormals(int width)
    {
        Vector3[] normals = new Vector3[width * width];

        for(int i = 0; i < normals.Length; i++)
        {
            normals[i] = Vector3.up;
        }

        return normals;
    }

    private static int[] CreateTriangles(int width, bool twoSided = false)
    {
        int[] tris = new int[(width - 1) * (width - 1) * 2 * 3 * (twoSided ? 2 : 1)];
        int triIndex = 0;

        for(int i = 0; i < ((width - 1) * (width - 1)); i += width)
        {
            for(int j = 0; j < width - 1; j++)
            {
                tris[triIndex] = i + j;
                tris[triIndex + 1] = i + j + width;
                tris[triIndex + 2] = i + j + 1;
                triIndex += 3;

                tris[triIndex] = i + j + 1;
                tris[triIndex + 1] = i + j + width;
                tris[triIndex + 2] = i + j + width + 1;
                triIndex += 3;

                if (twoSided)
                {
                    tris[triIndex] = i + j;
                    tris[triIndex + 1] = i + j + 1;
                    tris[triIndex + 2] = i + j + width;
                    triIndex += 3;

                    tris[triIndex] = i + j + 1;
                    tris[triIndex + 1] = i + j + width + 1;
                    tris[triIndex + 2] = i + j + width;
                    triIndex += 3;
                }
            }
        }

        return tris;
    }
}

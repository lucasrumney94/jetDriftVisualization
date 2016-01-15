using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
public class DeformSphere : MonoBehaviour {

    public int displacementFactor = 1000;
    public waveGenerator spectrum;
    public bool originalSet = false;

    private float[] averagedSamples;
    private Vector3[] originalVertexPositions;
    private Mesh mesh;
    private Vector3[] verts;

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        verts = mesh.vertices;
        originalVertexPositions = new Vector3[verts.Length];
        for(int i = 0; i < verts.Length; i++)
        {
            originalVertexPositions[i] = verts[i];
        }
    }

    void Update()
    {
        averagedSamples = DownsampleList(spectrum.Samples, verts.Length);
        SlideVerticies();
        mesh.vertices = verts;
    }

    //void OnDrawGizmos()
    //{
    //    mesh = GetComponent<MeshFilter>().sharedMesh;
    //    verts = mesh.vertices;

    //    for (int i = 1; i < verts.Length; i++)
    //    {
    //        Gizmos.color = new Color(1f, (float)i / verts.Length, (float)i / verts.Length);
    //        Gizmos.DrawSphere(verts[i], 0.02f);
    //        Gizmos.DrawLine(verts[i], verts[i - 1]);
    //    }
    //    Gizmos.color = Color.blue;
    //    Gizmos.DrawSphere(verts[0], 0.02f);
    //}

    private float[] DownsampleList(float[] values, int newLength)
    {
        float[] newList = new float[newLength];

        //Determine scale factor
        int scaleFactor = values.Length / newLength;

        //iterate through first list
        for(int i = 0, n = 0; i < values.Length && n < newList.Length; i += scaleFactor, n++)
        {
            //Average each set of [scaleFactor] values
            float sampleSum = 0f;
            for(int f = 0; f < scaleFactor; f++)
            {
                sampleSum += values[i + f];
            }
            newList[n] = sampleSum / scaleFactor;
        }

        return newList;
    }

    private void SlideVerticies()
    {
        for(int i = 0; i < verts.Length; i++)
        {
            //Deterimne vector to add
            Vector3 displacement = mesh.normals[i] * averagedSamples[i] * displacementFactor;
            verts[i] = originalVertexPositions[i] + displacement;
        }
    }
}

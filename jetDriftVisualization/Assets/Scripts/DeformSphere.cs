using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
public class DeformSphere : MonoBehaviour {

    public float displacementFactor = 1000;
    public waveGenerator spectrum;
    public bool originalSet = false;

    private float[] rescaledSamples;
    private Vector3[] originalVertexPositions;
    private Vector3[] originalNormals;
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

        originalNormals = new Vector3[verts.Length];
        for (int i = 0; i < verts.Length; i++)
        {
            originalNormals[i] = mesh.normals[i];
        }
    }

    void Update()
    {
        if (spectrum.Samples.Length > verts.Length)
        {
            rescaledSamples = DownsampleList(spectrum.Samples, verts.Length);
        }
        else if (spectrum.Samples.Length < verts.Length)
        {
            rescaledSamples = UpFillList(spectrum.Samples, verts.Length);
        }
        else rescaledSamples = spectrum.Samples;
        SlideVerticies();
        mesh.vertices = verts;
        mesh.RecalculateNormals();
    }

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

    private float[] UpsampleList(float[] values, int newLength)
    {
        float[] newList = new float[newLength];

        //Determine scale factor
        int scaleFactor = (int)Mathf.Floor((float)newLength / values.Length);

        newList[0] = values[0];
        //Interpolate between samples to form new list
        for(int i = 1, n = 1; i < values.Length && n < newLength; i += scaleFactor, n++)
        {
            float n0Pos = (float)(i - 1) / (values.Length - 1);
            float n1Pos = (float)i / (values.Length - 1);
            float slope = (values[i - 1] - values[i]) / (n1Pos - n0Pos);

            for(int a = 0; a < scaleFactor; a++)
            {
                float iPos = (float)i / (newLength - 1);
                iPos -= n0Pos;
                newList[i] = iPos * slope + values[i - 1];
            }
        }
        newList[newList.Length - 1] = values[values.Length - 1];

        return newList;
    }

    private float[] UpFillList(float[] values, int newLength)
    {
        float[] newList = new float[newLength];

        for(int i = 0; i < newLength; i += values.Length)
        {
            for(int n = 0; i + n < newLength && n < values.Length; n++)
            {
                newList[i + n] = values[n];
            }
        }

        return newList;
    }

    private void SlideVerticies()
    {
        for(int i = 0; i < verts.Length; i++)
        {
            //Deterimne vector to add
            Vector3 displacement = originalNormals[i] * rescaledSamples[i] * displacementFactor;
            verts[i] = originalVertexPositions[i] + displacement;
        }
    }
}

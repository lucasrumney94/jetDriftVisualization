using UnityEngine;
using System.Collections;
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class SphereCreator : MonoBehaviour {

    [Range(0, 5)]
    public int subdivisions;
    public float radius;
    public Mesh mesh;

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
    }

    private void FillVerticies()
    {

    }
}

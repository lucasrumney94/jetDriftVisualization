using UnityEngine;
using System.Collections;
using System;

public class OrbitalPosTester : MonoBehaviour {

    public float inversePositionScale = 1f;

    public float inclination;
    public float rightAscensionOfAscendingNode;
    [Range(0f, 1f)]
    public float eccentricity;
    public float arguementOfPeriapsis;
    [Range(0f, Mathf.PI * 2f)]
    public float meanAnomaly;
    public float meanMotion;

    public int accuracy = 25;
    public int conicSegments = 10;

    private Vector3[] orbitPositions;

    public GameObject planet;
    public HTMLParser parser;

    void Start()
    {

    }

    void Update()
    {
        DrawObjects();
        //double epoch = (DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
    }

    private void DrawObjects()
    {
        orbitPositions = new Vector3[parser.Satellites.Count];
        for (int i = 0; i < parser.Satellites.Count; i++)
        {
            Vector3 position = TLEtoVec3.Convert(parser.Satellites[i], accuracy, inversePositionScale);
            //Debug.Log(position);
            orbitPositions[i] = position;
        }
    }

    private void FillPositions()
    {
        orbitPositions = new Vector3[conicSegments];
        for(int i = 0; i < conicSegments; i++)
        {
            float anomaly = (i * 2f * Mathf.PI) / (conicSegments);
            //orbitPositions[i] = TLEtoVec3.Convert(inclination, rightAscensionOfAscendingNode, eccentricity, arguementOfPeriapsis, anomaly, meanMotion, accuracy, inversePositionScale);
        }
    }

    void OnDrawGizmos()
    {
        DrawObjects();
        Gizmos.color = Color.red;
        for (int i = 0; i < orbitPositions.Length; i++)
        {
            Gizmos.DrawCube(orbitPositions[i], Vector3.one);
            //Gizmos.DrawLine(orbitPositions[i], orbitPositions[i + 1]);
        }
        //Gizmos.DrawLine(orbitPositions[conicSegments - 1], orbitPositions[0]);
        //Gizmos.color = Color.green;
        //Gizmos.DrawLine(orbitPositions[0], Vector3.zero);
    }
}
